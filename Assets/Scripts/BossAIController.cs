using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Patrol,
    Chase,
    Attack,
    Search,
    Stun
}

public class BossAIController : MonoBehaviour
{

    public Transform player;
    public NavMeshAgent agent;
    [SerializeField] private Animator animatorBoss;

    [SerializeField] private float visualRadius = 20f;
    [SerializeField] private float visualAngle = 200f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float attackRange = 2f;


    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    private int currentWayPointIndex = 0;
    private int pointDistance = 1;


    [SerializeField] private float lostPlayerWaitTime = 1f;
    private float lostPlayerTimer = 0f;


    [SerializeField] private float searchRadius = 4f;
    [SerializeField] private float searchDuration = 4f;
    [SerializeField] private float giveUpChance = 0.4f;
    private Vector3 lastSeenPosition;
    private float searchTimer = 0f;


    [SerializeField] private float hearRadiusClose = 5f;
    [SerializeField] private float hearRadiusMedium = 12f;
    [SerializeField] private float hearRadiusFar = 20f;
    [SerializeField] private float noiseThreshold = 0.3f;


    public BossState currentState = BossState.Patrol;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animatorBoss = GetComponent<Animator>();
    }

    //void OnEnable()
    //{
    //    SoundManager.Instance.OnSoundEmitted += HearSound;
    //}

    //void OnDisable()
    //{
    //    SoundManager.Instance.OnSoundEmitted -= HearSound;
    //}

    void Update()
    {
        switch (currentState)
        {
            case BossState.Patrol: Patrol(); break;
            case BossState.Chase: Chase(); break;
            case BossState.Attack: Attack(); break;
            case BossState.Search: HandleSearch(); break;
            case BossState.Stun: HandleStun(); break;
        }
    }


    void Patrol()
    {
        if (wayPoints.Count == 0) return;

        animatorBoss.SetBool("IsWalking", true);
        animatorBoss.SetBool("IsRunning", false);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[currentWayPointIndex].position);
            currentWayPointIndex += pointDistance;

            if (currentWayPointIndex >= wayPoints.Count - 1)
            {
                currentWayPointIndex = wayPoints.Count - 1;
                pointDistance = -1;
            }
            else if (currentWayPointIndex <= 0)
            {
                currentWayPointIndex = 0;
                pointDistance = 1;
            }
        }

        if (CanSee())
        {
            lastSeenPosition = player.position;
            ChangeState(BossState.Chase);
        }
    }


    void Chase()
    {
        animatorBoss.SetBool("IsWalking", false);
        animatorBoss.SetBool("IsRunning", true);

        if (CanSee())
        {
            lostPlayerTimer = 0f;
            lastSeenPosition = player.position;
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                animatorBoss.SetBool("IsRunning", false);
                ChangeState(BossState.Attack);
            }
        }
        else
        {
            lostPlayerTimer += Time.deltaTime;

            if (lostPlayerTimer >= lostPlayerWaitTime)
            {
                lostPlayerTimer = 0f;
                animatorBoss.SetBool("IsRunning", false);
                ChangeState(BossState.Search);
            }
        }
    }

    void Attack()
    {

    }

    void HandleSearch()
    {
        animatorBoss.SetBool("IsWalking", true);
        searchTimer += Time.deltaTime;

        // Thấy player → Chase ngay
        if (CanSee())
        {
            searchTimer = 0f;
            ChangeState(BossState.Chase);
            return;
        }

        // Hết giờ → 40% bỏ / 60% tìm tiếp
        if (searchTimer >= searchDuration)
        {
            searchTimer = 0f;

            if (Random.Range(0f, 1f) < giveUpChance)
            {
                animatorBoss.SetBool("IsWalking", false);
                ChangeState(BossState.Patrol);
            }
            else
            {
                agent.SetDestination(GetRandomSearchPoint());
            }
            return;
        }

        // Đến điểm → random điểm mới
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(GetRandomSearchPoint());
        }
    }

    Vector3 GetRandomSearchPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * searchRadius;
        randomDir.y = 0;
        Vector3 searchTarget = lastSeenPosition + randomDir;

        if (NavMesh.SamplePosition(searchTarget, out NavMeshHit hit, searchRadius, NavMesh.AllAreas))
            return hit.position;

        return lastSeenPosition;
    }

    // ============================================================
    // STUN
    // ============================================================
    void HandleStun()
    {
        agent.ResetPath();
    }

    public void Stun(float duration)
    {
        if (currentState == BossState.Stun) return;

        ChangeState(BossState.Stun);
        agent.ResetPath();
        animatorBoss.SetTrigger("Stun");
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        animatorBoss.ResetTrigger("Stun");
        ChangeState(BossState.Patrol);
    }

    // ============================================================
    // SOUND DETECTION
    // ============================================================
    void HearSound(float noiseValue, Vector3 soundPosition)
    {
        if (noiseValue < noiseThreshold) return;
        if (currentState == BossState.Stun) return;
        if (currentState == BossState.Chase) return; // Đang đuổi rồi

        float dist = Vector3.Distance(transform.position, soundPosition);

        if (dist <= hearRadiusClose)
            ReactClose(soundPosition);
        else if (dist <= hearRadiusMedium)
            ReactMedium(soundPosition);
        else if (dist <= hearRadiusFar)
            ReactFar(soundPosition);
    }

    void ReactClose(Vector3 pos)
    {
        lastSeenPosition = pos;
        agent.SetDestination(pos);
        ChangeState(BossState.Search);
    }

    void ReactMedium(Vector3 pos)
    {
        lastSeenPosition = pos;

        Vector3 offset = Random.insideUnitSphere * 3f;
        offset.y = 0;
        Vector3 target = pos + offset;

        if (NavMesh.SamplePosition(target, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
        else
            agent.SetDestination(pos);

        ChangeState(BossState.Search);
    }

    void ReactFar(Vector3 pos)
    {
        if (Random.Range(0f, 1f) <= 0.6f)
        {
            lastSeenPosition = pos;
            agent.SetDestination(pos);
            ChangeState(BossState.Search);
        }
    }

    // ============================================================
    // CHANGE STATE
    // ============================================================
    void ChangeState(BossState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }

    // ============================================================
    // CAN SEE
    // ============================================================
    bool CanSee()
    {
        if (player == null) return false;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > visualRadius) return false;

        Vector3 dir = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dir) > visualAngle / 2f) return false;

        if (Physics.Raycast(transform.position, dir, dist, obstacleLayer)) return false;

        return true;
    }

    // ============================================================
    // GIZMOS
    // ============================================================
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

        // 3 vùng nghe
        Gizmos.color = new Color(1f, 0f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusClose);

        Gizmos.color = new Color(1f, 0.5f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusMedium);

        Gizmos.color = new Color(1f, 1f, 0f, 0.08f);
        Gizmos.DrawWireSphere(transform.position, hearRadiusFar);

        if (Application.isPlaying && currentState == BossState.Chase && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }

        if (Application.isPlaying && currentState == BossState.Search)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(lastSeenPosition, searchRadius);
        }

#if UNITY_EDITOR
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * 2.5f,
            $"[ {currentState} ]"
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