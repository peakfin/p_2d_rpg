using UnityEngine;
using System.Collections;

public class UI_Menu : MonoBehaviour {

    public GameObject UI_Status;
    public GameObject UI_Enchant;
    public GameObject UI_Enemy;
    public GameObject UI_Shop;
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
            case "enemy":
                UI_Enemy.SetActive(true);
                break;
            case "shop":
                UI_Shop.SetActive(true);
                break;
            case "save":
                GeneralPopup.Instance.OpenPopup(GeneralPopup.POPUP_STYLE.POPUP_STYLE_TWOBTN, "정말로 저장합니까?", () => { UserDataMgr.Instance.SaveData(); }, ()=> { });
                break;
        }
    }
}
