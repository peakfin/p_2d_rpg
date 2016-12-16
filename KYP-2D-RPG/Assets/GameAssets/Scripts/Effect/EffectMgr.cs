using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectMgr : MonoBehaviour {

    public static EffectMgr Instance = null;
    public GameObject Effects;

    public enum EFFECT_TYPE
    {
        EFFECT_TYPE_FONT_MISS,
        EFFECT_TYPE_FONT_NORMAL,
        EFFECT_TYPE_FONT_CRITICAL,
        EFFECT_NORMAL_ATTACK,
        EFFECT_CRITICAL_ATTACK,
    }

    public struct EffectData
    {
        public double number;
        public string text;
    }

	// Use this for initialization
	void Start () {
	    if (Instance == null)
        {
            Init();
            Instance = this;
        }
	}

    void Init()
    {
        //여기서 이팩트들을 미로 로딩하고 풀링까지 하면 좋겠다.
    }

    // Update is called once per frame
    void Update () {
	    for(int i = 0; i < Effects.transform.childCount; i++)
        {
            GameObject obj = Effects.transform.GetChild(i).gameObject;
            if (obj.tag != "ParticleEffect")
            {
                continue;
            }

            ParticleSystem ptc = obj.GetComponent<ParticleSystem>();
            if (ptc.time >= ptc.duration)
            {
                Destroy(obj);
            }
        }
	}

    public void GenerateEffect(EFFECT_TYPE type, Vector3 pos = new Vector3(), EffectData data = new EffectData())
    {
        switch(type)
        {
            case EFFECT_TYPE.EFFECT_TYPE_FONT_MISS:
                GenerateMissEffect(pos);
                break;
            case EFFECT_TYPE.EFFECT_TYPE_FONT_NORMAL:
                GenerateNormalNumEffect(pos, data.number);
                break;
            case EFFECT_TYPE.EFFECT_TYPE_FONT_CRITICAL:
                GenerateCriticalNumEffect(pos, data.number);
                break;
            case EFFECT_TYPE.EFFECT_CRITICAL_ATTACK:
                GenerateCriticalAttackEffect(pos);
                break;
            case EFFECT_TYPE.EFFECT_NORMAL_ATTACK:
                GenerateNormalAttackEffect(pos);
                break;
        }
    }
    void GenerateCriticalAttackEffect(Vector3 pos)
    {
        GenerateAttackEffect(pos, "Critical");
    }

    void GenerateNormalAttackEffect(Vector3 pos)
    {
        GenerateAttackEffect(pos, "Normal");
    }

    void GenerateAttackEffect(Vector3 pos, string path)
    {
        GameObject Prefab = Resources.Load("Effect/AttackEffect/" + path + "AttackEffect") as GameObject;

        GameObject Obj = Instantiate(Prefab) as GameObject;

        Obj.transform.parent = Effects.transform;
        Obj.tag = "ParticleEffect";
        Obj.transform.position = pos;
        
    }

    void GenerateMissEffect(Vector3 pos)
    {
        GameObject PrefabParent = Resources.Load("Effect/DamageFont/DamageFont") as GameObject;
        GameObject PrefabChild = Resources.Load("Effect/DamageFont/Children/MissFont") as GameObject;

        GameObject ObjParent = Instantiate(PrefabParent) as GameObject;
        GameObject ObjChild = Instantiate(PrefabChild) as GameObject;

        Vector3 scale = ObjChild.transform.localScale;

        ObjParent.transform.parent = Effects.transform;

        ObjChild.transform.parent = ObjParent.transform.FindChild("Animation");
        ObjChild.transform.localScale = scale;

        ObjParent.transform.position = pos;
        ObjChild.transform.localPosition = Vector3.zero;
    }

    void GenerateCriticalNumEffect(Vector3 pos, double number)
    {
        GenerateNum(pos, number, "Critical");
    }

    void GenerateNormalNumEffect(Vector3 pos, double number)
    {
        GenerateNum(pos, number, "Normal");
    }

    void GenerateNum(Vector3 pos, double number, string path)
    {
        GameObject PrefabParent = Resources.Load("Effect/DamageFont/DamageFont") as GameObject;
        GameObject PrefabChild = Resources.Load("Effect/DamageFont/Children/"+path+"Font/"+path+"NumFont") as GameObject;

        // 숫자를 역순으로 리스트에 넣음
        List<int> NumsLst = new List<int>();

        double _number = number;

        while (_number / 10 >= 1)
        {
            NumsLst.Add((int)(_number % 10));
            _number = _number / 10;
        }
        NumsLst.Add((int)(_number % 10));

        //숫자 프리팹 생성
        List<GameObject> PrefabNumLst = new List<GameObject>();

        for (int i = 0; i < NumsLst.Count; i++)
        {
            GameObject Prefab = Resources.Load("Effect/DamageFont/Children/"+path+"Font/" + NumsLst[i]) as GameObject;
            PrefabNumLst.Add(Prefab);
        }

        GameObject ObjParent = Instantiate(PrefabParent) as GameObject;
        GameObject ObjChild = Instantiate(PrefabChild) as GameObject;
        //숫자 오브잭트 생성
        List<GameObject> ObjNumLst = new List<GameObject>();
        for (int i = 0; i < PrefabNumLst.Count; i++)
        {
            GameObject Obj = Instantiate(PrefabNumLst[i]) as GameObject;
            ObjNumLst.Add(Obj);
        }

        //
        Vector3 scale = ObjChild.transform.localScale;

        ObjParent.transform.parent = Effects.transform;

        ObjChild.transform.parent = ObjParent.transform.FindChild("Animation");
        ObjChild.transform.localScale = scale;

        ObjParent.transform.position = pos;
        ObjChild.transform.localPosition = Vector3.zero;

        float term = 0.65f;
        for (int i = 0; i < ObjNumLst.Count; i++)
        {
            scale = ObjNumLst[i].transform.localScale;
            ObjNumLst[i].transform.parent = ObjChild.transform;
            ObjNumLst[i].transform.localScale = scale;
            Vector3 npos = Vector3.zero;
            int cnt = ObjNumLst.Count;
            if (cnt % 2 == 0) cnt -= 1;
            npos.x = term * cnt / 2 - term * i;
            ObjNumLst[i].transform.localPosition = npos;
        }
    }
}
