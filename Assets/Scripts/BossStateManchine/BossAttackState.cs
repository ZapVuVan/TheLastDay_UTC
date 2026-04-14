using UnityEngine;

public class BossAttackState : BossBaseState
{
    public override void EnterState(BossAIController boss)
    {
        boss.agent.ResetPath();
        // TODO: trigger animation attack
    }

    public override void UpdateState(BossAIController boss)
    {
        // Nếu player chạy ra ngoài range thì quay lại Chase
        if (Vector3.Distance(boss.transform.position, boss.player.position) > boss.attackRange)
            boss.ChangeState(boss.chaseState);
    }

    public override void ExitState(BossAIController boss) { }
}