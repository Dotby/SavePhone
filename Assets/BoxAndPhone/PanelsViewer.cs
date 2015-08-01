using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public EngineerControlAnim man;

	public bool visible = false;
	public Color onColor;
	Color offColor;

	public GameObject pointsPanel;
	Image[] stepPoints;
	public bool upped = false;
	// Use this for initialization
	void Start () {
		StopAnimation();
		foreach(GameObject gm in panels){gm.SetActive(false);}
		ingPan.SetActive(false);

		InitStepIcons();
	}

//	public void InitLines(){
//		Debug.Log("initLines");
//
//		LineDrawer[] lines = GameObject.FindObjectsOfType<LineDrawer>();
//		
//		foreach(LineDrawer line in lines){
//			Debug.Log("line" + line + " man = " + this);
//
//			line.manager = this;
//		}
//	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitStepIcons(){

		stepPoints = new Image[panels.Length];

		GameObject tmp = pointsPanel.transform.GetChild(0).gameObject;
		offColor = tmp.GetComponent<Image>().color;
		stepPoints[0] = tmp.GetComponent<Image>();

		for (int i = 1; i < panels.Length; i++){
			GameObject tmpNew = (GameObject)Instantiate(tmp as Object);
			tmpNew.transform.SetParent(pointsPanel.transform);
			tmpNew.transform.localScale = tmpNew.transform.lossyScale;
			stepPoints[i] = tmpNew.GetComponent<Image>();
		}
	}

	public void SetActivePoint(){
		foreach(Image img in stepPoints){
			img.color = offColor;
		}
		stepPoints[panNum].color = onColor;
	}
	
	public void HideAll(){
		ShowTip();
		animPhone.speed = 0f;
		boxUp.speed = 0f;
		if (prevPan != null) {prevPan.SetActive(false);}
		ingPan.SetActive(false);
		lastMode = mode;
		mode = 0;
		visible = false;
		//CancelInvoke();
	}


	public void ShowMan(){
		man.Show();
	}

	public void StopAnimation(){
		animPhone.speed = 0;
	}

	public void End(){
		animPhone.enabled = false;
	}

	public void PhoneUpped(){
		if (upped == false){
			upped = true;
		Debug.Log("upped");
			Debug.Log(animPhone.gameObject.transform.localRotation.eulerAngles);
			//animPhone.gameObject.transform.localRotation.eulerAngles = new Vector3(67.0f, 180.0f, 270.0f);
			animPhone.enabled = false;
			//animPhone.gameObject.transform.localRotation.eulerAngles = new Vector3(67.0f, 180.0f, 270.0f);
		//animPhone.Play("idleUp");
		//animPhone.speed = 1f;//////////////////
			////ТУТ БЫЛА ТРАБЛА РАНЬШЕ//////////////////////////////////////////////////////
		
		
		
		//animPhone.gameObject.transform.eulerAngles = Quaternion.Euler(67.0f, 180.0f, 270.0f).eulerAngles;
		//	Debug.Log("rotate" + animPhone.gameObject.transform.localRotation);
		//animPhone.gameObject.transform.localRotation = new
		foreach(GameObject gm in panels){gm.SetActive(true);}
			foreach(GameObject gm in panels){gm.SetActive(false);}
		//Invoke("Next", 1f);
		}

		///animPhone.gameObject.transform.localRotation.eulerAngles.Set(67.0f, 180.0f, 270.0f);
		Debug.Log(animPhone.gameObject.transform.localRotation.eulerAngles);
		//animPhone.enabled = false;
	}

	public void ShowAll(){

		animPhone.speed = 1f;
		boxUp.speed = 1f;

		visible = true;

		if (first == true && mode == 2){
			first = false;
			//Invoke("Next",5f);
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
			//Invoke("Next", 3f);
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
		panNum = 0;

		if (first == true){
			first = false;
			boxUp.enabled = true;
			animPhone.speed = 1;
			boxUp.Play("upme");

		}

		if (visible == true){
			mode = 2;
			//Invoke("Next", 3f);
			foreach(GameObject gm in panels){gm.SetActive(false);}
			ingPan.SetActive(false);
		}else{
			lastMode = 2;
			ingPan.SetActive(false);

		}

		//panels[panNum].SetActive(true);
		//activePan = panels[panNum];

	}

	public void Prev()
	{
		if (mode == 1 || mode == 0) {return;}
		
		ingPan.SetActive(false);
		
		if (prevPan != null){
			prevPan.SetActive(false);
		}
		
		//_CONTROLLER.SetModeTo(1);
		
		if (panNum - 1 > 0){
			panNum--;
		}else{
			panNum = panels.Length - 1;
		}
		
		//activePan = panels[panNum];
		panels[panNum].SetActive(true);
		prevPan = panels[panNum];
		prevPan.GetComponent<BillboardPanel>().pointPos.GetComponent<LineDrawer>().Create();
		//Invoke("Next", 3f);
		SetActivePoint();
	}

	public void Switch(){
		//if (prevPan != null){
			//prevPan.SetActive(false);
		//}

		if (panNum + 1 < panels.Length){
			panNum++;
		}else{
			panNum = 0;
		}
		
		//activePan = panels[panNum];
		panels[panNum].SetActive(true);
		prevPan = panels[panNum];
		prevPan.GetComponent<BillboardPanel>().ShowSelf();

	}

	
	public void EndHideLine(){
		panels[panNum].GetComponent<BillboardPanel>().HideSelf();
		Switch();
	}

	public void Next()
	{
		if (mode == 1 || mode == 0) {return;}

		ingPan.SetActive(false);



		//_CONTROLLER.SetModeTo(1);
		


		if (prevPan != null){
			prevPan.GetComponent<BillboardPanel>().pointPos.GetComponent<LineDrawer>().Delete();
		}
		else{
			Switch();
		}
		//Invoke("Next", 3f);
		SetActivePoint();
	}
}
