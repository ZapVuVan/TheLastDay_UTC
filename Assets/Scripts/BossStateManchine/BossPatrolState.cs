public class BossPatrolState : BossBaseState
{
    private int pointDistance = 0;
    public override void EnterState(BossAIController boss)
    {
        boss.animatorBoss.SetBool("IsWalking", true);
        boss.animatorBoss.SetBool("IsRunning", false);

        if (boss.wayPoints.Count > 0)
            boss.agent.SetDestination(boss.wayPoints[boss.currentWayPointIndex].position);
    }

    public override void UpdateState(BossAIController boss)
    {
        if (boss.wayPoints.Count == 0) return;

        if (!boss.agent.pathPending && boss.agent.remainingDistance <= boss.agent.stoppingDistance)
        {
            boss.currentWayPointIndex += boss.pointDistance;

            if (boss.currentWayPointIndex >= boss.wayPoints.Count - 1)
            {
                boss.currentWayPointIndex = boss.wayPoints.Count - 1;
                boss.pointDistance = -1;
            }
            else if (boss.currentWayPointIndex <= 0)
            {
                boss.currentWayPointIndex = 0;
                boss.pointDistance = 1;
            }

            boss.agent.SetDestination(boss.wayPoints[boss.currentWayPointIndex].position);
            pointDistance++;
            if(pointDistance >= 3)
            {
                boss.bossSound.PlayRoar();
                pointDistance = 0;
            }
        }

        if (boss.CanSee())
        {
            boss.lastSeenPosition = boss.player.position;
            boss.ChangeState(boss.chaseState);
        }
    }

    public override void ExitState(BossAIController boss) { }
}