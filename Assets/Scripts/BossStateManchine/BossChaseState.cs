using UnityEngine;

public class BossChaseState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        boss.animatorBoss.SetBool("IsWalking", false);
        boss.animatorBoss.SetBool("IsRunning", true);
    }

    public override void UpdateState(BossAIController boss)
    {
        if (boss.activeRadio != null)
        {
            boss.agent.SetDestination(boss.activeRadio.transform.position);

            if (!boss.agent.pathPending &&
                boss.agent.remainingDistance <= boss.agent.stoppingDistance)
            {
                boss.lastSeenPosition = boss.activeRadio.transform.position;
                boss.ClearActiveRadio();
                boss.ChangeState(boss.searchState);
            }
            return;
        }

        if (boss.CanSee())
        {
            Debug.Log("Player spotted! Chasing...");
            boss.lostPlayerTimer = 0f;
            boss.lastSeenPosition = boss.player.position;
            boss.agent.SetDestination(boss.player.position);

            float dist = Vector3.Distance(boss.transform.position, boss.player.position);
            Debug.Log($"Distance to player: {dist} | Attack range: {boss.attackRange}");

            if (dist <= boss.attackRange)
            {
                boss.ChangeState(boss.attackState);
                return;
            }

        }
        else
        {
            boss.lostPlayerTimer += Time.deltaTime;
            if (boss.lostPlayerTimer >= boss.lostPlayerWaitTime)
            {
                boss.lostPlayerTimer = 0f;
                boss.ChangeState(boss.searchState);
            }
        }
    }

    public override void ExitState(BossAIController boss)
    {
        boss.animatorBoss.SetBool("IsRunning", false);
    }
}