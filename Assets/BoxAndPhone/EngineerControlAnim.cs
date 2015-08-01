using UnityEngine;
using System.Collections;

public class EngineerControlAnim : MonoBehaviour {

	Animator anim;
	public Animator animBody;

	public bool axe = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play("idleOff");
		//animBody = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayWater(){
		Debug.Log("PlayWater");
		animBody.Play("water");
	}

	public void PlayAxe(){
		Debug.Log("PlayAxe");
		animBody.Play("axe");
	}

	public void UpEnd(){
		//anim.enabled = true;
		Debug.Log("upend");
		if (axe == true){
			PlayAxe();
		}else{
			PlayWater();
		}
		//anim.speed = 0f;
	}

	public void DownEnd(){
		//anim.enabled = true;
		anim.speed = 0f;
	}

	public void Show(){
		anim.enabled = true;
		anim.speed = 1f;
		anim.Play("show");
	}

	public void Hide(){
		anim.enabled = true;
		anim.speed = 1f;
		anim.Play("hide");
	}
}
