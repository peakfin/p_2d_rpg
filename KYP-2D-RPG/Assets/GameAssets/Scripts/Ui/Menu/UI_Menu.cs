using UnityEngine;
using System.Collections;

public class UI_Menu : MonoBehaviour {

    public GameObject UI_Status;
    public GameObject UI_Enchant;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenUI(string name)
    {
        switch(name)
        {
            case "status":
                UI_Status.SetActive(true);
                break;
            case "enchant":
                UI_Enchant.SetActive(true);
                break;
        }
    }
}
