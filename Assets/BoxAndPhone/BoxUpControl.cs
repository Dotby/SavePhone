using UnityEngine;
using System.Collections;

public class BoxUpControl : MonoBehaviour {

	public Animator manager;
	public PanelsViewer pviewer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpPhone(){

		//gameObject.GetComponent<Animator>().enabled = false;

		if (pviewer.scenePart == 2){

		if (manager.enabled == false){
			manager.enabled = true;
		}

			gameObject.SetActive(false);
			manager.Play("up");
		}
		else{
			gameObject.SetActive(false);
			pviewer.Next();
		}
	}
}
