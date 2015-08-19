using UnityEngine;
using System.Collections;

public class EngineerControlAnim : MonoBehaviour {

	public Animator anim;
	public Animator animBody;

	public string nextAction = "";

	public GameObject smallPhone;

	public bool axe = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//anim.Play("idleOff");
		//animBody = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SmallPhoheVisible(int v){
		if (smallPhone == null) {return;}

		if (v == 0) {smallPhone.SetActive(false);}
		if (v == 1) {smallPhone.SetActive(true);}
	}

	public void PlaySnd(string sn){
		//animBody.GetComponent<BodyControl>().PlaySnd(sn);
	}

	public void PlayWater(){
		Debug.Log("PlayWater");
		animBody.Play("water");
	}

	public void PlayAxe(){
		Debug.Log("PlayAxe");
		animBody.Play("axe");
	}

	public void PlayAnim(string animName){
		//Debug.Log("PlayAxe");
		animBody.Play(animName);
	}

	public void PlayAction(){
		Debug.Log("PlayAction");
		animBody.Play(nextAction);
	}

	public void UpEnd(){
		//anim.enabled = true;
		Debug.Log("upend");
		PlayAction();
		//anim.speed = 0f;
	}

	public void DownEnd(){
		anim.Play("idleOff");
	
		//Hide();
		//anim.Play("idleOff");
		//anim.enabled = true;
		//anim.speed = 0f;
	}

	public void ShowAnim(string animName){

		//smallPhone.SetActive(false);
		//ShowAnim("idleoff");
		animBody.Play("idleoff");
		//anim.Play("idleOff");

		anim.enabled = true;
		anim.speed = 1f;
		anim.Play("show");
		nextAction = animName;
		Debug.Log("Play after: " + anim.GetCurrentAnimatorClipInfo(0).Length);
		//Invoke("PlayAnim", (float)anim.GetCurrentAnimatorClipInfo(0).Length);
	}

	public void HideForce(){
		animBody.Play("idleoff");
		//anim.enabled = true;
	}

	public void Hide(){
		//anim.enabled = false;
		//anim.speed = 1f;

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("show") == true) {Debug.Log("NOOOO"); return;}
		anim.Play("hide");
	}
}
