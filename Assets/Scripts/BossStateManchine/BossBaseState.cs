using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossAIController boss);
    public abstract void UpdateState(BossAIController boss);
    
    public abstract void ExitState(BossAIController boss);
}
