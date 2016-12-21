using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneralPopup : MonoBehaviour {
    public static GeneralPopup Instance;
    public enum POPUP_STYLE
    {
        POPUP_STYLE_ONEBTN,
        POPUP_STYLE_TWOBTN,
    }

    public GameObject OneBtnPopup;
    public GameObject TwoBtnPopup;

    public delegate void DelegateOk();
    public delegate void DelegateCancel();

    DelegateOk FuncOk;
    DelegateCancel FuncCancel;

    public Text TextOneBtn;
    public Text TextTwoBtn;

    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenPopup(POPUP_STYLE st, string text, DelegateOk delOk, DelegateCancel delCancel = null)
    {
        switch(st)
        {
            case POPUP_STYLE.POPUP_STYLE_ONEBTN:
                OneBtnPopup.SetActive(true);
                FuncOk = new DelegateOk(delOk);
                TextOneBtn.text = text;
                break;
            case POPUP_STYLE.POPUP_STYLE_TWOBTN:
                TwoBtnPopup.SetActive(true);
                FuncOk = new DelegateOk(delOk);
                FuncCancel = new DelegateCancel(delCancel);
                FuncCancel += () => { TwoBtnPopup.SetActive(false); };
                TextTwoBtn.text = text;
                break;
        }

        FuncOk += () => { (st == POPUP_STYLE.POPUP_STYLE_ONEBTN?OneBtnPopup:TwoBtnPopup).SetActive(false); };
    }

    public void CallOk()
    {
        FuncOk();
    }

    public void CallCancel()
    {
        FuncCancel();
    }
}
