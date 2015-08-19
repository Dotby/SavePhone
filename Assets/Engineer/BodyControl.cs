using UnityEngine;
using System.Collections;

public class BodyControl : MonoBehaviour {

	EngineerControlAnim man;

	public AudioClip[] snds;

	// Use this for initialization 
	void Start () {
		gameObject.GetComponent<Animator>().Play("idleoff");
		man = transform.parent.gameObject.GetComponent<EngineerControlAnim>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySnd(string name){
		Debug.Log("Try play: " + name);
		foreach(AudioClip ac in snds){
			if (ac.name == name){
				AudioSource.PlayClipAtPoint(ac, Vector3.zero);
				return;
			}
		}
	}

	public void AnimEnd(){
		gameObject.GetComponent<Animator>().Play("idleoff");
		man.Hide();
	}
}
