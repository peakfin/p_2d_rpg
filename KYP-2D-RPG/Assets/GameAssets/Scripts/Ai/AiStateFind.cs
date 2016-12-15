using UnityEngine;
using System.Collections;

public class AiStateFind : AiState {
    public float Sight;
    Rigidbody2D Body;
    override public void OnEnter()
    {
        StayTime = float.MaxValue;
        if(AiCompnent !=null) AiCompnent.Target = null;
        Body = GetComponent<Rigidbody2D>();

        base.OnEnter();
    }

    public override bool OnStay()
    {
        Vector3 pos = transform.position;
        float factor = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        pos.x += factor;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.right);
        if(hit.collider != null)
        {
            string hitTag = hit.collider.gameObject.tag;
            if (hitTag == "Player" && hit.distance < Sight)
            {
                AiCompnent.Target = hit.collider.gameObject;
                return true;
            }
        }

        pos = transform.position;
        pos.x -= factor;
        hit = Physics2D.Raycast(pos, Vector2.left);
        if (hit.collider != null)
        {
            string hitTag = hit.collider.gameObject.tag;
            if (hitTag == "Player" && hit.distance < Sight)
            {
                AiCompnent.Target = hit.collider.gameObject;
                return true;
            }
        }

        //Debug.Log("AiStateFind Stay");
        return base.OnStay();
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
