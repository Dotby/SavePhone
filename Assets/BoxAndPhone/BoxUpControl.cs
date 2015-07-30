using UnityEngine;
using System.Collections;

public class BoxUpControl : MonoBehaviour {

	public Animator manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpPhone(){

		//gameObject.GetComponent<Animator>().enabled = false;
		if (manager.enabled == false){
			manager.enabled = true;
		}
		gameObject.SetActive(false);
		manager.Play("up");
	}
}
