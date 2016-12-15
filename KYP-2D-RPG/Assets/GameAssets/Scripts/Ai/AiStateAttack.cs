using UnityEngine;
using System.Collections;

public class AiStateAttack : AiState {
    public float AttackDelay;
    override public void OnEnter()
    {
        StayTime = float.MaxValue;
        
        base.OnEnter();
    }

    public override bool OnStay()
    {
        //Debug.Log("AiStateAttack Stay");
        bool bAttack = GetComponent<Enemy>().OnAttack;
        if (AttackDelay < StateTime
            && !bAttack)
        {
            GetComponent<Enemy>().OnAttack = true;
        }
        else if (bAttack)
        {
            GetComponent<Enemy>().OnAttack = false;
            StayTime = 0;
        }
       

        return base.OnStay();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
