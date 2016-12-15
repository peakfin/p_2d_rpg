using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public enum DIR
    {
        DIR_RIGHT,
        DIR_LEFT,
    }

    public Vector3 Vel;

    public DIR Dir;

    protected Animator Anim;
   
    public float Speed;

    protected GameObject AttackBox = null;

    // Use this for initialization
    void Start () {
   
    }
	// Update is called once per frame
	void Update () {

	}

    //void LateUpdate()
    //{

    //}

    protected void UpdateDir()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (body == null || renderer == null) return;

        if (body.velocity.x > 0)
        {
            Dir = DIR.DIR_RIGHT;
        }
        else if (body.velocity.x < 0)
        {
            Dir = DIR.DIR_LEFT;
        }

        if (AttackBox != null)
        {
            float factor = 1;
            if (Dir == DIR.DIR_LEFT) factor = -1;
            Vector2 offset = AttackBox.GetComponent<BoxCollider2D>().offset;
            offset.x = Mathf.Abs(offset.x) * factor;
            AttackBox.GetComponent<BoxCollider2D>().offset = offset;
        }

        if (Dir == DIR.DIR_RIGHT)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }


    protected void UpdateVel()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if (body == null) return;

        Vector3 vel = Vel;
        Vector3 curVel = body.velocity;

        if (vel.x > 0) vel.x = Speed;
        else if (vel.x < 0) vel.x = Speed * -1;

        if (curVel.y < 0)
        {
            vel.y = -1f;
        }
        else if (vel.y > 0)
        {
            vel.y = Speed * 3;
        }

        

        body.velocity = vel;
    }

    protected bool IsDie()
    {
        BattleValue bv = GetComponent<BattleValue>();

        if(bv != null && bv.CurHp <= 0)
        {
            return true;
        }
        return false;
    }

    protected virtual void Die()
    {
        
    }
}
