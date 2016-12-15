using UnityEngine;
using System.Collections;

public class AiStateIdle : AiState
{
    // Start Update 함수 안쓴다.
    override public void OnEnter()
    {
        base.OnEnter();
    }

    public override bool OnStay()
    {
        //Debug.Log("AiStateIdle Stay");
        return base.OnStay();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
