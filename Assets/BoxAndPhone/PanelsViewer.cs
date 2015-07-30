using UnityEngine;
using System.Collections;

public class PanelsViewer : MonoBehaviour {

	public GameObject[] panels;
	int panNum = -1;

	public int mode = -1;
	int lastMode = 0;
	public GameObject ingPan;

	GameObject activePan = null;
	GameObject prevPan = null;

	public bool first = true;

	public GameObject tip;

	public Animator animPhone;
	public Animator boxUp;
	public Controller _CONTROLLER;

	public bool visible = false;

	// Use this for initialization
	void Start () {
		StopAnimation();
		foreach(GameObject gm in panels){gm.SetActive(false);}
		ingPan.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void HideAll(){
		ShowTip();
		if (prevPan != null) {prevPan.SetActive(false);}
		ingPan.SetActive(false);
		lastMode = mode;
		mode = 0;
		visible = false;
	}

	public void StopAnimation(){
		animPhone.speed = 0;
	}

	public void ShowAll(){

		visible = true;

		if (first == true && mode == 2){
			first = false;

			animPhone.speed = 1;
			//animPhone.Play("upphone");
			boxUp.Play("upme");
		}

		if (first == true && mode == 1){
			first = false;
			
			animPhone.speed = 1;
			//animPhone.Play("upphone");
			//boxUp.Play("upme");
		}

		HideTip();
		mode = lastMode;

		if (mode == 1){
			foreach(GameObject gm in panels){gm.SetActive(false);}
			ingPan.SetActive(true);
		}

		if (mode == 2){
			//prevPan.SetActive(true);
			Invoke("Next", 3f);
		}
	}

	public void HideTip(){
		tip.GetComponent<Animator>().Play("hide");
	}

	public void ShowTip(){
		tip.GetComponent<Animator>().Play("show");
	}

	public void IngMode(){
		if (mode == 1){return;}

		first = true;

		if (visible == true){
			mode = 1;
			foreach(GameObject gm in panels){gm.SetActive(false);}
			boxUp.gameObject.SetActive(false);
			ingPan.SetActive(true);
		}else{
			lastMode = 1;
		}
	}

	public void UserMode(){
		if (mode == 2){return;}

		first = true;

		if (first == true){
			first = false;
			animPhone.speed = 1;
			boxUp.Play("upme");
		}

		if (visible == true){
			mode = 2;
			foreach(GameObject gm in panels){gm.SetActive(false);}
			ingPan.SetActive(false);
		}else{
			lastMode = 2;
			ingPan.SetActive(false);
		}

		int panNum = 0;
		panels[panNum].SetActive(true);
		//activePan = panels[panNum];
		Invoke("Next", 3f);
	}

	void Next()
	{
		if (mode == 1 || mode == 0) {return;}

		ingPan.SetActive(false);

		if (prevPan != null){
			prevPan.SetActive(false);
			}

		//_CONTROLLER.SetModeTo(1);

		if (panNum +1 < panels.Length){
			panNum++;
		}else{
			panNum = 0;
		}

		//activePan = panels[panNum];
		panels[panNum].SetActive(true);
		prevPan = panels[panNum];
		prevPan.GetComponent<BillboardPanel>().pointPos.GetComponent<LineDrawer>().Create();
		Invoke("Next", 3f);
	}
}
