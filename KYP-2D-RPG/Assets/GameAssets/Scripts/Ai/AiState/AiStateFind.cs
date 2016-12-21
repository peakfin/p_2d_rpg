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
        BattleValue bv = GetComponent<BattleValue>();
        
        Vector2 dirVec = Vector2.zero;
        Vector3 pos = transform.position;
        float factor = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        if (gameObject.GetComponent<Entity>().Dir == Entity.DIR.DIR_LEFT)
        {
            dirVec = Vector2.left;
            pos.x -= factor;
        }
        else
        {
            dirVec = Vector2.right;
            pos.x += factor;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(pos, dirVec);
        if(hit.collider != null)
        {
            string hitTag = hit.collider.gameObject.tag;
            if (hitTag == "Player" && hit.distance < Sight)
            {
                AiCompnent.Target = hit.collider.gameObject;
                return true;
            }
        }

        if (bv.CurHp < bv.MaxHp)
        {
            pos = transform.position;
            if (gameObject.GetComponent<Entity>().Dir == Entity.DIR.DIR_LEFT)
            {
                dirVec = Vector2.right;
                pos.x += factor;
            }
            else
            {
                dirVec = Vector2.left;
                pos.x -= factor;
            }
            hit = Physics2D.Raycast(pos, dirVec);
            if (hit.collider != null)
            {
                string hitTag = hit.collider.gameObject.tag;
                if (hitTag == "Player" && hit.distance < Sight)
                {
                    AiCompnent.Target = hit.collider.gameObject;
                    return true;
                }
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
