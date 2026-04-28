using UnityEngine;

public class BossDieState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        StopBoss(boss);
        PlayDeathAnimation(boss);
    }

    public override void UpdateState(BossAIController boss) { }
    public override void ExitState(BossAIController boss) { }

    private void StopBoss(BossAIController boss)
    {
        boss.agent.ResetPath();
        boss.agent.velocity = Vector3.zero;
        boss.agent.isStopped = true;
    }

    private void PlayDeathAnimation(BossAIController boss)
    {
        boss.animatorBoss.applyRootMotion = true;
        boss.animatorBoss.SetBool("IsPosion", true);
    }
}