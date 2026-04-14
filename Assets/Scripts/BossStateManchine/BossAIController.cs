using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Transform player;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Animator animatorBoss;

    [Header("Vision")]
    [SerializeField] public float visualRadius = 20f;
    [SerializeField] public float visualAngle = 200f;
    [SerializeField] public LayerMask obstacleLayer;
    [SerializeField] public float attackRange = 2f;

    [Header("Patrol")]
    [SerializeField] public List<Transform> wayPoints = new List<Transform>();
    [HideInInspector] public int currentWayPointIndex = 0;
    [HideInInspector] public int pointDistance = 1;

    [Header("Chase")]
    [SerializeField] public float lostPlayerWaitTime = 1f;
    [HideInInspector] public float lostPlayerTimer = 1f;

    [Header("Search")]
    [SerializeField] public float searchRadius = 4f;
    [SerializeField] public float searchDuration = 4f;
    [SerializeField] public float giveUpChance = 0.4f;
    [HideInInspector] public Vector3 lastSeenPosition;
    [HideInInspector] public float searchTimer = 0f;

    [Header("Hearing")]
    [SerializeField] public float hearRadiusClose = 20f;
    [SerializeField] public float hearRadiusMedium = 30f;
    [SerializeField] public float hearRadiusFar = 50f;
    [SerializeField] public float noiseThreshold = 0.3f;

    [HideInInspector] public BossPatrolState patrolState;
    [HideInInspector] public BossChaseState chaseState;
    [HideInInspector] public BossAttackState attackState;
    [HideInInspector] public BossSearchState searchState;
    [HideInInspector] public BossStunState stunState;

    public BossBaseState currentState;

    public BossSound bossSound;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animatorBoss = GetComponent<Animator>();
        bossSound = GetComponent<BossSound>();

        patrolState = new BossPatrolState();
        chaseState = new BossChaseState();
        attackState = new BossAttackState();
        searchState = new BossSearchState();
        stunState = new BossStunState();
    }

    void Start()
    {

        SoundManager.Instance.OnSoundEmitted += HearSound;
        ChangeState(patrolState);
    }

    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void ChangeState(BossBaseState newState)
    {
        if (currentState == newState) return;
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public bool CanSee()
    {
        if (player == null) return false;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > visualRadius) return false;

        Vector3 dir = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dir) > visualAngle / 2f) return false;
        if (Physics.Raycast(transform.position, dir, dist, obstacleLayer)) return false;

        return true;
    }

    private void HearSound(SoundDataSO data, Vector3 soundPosition)
    {
        if (data.noiseValue < noiseThreshold) return;
        if (currentState == stunState || currentState == chaseState) return;

        float dist = Vector3.Distance(transform.position, soundPosition);

        if (dist <= hearRadiusClose)
        {
            ReactClose(soundPosition);
        }
        else if (dist <= hearRadiusMedium)
        {
            if (data.noiseValue >= 0.3f) ReactMedium(soundPosition);
            else if (Random.Range(0f, 1f) < 0.7f) ReactFar(soundPosition);
        }
        else if (dist <= hearRadiusFar)
        {
            if (data.noiseValue >= 0.5f) ReactFar(soundPosition);
            else if (Random.Range(0f, 1f) < 0.4f) ReactFar(soundPosition);
        }
    }

    public void ReactClose(Vector3 pos)
    {
        lastSeenPosition = pos;
        agent.SetDestination(pos);
        ChangeState(chaseState);
    }

    public void ReactMedium(Vector3 pos)
    {
        lastSeenPosition = pos;
        Vector3 offset = Random.insideUnitSphere * 3f;
        offset.y = 0;
        Vector3 target = pos + offset;

        if (NavMesh.SamplePosition(target, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
        else
            agent.SetDestination(pos);

        ChangeState(searchState);
    }

    public void ReactFar(Vector3 pos)
    {
        if (Random.Range(0f, 1f) <= 0.6f)
        {
            lastSeenPosition = pos;
            agent.SetDestination(pos);
            ChangeState(searchState);
        }
    }

    public void Stun(float duration)
    {
        if (currentState == stunState) return;
        ChangeState(stunState);
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        animatorBoss.ResetTrigger("Stun");
        ChangeState(patrolState);
    }

    public Vector3 GetRandomSearchPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * searchRadius;
        randomDir.y = 0;
        Vector3 searchTarget = lastSeenPosition + randomDir;

        if (NavMesh.SamplePosition(searchTarget, out NavMeshHit hit, searchRadius, NavMesh.AllAreas))
            return hit.position;

        return lastSeenPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visualRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,
            transform.position + DirFromAngle(-visualAngle / 2f) * visualRadius);
        Gizmos.DrawLine(transform.position,
            transform.position + DirFromAngle(visualAngle / 2f) * visualRadius);

        Gizmos.color = new Color(1f, 0f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusClose);

        Gizmos.color = new Color(1f, 0.5f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusMedium);

        Gizmos.color = new Color(1f, 1f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusFar);

        if (Application.isPlaying && currentState == chaseState && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }

        if (Application.isPlaying && currentState == searchState)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(lastSeenPosition, searchRadius);
        }

#if UNITY_EDITOR
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * 2.5f,
            $"[ {currentState?.GetType().Name} ]"
        );
#endif
    }

    Vector3 DirFromAngle(float angleDeg)
    {
        angleDeg += transform.eulerAngles.y;
        return new Vector3(
            Mathf.Sin(angleDeg * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleDeg * Mathf.Deg2Rad)
        );
    }
}