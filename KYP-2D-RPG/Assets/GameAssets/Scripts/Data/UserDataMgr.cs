using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UserDataMgr : MonoBehaviour {

    static UserDataMgr Instance = null;

    Dictionary<string, string> SavaData = new Dictionary<string, string>();

    public double MaxHp; //최대 체력
    public double CurHp; //현 체력

    //크리티컬은 CriRatio - RegCriRatio 확률로계산한다.
    //데미지는 Atk - Def 계산 후 크리티컬시 CriDmgRatio 를 곱한다.
    public double   Atk;            // 공격력
    public float    CriRatio;       // 크리티컬 확률 %
    public float    CriDmgRatio;    // 크리티컬 데미지율 공격력에 곱해서 사용한다
    public double   Def;            // 방어력
    public float    RegCriRatio;    // 크리티컬 저항 확률 %


    // 회피확률은 Hit-Dot 로 처리한다.
    public float Dot; // 회피율 %
    public float Hit; // 명중률 %

    // Use this for initialization
    void Start () {
	    if (Instance == null)
        {
            Instance = this;
            Init();
        }
	}

    void Init()
    {
        //data load

    }

    void LoadData()
    {
        if(!double.TryParse(PlayerPrefs.GetString("MaxHp"), out MaxHp))
        {
            MaxHp = 1000;
        }
        if(!double.TryParse(PlayerPrefs.GetString("CurHp"), out CurHp))
        {
            CurHp = 1000;
        }
        if (!double.TryParse(PlayerPrefs.GetString("Atk"), out Atk))
        {
            Atk = 100;
        }
        if (!float.TryParse(PlayerPrefs.GetString("CriRatio"), out CriRatio))
        {
            CriRatio = 5;
        }
        if (!float.TryParse(PlayerPrefs.GetString("CriDmgRatio"), out CriDmgRatio))
        {
            CriDmgRatio = 1.1f;
        }
        if (!double.TryParse(PlayerPrefs.GetString("Def"), out Def))
        {
            Def = 0;
        }
        if (!float.TryParse(PlayerPrefs.GetString("RegCriRatio"), out RegCriRatio))
        {
            RegCriRatio = 0;
        }
        if (!float.TryParse(PlayerPrefs.GetString("Dot"), out Dot))
        {
            Dot = 10;
        }

        if (!float.TryParse(PlayerPrefs.GetString("Hit"), out Hit))
        {
            Hit = 80;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
