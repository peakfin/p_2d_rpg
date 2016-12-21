using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {
    [Serializable]
    public class JsonMonsterStatus
    {
        public List<string> MonsterPosList;
        public static JsonMonsterStatus CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<JsonMonsterStatus>(jsonString);
        }
        public string SaveToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
    public static EnemyGenerator Instance;
    public GameObject Enemys;

	// Use this for initialization
	void Start () {
        if (Instance != null) return;
        Instance = this;
        Init();
    }

    void Init()
    {
        GenerateEnemys();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateEnemys(bool isStart = true)
    {
        GameObject Prefab = Resources.Load("Entity/Enemys/Enemy_Knight5_Axe") as GameObject;
        JsonMonsterStatus data = JsonMonsterStatus.CreateFromJSON(UserDataMgr.Instance.MonsterStatus);
        if ((isStart && data == null) || !isStart)//data.MonsterPosList.Count == 0)
        {
            if(!isStart)
            {
                if (data.MonsterPosList.Count > 0)
                {
                    GeneralPopup.Instance.OpenPopup(GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN, "몬스터가 남아있습니다.", () => { });
                    return;
                }
                else
                {
                    long exp = UserDataMgr.Instance.MonGenNeedExp;

                    string str = string.Format("경험치가 {0} 소모됩니다.", exp);
                    GeneralPopup.Instance.OpenPopup(
                        GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN, 
                        str, 
                        () => {
                            if (exp > UserDataMgr.Instance.Exp)
                                GeneralPopup.Instance.OpenPopup(GeneralPopup.POPUP_STYLE.POPUP_STYLE_ONEBTN, "경험치가 부족합니다.", () => { });
                            else
                            {
                                data = new JsonMonsterStatus();
                                data.MonsterPosList = new List<string>();
                                for (int i = 0; i < Enemys.transform.childCount; i++)
                                {
                                    GameObject Obj = Instantiate(Prefab) as GameObject;

                                    Obj.transform.parent = Enemys.transform.GetChild(i);
                                    Obj.transform.position = Enemys.transform.GetChild(i).position;

                                    data.MonsterPosList.Add(Enemys.transform.GetChild(i).name);
                                }

                                UserDataMgr.Instance.MonsterStatus = data.SaveToString();
                                UserDataMgr.Instance.SaveData();
                            }
                        },
                        () => { }
                    );
                    return;
                }
            }
            data = new JsonMonsterStatus();
            data.MonsterPosList = new List<string>();
            for (int i = 0; i < Enemys.transform.childCount; i++)
            {
                GameObject Obj = Instantiate(Prefab) as GameObject;

                Obj.transform.parent = Enemys.transform.GetChild(i);
                Obj.transform.position = Enemys.transform.GetChild(i).position;

                data.MonsterPosList.Add(Enemys.transform.GetChild(i).name);
            }

            UserDataMgr.Instance.MonsterStatus = data.SaveToString();
            UserDataMgr.Instance.SaveData();

        }
        else if (isStart)
        {
            for (int i = 0; i < Enemys.transform.childCount; i++)
            {
                string strFind = data.MonsterPosList.Find(item => item == Enemys.transform.GetChild(i).name);
                if (strFind == null || strFind == "") continue;
                GameObject Obj = Instantiate(Prefab) as GameObject;

                Obj.transform.parent = Enemys.transform.GetChild(i);
                Obj.transform.position = Enemys.transform.GetChild(i).position;

                data.MonsterPosList.Add(Enemys.transform.GetChild(i).name);
            }
        }
    }

    public void RemoveMonster(string name)
    {
        JsonMonsterStatus data = JsonMonsterStatus.CreateFromJSON(UserDataMgr.Instance.MonsterStatus);
        if (data == null) return;

        data.MonsterPosList.Remove(name);
        UserDataMgr.Instance.MonsterStatus = data.SaveToString();
        UserDataMgr.Instance.SaveData();
    }
}
