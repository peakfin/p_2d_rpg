using UnityEngine;
using System.Collections;

public class BattleValue : MonoBehaviour {

    public double MaxHp; //최대 체력
    public double CurHp; //현 체력


    //크리티컬은 CriRatio - RegCriRatio 확률로계산한다.
    //데미지는 Atk - Def 계산 후 크리티컬시 CriDmgRatio 를 곱한다.
    public double Atk; // 공격력
    public float CriRatio; // 크리티컬 확률 %
    public float CriDmgRatio; // 크리티컬 데미지율 공격력에 곱해서 사용한다
    public double Def; // 방어력
    public float RegCriRatio; // 크리티컬 저항 확률 %


    // 회피확률은 Hit-Dot 로 처리한다.
    public float Dot; // 회피율 %
    public float Hit; // 명중률 %

    public GameObject HpBar;

    float FullHpScale = 0;
    float FullHpPos = 0;

	// Use this for initialization
	void Start () {
        if (HpBar != null)
        {
            FullHpScale = HpBar.transform.localScale.x;
            FullHpPos = HpBar.transform.localPosition.x;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (HpBar != null)
        {
            if (CurHp < 0) CurHp = 0;
            Vector3 scale = HpBar.transform.localScale;
            double factor = CurHp / MaxHp;
            scale.x = FullHpScale * (float)factor;
            HpBar.transform.localScale = scale;

            Vector3 pos = HpBar.transform.localPosition;
            pos.x = FullHpPos - (1 - (float)factor) * 2.6f;
            HpBar.transform.localPosition = pos;
        }

    }
}
