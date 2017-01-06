using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UserDataMgr : MonoBehaviour {

    public Text TextEnchant;
    string[] ArrStrStatus = {
        "Lv",
        "Exp",
        "MaxHp",
        "CurHp",
        "Atk",
        "CriRatio",
        "CriDmgRatio",
        "Def",
        "RegCriRatio",
        "Dot",
        "Hit",
        "MonsterLv",
        "MonsterStatus",
        "Enchant"
    };

    public long AvatarNeedExp
    {
        get
        {
            long num = Lv * Lv * Lv * 100;
            int cnt = 10;
            for (int i = 1; ; i++)
            {
                if (Lv < cnt * i)
                {
                    num /= cnt * i;
                    break;
                }
            }
            return num;
        }
    }
    public long MonsterNeedExp
    {
        get
        {
            long monLv = MonsterLv;
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
            return exp;
        }
    }
    public long MonGenNeedExp
    {
        get
        {
            long monLv = MonsterLv;
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
            return exp;
        }
    }
    public long FullHpExp
    {
        get
        {
            long num = Lv * Lv * Lv * 100;
            int cnt = 10;
            for (int i = 1; ; i++)
            {
                if (Lv < cnt * i)
                {
                    num /= cnt * i;
                    break;
                }
            }
            return num / 2;
        }
    }

    public long EnchentExp
    {
        get
        {
            int lv = UserDataMgr.Instance.Enchant;
            long exp = (lv + 1) * (lv + 1) * (lv + 1) * 10;
            for (int i = 1; ; i++)
            {
                if (lv < i * 10)
                {
                    exp /= i * 10;
                    break;
                }
            }
            exp *= 15;
            return exp;
        }
    }


    static public UserDataMgr Instance = null;
   
    Dictionary<string, string> DicSaveData = new Dictionary<string, string>();

    public int Enchant
    {
        get
        {
            int ret = 0;
            if(int.TryParse(DicSaveData["Enchant"], out ret))
            {
                return ret;
            }
            return 0;
        }
        set
        {
            DicSaveData["Enchant"] = value.ToString();
        }
    }
    public long Lv
    {
        get
        {
            long ret = 1;
            if(long.TryParse(DicSaveData["Lv"], out ret))
            {
                return ret;
            }
            return 1;
        }
        set
        {
            DicSaveData["Lv"] = value.ToString();
        }
    }

    public long Exp
    {
        get
        {
            long ret = 0;
            if(long.TryParse(DicSaveData["Exp"], out ret))
            {
                return ret;
            }
            return 0;
        }
        set
        {
            DicSaveData["Exp"] = value.ToString();
        }
    }

    public double MaxHp {
        get
        {
            double ret = 1000;
            if(double.TryParse(DicSaveData["MaxHp"], out ret))
            {
                return ret;
            }
            return 1000;
        }
        set
        {
            DicSaveData["MaxHp"] = value.ToString();
        }
    } 
    //최대 체력
    public double CurHp
    {
        get
        {
            double ret = 1000;
            if (double.TryParse(DicSaveData["CurHp"], out ret))
            {
                return ret;
            }
            return 1000;
        }
        set
        {
            DicSaveData["CurHp"] = value.ToString();
        }
    } 
    //현 체력


    public double   Atk
    {
        get
        {
            double ret = 100;
            if (double.TryParse(DicSaveData["Atk"], out ret))
            {
                return ret;
            }
            return 100;
        }
        set
        {
            DicSaveData["Atk"] = value.ToString();
        }
    }
    public float    CriRatio
    {
        get
        {
            float ret = 10;
            if (float.TryParse(DicSaveData["CriRatio"], out ret))
            {
                return ret;
            }
            return 10;
        }
        set
        {
            DicSaveData["CriRatio"] = value.ToString();
        }
    }
    public float    CriDmgRatio
    {
        get
        {
            float ret = 1.1f;
            if (float.TryParse(DicSaveData["CriDmgRatio"], out ret))
            {
                return ret;
            }
            return 1.1f;
        }
        set
        {
            DicSaveData["CriDmgRatio"] = value.ToString();
        }
    }
    public double   Def
     {
        get
        {
            double ret = 0;
            if (double.TryParse(DicSaveData["Def"], out ret))
            {
                return ret;
            }
            return 0;
        }
        set
        {
            DicSaveData["Def"] = value.ToString();
        }
    }          
    public float    RegCriRatio
    {
        get
        {
            float ret = 0;
            if (float.TryParse(DicSaveData["RegCriRatio"], out ret))
            {
                return ret;
            }
            return 0;
        }
        set
        {
            DicSaveData["RegCriRatio"] = value.ToString();
        }
    }


    // 회피확률은 Hit-Dot 로 처리한다.
    public float Dot // 회피율 %
    {
        get
        {
            float ret = 20;
            if (float.TryParse(DicSaveData["Dot"], out ret))
            {
                return ret;
            }
            return 20;
        }
        set
        {
            DicSaveData["Dot"] = value.ToString();
        }
    }
    public float Hit // 명중률 %
    {
        get
        {
            float ret = 100;
            if (float.TryParse(DicSaveData["Hit"], out ret))
            {
                return ret;
            }
            return 100;
        }
        set
        {
            DicSaveData["Hit"] = value.ToString();
        }
    }

    public long MonsterLv
    {
        get
        {
            long ret = 1;
            if (long.TryParse(DicSaveData["MonsterLv"], out ret))
            {
                return ret;
            }
            return 1;
        }
        set
        {
            DicSaveData["MonsterLv"] = value.ToString();
        }
    }

    public string MonsterStatus
    {
        get
        {
            string ret = DicSaveData["MonsterStatus"];
            return ret;
        }
        set
        {
            DicSaveData["MonsterStatus"] = value.ToString();
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
    }
    // Use this for initialization
    void Start () {
	    
	}

    void Init()
    {
        //data load
        LoadData();
    }

    void LoadData()
    {
        //PlayerPrefs.DeleteAll();
        for (int i = 0; i < ArrStrStatus.Length; i++)
        {
            DicSaveData.Add(ArrStrStatus[i], PlayerPrefs.GetString(ArrStrStatus[i]));
        }
        Debug.Log(" [ KYP ] Load Data");
    }

    public void SaveData()
    {
        DicSaveData.ToString();
        for (int i = 0; i < ArrStrStatus.Length; i++)
        {
            PlayerPrefs.SetString(ArrStrStatus[i], DicSaveData[ArrStrStatus[i]]);
        }
        PlayerPrefs.Save();
        Debug.Log(" [ KYP ] Save Data");
    }
	
	// Update is called once per frame
	void Update () {
        TextEnchant.text = string.Format("+{0}", Enchant);
	}

    public void StatsUp(string status)
    {
        //성장치는 테이블관리로 변경하자
        switch (status)
        {
            case "MaxHp":
                MaxHp += 10;
                break;
            case "Atk":
                Atk += 10;
                break;
            case "Def":
                Def += 10;
                break;
            case "CriRatio":
                CriRatio += 1;
                break;
            case "CriDmgRatio":
                CriDmgRatio += 0.1f;
                break;
            case "RegCriRatio":
                RegCriRatio += 0.5f;
                break;
            case "Hit":
                Hit += 1;
                break;
            case "Dot":
                Dot += 1;
                break;
            default:
                Debug.Log("test");
                break;
        }
        Lv += 1;
        SaveData();
    }
}
