using UnityEngine;
using System.Collections;

public class UI_Enemy : UI_Background
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateEnemy()
    {

        EnemyGenerator.Instance.GenerateEnemys(false);
    }

    public void ShowAdsAndGenEnemy()
    {
        //todo show ads
        EnemyGenerator.Instance.GenerateEnemys(false, true);
    }

    public void LevelUpMonster()
    {
        long monLv = UserDataMgr.Instance.MonsterLv;
        long exp = monLv * monLv * monLv * 10;
        for (int i = 1; ; i++)
        {
            if (monLv < i * 10)
            {
                exp /= i * 10;
                break;
            }
        }
        exp *= 5;
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
                    UserDataMgr.Instance.Exp -= exp;
                    UserDataMgr.Instance.MonsterLv += 1;
                    UserDataMgr.Instance.SaveData();
                    GeneralPopup.Instance.OpenPopup(
                        GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN,
                        "다음 몬스터 소환부터 몬스터가 강해집니다.",
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
