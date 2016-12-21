using UnityEngine;
using System.Collections;

public class TextLayerSetter : MonoBehaviour {

    public string SortingLayerName;
    public int SortOrder;
	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        GetComponent<MeshRenderer>().sortingOrder = SortOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
