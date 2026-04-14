using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoarState : BossBaseState
{
    private const string IS_ROARING = "IsRoaring";
    public override void EnterState(BossAIController boss)
    {
        boss.animatorBoss.SetBool(IS_ROARING, true);
        boss.bossSound.PlayRoar();
    }

    public override void UpdateState(BossAIController boss)
    {
        
    }
    public override void ExitState(BossAIController boss)
    {
        boss.animatorBoss.SetBool(IS_ROARING, false);
    }
}
