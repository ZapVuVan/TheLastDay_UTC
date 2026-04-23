using UnityEngine;

public class BossDieState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        boss.agent.ResetPath();
        boss.agent.isStopped = true;
        boss.animatorBoss.SetBool("IsPosion", true);
    }

    public override void UpdateState(BossAIController boss) { }

    public override void ExitState(BossAIController boss) { }
}