using UnityEngine;
using System.Collections;

public class UI_Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
