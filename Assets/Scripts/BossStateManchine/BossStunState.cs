public class BossStunState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        boss.agent.ResetPath();
        boss.animatorBoss.SetTrigger("Stun");
    }

    public override void UpdateState(BossAIController boss) { }

    public override void ExitState(BossAIController boss) { }
}