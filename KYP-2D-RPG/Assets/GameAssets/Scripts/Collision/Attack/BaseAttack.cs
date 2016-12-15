using UnityEngine;
using System.Collections;

public class BaseAttack : MonoBehaviour {

    GameObject AtkObj;
	// Use this for initialization
	void Start () {
        AtkObj = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject DefObj = coll.gameObject;

        if (AtkObj == null || DefObj == null || AtkObj == DefObj) return;

        // 여기서 공격한 오브젝트와 공격당한 오브젝트의 계산등을 수행한다.

        BattleValue atkBv = AtkObj.GetComponent<BattleValue>();
        BattleValue defBv = DefObj.GetComponent<BattleValue>();

        if (atkBv == null || defBv == null) return;


        //check hit
        bool isHit = true;

        float hitVal = atkBv.Hit - defBv.Dot;
        float r = Random.Range(0, 101f);
        if (r > hitVal) isHit = false;

        if(!isHit)
        {
            // 여기서 미스 이팩트
            Debug.Log("miss");
            return;
        }

        //check critical
        bool isCri = true;

        float criVal = atkBv.CriRatio - defBv.RegCriRatio;
        r = Random.Range(0, 101f);
        if (r > criVal) isCri = false;

        //check damage
        double damage = atkBv.Atk - defBv.Def;

        if(isCri)
        {
            Debug.Log("Cri!");
            damage *= atkBv.CriDmgRatio;
        }

        //update hp
        defBv.CurHp -= damage;
        //여기서 데미지 이팩트 및 데미지 폰트 처리

        Debug.Log("attack!");
    }
}
