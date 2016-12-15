using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ai : MonoBehaviour {
    public string[] ArrAiStateNames;
    public AiState[] ArrAiState;
    public Dictionary<string, AiState> DicAiState = new Dictionary<string, AiState>();

    public int CurStateIdx = 0;
    public int NextStateIdx = 0;
    int PreStateIdx = 0;

    public GameObject Target = null;

    public void Process()
    {
        if(ArrAiState[CurStateIdx].OnStay())
        {
            UpdateState();
        }
    }

    void UpdateState()
    {
        PreStateIdx = CurStateIdx;
        CurStateIdx = NextStateIdx;

        ArrAiState[PreStateIdx].OnLeave();
        ArrAiState[CurStateIdx].OnEnter();
    }
    // Start Update 함수 안쓴다.

    // Use this for initialization
    void Start()
    {
        ArrAiState[CurStateIdx].OnEnter();
    }
    // Update is called once per frame
    void Update()
    {

    }


}
