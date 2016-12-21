using UnityEngine;
using System.Collections;

public class UI_Shop : UI_Background {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowAdsAndFullHp()
    {
        //광고 보여줌

        GeneralPopup.Instance.OpenPopup(
            GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN,
            string.Format("광고를 보고 체력을 회복합니다."),
            () => {
                //광고 보여줌

                FullHp();
                
            },
            () =>
            {

            }
        );
    }

    public void FullHp()
    {

        long exp = UserDataMgr.Instance.FullHpExp;
        GeneralPopup.Instance.OpenPopup(
            GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN,
            string.Format("경험치가 {0} 소모됩니다.", exp),
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
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<BattleValue>().CurHp = UserDataMgr.Instance.MaxHp;
                    UserDataMgr.Instance.SaveData();
                    GeneralPopup.Instance.OpenPopup(
                        GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                        "체력이 모두 회복되었습니다.",
                        () => { }
                    );
                }
            },
            () =>
            {

            }
        );
    }
}
