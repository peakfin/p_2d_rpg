using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomButton : Button
{
    GameObject UiController = null;
	// Update is called once per frame
	//test..
	void Update () {
        if (IsPressed())
        {
            WhilePressed();
        }
    }

    public void WhilePressed()
    {
        if(UiController == null)
        {
            UiController = GameObject.FindGameObjectWithTag("UI_Controller");
        }
        switch(name)
        {
            case "Left":
                UiController.GetComponent<AvatarController>().OnLeft();
                break;
            case "Right":
                UiController.GetComponent<AvatarController>().OnRight();
                break;
            case "Attack":
                UiController.GetComponent<AvatarController>().OnAttack();
                break;
            case "Jump":
                UiController.GetComponent<AvatarController>().OnJump();
                break;
        }
        //Move your guys
    }
}
