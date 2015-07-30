using UnityEngine;
using System.Collections;

public class MobileAnimControl : MonoBehaviour {

	public GameObject manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpEnd(){
		Debug.Log("UP end");
		manager.GetComponent<PanelsViewer>().PhoneUpped();
	}
}
