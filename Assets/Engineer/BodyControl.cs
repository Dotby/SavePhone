using UnityEngine;
using System.Collections;

public class BodyControl : MonoBehaviour {

	EngineerControlAnim man;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Animator>().Play("idleoff");
		man = transform.parent.gameObject.GetComponent<EngineerControlAnim>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimEnd(){
		gameObject.GetComponent<Animator>().Play("idleoff");
		man.Hide();
	}
}
