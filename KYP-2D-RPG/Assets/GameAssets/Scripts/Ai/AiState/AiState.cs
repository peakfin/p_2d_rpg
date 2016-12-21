using UnityEngine;
using System.Collections;

public class AiState : MonoBehaviour {
    public float StayTime;
    protected float StateTime;
    protected BattleValue bv;
    protected Ai AiCompnent;

    public string[] ArrNextStateName;
    public int[] ArrNextStatePer;
    // Use this for initialization
    void Start()
    {
        bv = GetComponent<BattleValue>();
        AiCompnent = GetComponent<Ai>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnEnter()
    {
        StateTime = 0;
    }
    public virtual bool OnStay()
    {
        StateTime += Time.deltaTime;
        //Debug.Log("AiState Stay");
        if (StateTime >= StayTime) return true;

        return false;
    }

    public virtual void OnLeave()
    {
        int sum = 0;
        for(int i = 0; i < ArrNextStatePer.Length; i++)
        {
            sum += ArrNextStatePer[i];
        }

        int r = Random.Range(1, sum + 1);

        sum = 0;

        int nextStateIdx = 0;
        for(int i = 0; i < ArrNextStatePer.Length; i++)
        {
            sum += ArrNextStatePer[i];
            if (r < sum)
            {
                nextStateIdx = i;
                break;
            }
        }
        


        for (int i = 0; i < GetComponent<Ai>().ArrAiStateNames.Length; i++)
        {
            if (AiCompnent.ArrAiStateNames[i] == ArrNextStateName[nextStateIdx])
            {
                AiCompnent.NextStateIdx = i;
                break;
            }
        }
    }
}
