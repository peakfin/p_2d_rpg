using UnityEngine;
using System.Collections;

public class Avatar : Entity
{
    public bool OnAttack = false;
   
    // Use this for initialization
    void Start() {
        AttackBox = transform.FindChild("Attack").gameObject;

        Vel = Vector3.zero;

        Anim = this.gameObject.GetComponent<Animator>();
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated

        PhysicsMaterial2D mat = new PhysicsMaterial2D();
        mat.bounciness = 0;
        mat.friction = 0;
        GetComponent<CircleCollider2D>().sharedMaterial = mat;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateVel();
        UpdateAnim();
        UpdateDir();
    }

    

    void UpdateAnim()
    {
        if (IsDie())
        {
            UpdateAnimDead();
            Die();
        }
        else if (OnAttack)
            //if (OnAttack)
        {
            UpdateAnimAttack();
            OnAttack = false;
        }
        else if(Vel.y != 0)
        {
            UpdateAnimJump();
        }
        else if(Vel.x != 0)
        {
            UpdateAnimWalk();
        }
        else
        {
            UpdateAnimIdle();
        }
       
    }

    void UpdateAnimAttack()
    {
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", true);//Shield animation is deactivated
        Anim.SetBool("idle", false);//Shield animation is deactivated
    }

    void UpdateAnimWalk()
    {
        Anim.SetBool("walk", true);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated
        Anim.SetBool("idle", false);//Shield animation is deactivated
    }

    void UpdateAnimDead()
    {
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", true);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated
        Anim.SetBool("idle", false);//Shield animation is deactivated
    }

    void UpdateAnimJump()
    {
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", true);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated
        Anim.SetBool("idle", false);//Shield animation is deactivated
    }

    void UpdateAnimShield()
    {
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", true);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated
        Anim.SetBool("idle", false);//Shield animation is deactivated
    }
    void UpdateAnimIdle()
    {
        Anim.SetBool("walk", false);//Walking animation is deactivated
        Anim.SetBool("dead", false);//Dying animation is deactivated
        Anim.SetBool("jump", false);//Jumping animation is deactivated
        Anim.SetBool("shield", false);//Shield animation is deactivated
        Anim.SetBool("attack", false);//Shield animation is deactivated
        Anim.SetBool("idle", true);//Shield animation is deactivated
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            bool isGround = false;
            for (int i = 0; i < coll.contacts.Length; i++)
            {
                //Debug.Log("i = " + i +" : " + coll.contacts[i].point.y);
                //Debug.Log(transform.position.y - coll.contacts[i].point.y);
                float hitTerm = transform.position.y - coll.contacts[i].point.y;
                float modelTerm = (GetComponent<CircleCollider2D>().radius - GetComponent<CircleCollider2D>().offset.y) * transform.localScale.y;

                float diff = hitTerm * 0.01f;

                isGround = Mathf.Abs(hitTerm - modelTerm) <= diff;
            }

            if (isGround)
                Vel.y = 0;
        }
    }
}
