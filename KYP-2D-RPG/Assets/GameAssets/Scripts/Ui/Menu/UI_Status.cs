using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Status : UI_Background
{
    public Text TextLv;
    public Text TextNeedExp;
    public Text TextExp;
    public Text TextAtk;
    public Text TextDef;
    public Text TextCri;
    public Text TextCriDmg;
    public Text TextRegCri;
    public Text TextHit;
    public Text TextDot;
    public Text TextHp;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        TextLv.text = UserDataMgr.Instance.Lv.ToString();
        //TextNeedExp.text = UserDataMgr.Instance.Exp.ToString();
        long num = UserDataMgr.Instance.Lv * UserDataMgr.Instance.Lv * UserDataMgr.Instance.Lv * 100;
        int cnt = 10;
        for(int i = 1; ;i++)
        {
            if(UserDataMgr.Instance.Lv < cnt*i)
            {
                num /= cnt * i;
                break;
            }
        }
        TextNeedExp.text = num.ToString();
        TextExp.text = UserDataMgr.Instance.Exp.ToString();
        TextAtk.text = UserDataMgr.Instance.Atk.ToString();
        TextDef.text = UserDataMgr.Instance.Def.ToString();
        TextCri.text = UserDataMgr.Instance.CriRatio.ToString();
        TextCriDmg.text = UserDataMgr.Instance.CriDmgRatio.ToString();
        TextRegCri.text = UserDataMgr.Instance.RegCriRatio.ToString();
        TextHit.text = UserDataMgr.Instance.Hit.ToString();
        TextDot.text = UserDataMgr.Instance.Dot.ToString();
        TextHp.text = UserDataMgr.Instance.MaxHp.ToString();
    }

    public void StatusUp(string status)
    {
        //경험치 체크 여기서 한다.
        long needExp = 0;
        if(!long.TryParse(TextNeedExp.text, out needExp))
        {
            needExp = long.MaxValue;
        }
        if (UserDataMgr.Instance.Exp < needExp)
        {
            GeneralPopup.Instance.OpenPopup(
                GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                "경험치가 부족합니다.",
                () => { }
            );
            return;
        }

        string text = "";
        switch(status)
        {
            case "MaxHp":
                text = string.Format("체력을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.MaxHp, UserDataMgr.Instance.MaxHp + 10);
                break;
            case "Atk":
                text = string.Format("공격력을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.Atk, UserDataMgr.Instance.Atk + 10);
                break;
            case "Def":
                text = string.Format("방어력을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.Def, UserDataMgr.Instance.Def + 10);
                break;
            case "CriRatio":
                text = string.Format("치명을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.CriRatio, UserDataMgr.Instance.CriRatio + 1);
                break;
            case "CriDmgRatio":
                text = string.Format("크리티컬 데미지를 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.CriDmgRatio, UserDataMgr.Instance.CriDmgRatio + 0.1f);
                break;
            case "RegCriRatio":
                text = string.Format("크리티컬 저항을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.RegCriRatio, UserDataMgr.Instance.RegCriRatio + 0.5f);
                break;
            case "Hit":
                text = string.Format("명중을 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.Hit, UserDataMgr.Instance.Hit + 1);
                break;
            case "Dot":
                text = string.Format("회피를 {0}에서 {1}으로 올립니다.", UserDataMgr.Instance.Dot, UserDataMgr.Instance.Dot + 1);
                break;
            default:
                Debug.Log("test");
                break;
        }
        
        GeneralPopup.Instance.OpenPopup(
            GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN, 
            text, 
            () => {
                Debug.Log("UpUp");
                UserDataMgr.Instance.StatsUp(status);
                UserDataMgr.Instance.Exp -= needExp;
            }, 
            () => { Debug.Log("Cancel");  }
        );
    }
}
