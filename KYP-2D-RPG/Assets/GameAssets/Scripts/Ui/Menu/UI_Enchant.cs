using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Enchant : UI_Background
{
    public Text TextNext;
    public Text CurExp;
    public Text NeedExp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TextNext.text = string.Format("+{0}", UserDataMgr.Instance.Enchant + 1);
        CurExp.text = UserDataMgr.Instance.Exp.ToString();
        NeedExp.text = GetNeedExp().ToString();
	}

    long GetNeedExp()
    {
        return UserDataMgr.Instance.EnchentExp; ;
    }



    public void EnchantSowrd()
    {
        long exp = UserDataMgr.Instance.EnchentExp;
        //exp = 0;
     
        GeneralPopup.Instance.OpenPopup(
            GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN,
            string.Format("경험치가 {0} 소모됩니다.강화는 실패할 확률이 있습니다. 실패하면 강화 단계는 유지되나 경험치는 손실됩니다.", exp),
            () => {
                if (UserDataMgr.Instance.Exp < exp)
                {
                    GeneralPopup.Instance.OpenPopup(
                        GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                        "경험치가 부족합니다.",
                        () => { }
                    );
                }
                else
                {
                    long max = (UserDataMgr.Instance.Enchant + 1);
                    max *= max;
                    max *= max;
                    max *= max;
                    UserDataMgr.Instance.Exp -= exp;
                    if (Random.Range(0, max) <= 3)
                    {
                        UserDataMgr.Instance.Enchant += 1;
                        GeneralPopup.Instance.OpenPopup(
                            GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                            "강화 성공!",
                            () => { }
                        );
                    }
                    else
                    {
                        GeneralPopup.Instance.OpenPopup(
                            GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                            "강화 실패....",
                            () => { }
                        );
                    }
                    UserDataMgr.Instance.SaveData();

                }
            },
            () =>
            {

            }
        );
    }
}
