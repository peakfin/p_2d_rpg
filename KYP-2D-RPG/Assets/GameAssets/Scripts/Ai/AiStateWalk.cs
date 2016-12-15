using UnityEngine;
using System.Collections;

public class AiStateWalk : AiState {
    public float Range;
    override public void OnEnter()
    {
        StayTime = float.MaxValue;
        base.OnEnter();
    }

    public override bool OnStay()
    {
        //Debug.Log("AiStateWalk Stay");
        GameObject target = AiCompnent.Target;

        float dist = Vector2.Distance(transform.position, target.transform.position);

        if (dist < Range)
        {
            StayTime = 0;
            Vector3 vel = Vector3.zero;
            GetComponent<Enemy>().Vel = vel;
        }
        else if (transform.position.x > target.transform.position.x)
        {
            //오른쪽 이동
            Vector3 vel = Vector3.zero;
            vel.x = -10;
            GetComponent<Enemy>().Vel = vel;
        }
        else
        {
            //왼쪽 이동
            Vector3 vel = Vector3.zero;
            vel.x = 10;
            GetComponent<Enemy>().Vel = vel;
        }
        return base.OnStay();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
