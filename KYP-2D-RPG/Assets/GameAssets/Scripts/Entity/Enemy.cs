using UnityEngine;
using System.Collections;

public class Enemy : Entity
{
    public bool OnAttack = false;
    Ai ai;
    public long Exp;
    // Use this for initialization
    void Start()
    {
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

        ai = GetComponent<Ai>();
        InitBattleValue();
    }

    void InitBattleValue()
    {
        //레벨에따른 능력치 여기서 셋팅
        BattleValue bv = GetComponent<BattleValue>();
        long lv = UserDataMgr.Instance.MonsterLv;
        bv.Lv = lv;
        bv.MaxHp = 500 + (lv - 1) * 10;
        bv.CurHp = bv.MaxHp;
        bv.Atk = 50 + (lv - 1) * 10;
        bv.Def = (lv - 1) * 5;
        bv.CriRatio = (lv - 1) * 1;
        bv.CriDmgRatio = 1 + (lv - 1) * 0.1f;
        bv.CriRatio = (lv - 1) * 0.1f;
        bv.Hit = 70 + (lv - 1) * 1;
        bv.Dot = (lv - 1) * 0.5f;
        Exp = lv * lv * lv * 10;
        for(int i = 1; ;i++)
        {
            if (lv < i * 10)
            {
                Exp /= i * 10;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ai != null && !IsDie()) ai.Process();

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
        }
        else if (Vel.y != 0)
        {
            UpdateAnimJump();
        }
        else if (Vel.x != 0)
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

    override protected void Die()
    {
        if(GetComponent<Rigidbody2D>() == null ||
            GetComponent<CircleCollider2D>() == null ||
            GetComponent<Ai>() == null)
        {
            return;
        }

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<Ai>());
        if (transform.FindChild("HpBack") != null)
        {
            GameObject HpBar = transform.FindChild("HpBack").gameObject;
            HpBar.SetActive(false);
        }

        UserDataMgr.Instance.Exp += Exp;
        EnemyGenerator.Instance.RemoveMonster(transform.parent.name);

    }
}
