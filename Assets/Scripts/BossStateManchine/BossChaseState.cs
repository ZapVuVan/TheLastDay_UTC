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
        // Đang đến radio → ưu tiên tuyệt đối
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

        // Chase player bình thường
        if (boss.CanSee())
        {
            boss.lostPlayerTimer = 0f;
            boss.lastSeenPosition = boss.player.position;
            boss.agent.SetDestination(boss.player.position);

            if (Vector3.Distance(boss.transform.position, boss.player.position) <= boss.attackRange)
                boss.ChangeState(boss.attackState);
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