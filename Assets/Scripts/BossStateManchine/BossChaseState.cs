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
        if (boss.CanSee())
        {
            boss.lostPlayerTimer = 1f;
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