using UnityEngine;

public class BossSearchState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        boss.searchTimer = 0f;
        boss.animatorBoss.SetBool("IsWalking", true);
        boss.agent.SetDestination(boss.GetRandomSearchPoint());
    }

    public override void UpdateState(BossAIController boss)
    {
        if (boss.CanSee())
        {
            boss.searchTimer = 0f;
            boss.ChangeState(boss.chaseState);
            return;
        }

        boss.searchTimer += Time.deltaTime;

        if (boss.searchTimer >= boss.searchDuration)
        {
            boss.searchTimer = 0f;

            if (Random.Range(0f, 1f) < boss.giveUpChance)
                boss.ChangeState(boss.patrolState);
            else
                boss.agent.SetDestination(boss.GetRandomSearchPoint());

            return;
        }

        if (!boss.agent.pathPending && boss.agent.remainingDistance <= boss.agent.stoppingDistance)
            boss.agent.SetDestination(boss.GetRandomSearchPoint());
    }

    public override void ExitState(BossAIController boss)
    {
        boss.animatorBoss.SetBool("IsWalking", false);
    }
}