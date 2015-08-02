using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelsViewer : MonoBehaviour {
	
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

	public InfoTextPanel infoText;

	public GameObject[] partsToShow;

	public AudioClip clickSnd;

	string[] texts = new string[]{
		"Устойчивый 4,5 дюймовый экран Corning Gorilla Glass 2 сопротивляется царапинам и повреждениям при падении.",
		"Резиновый корпус смягчает механические удары, защищает телефон от воды и пыли.",
		"Две встроенные фото- и видеокамеры по 13 Мп. позволяют делать высококачественную фото- и видеосъемку при сильном или слабом свете.",
		"Емкая батарея Li-Ion 4200 мАч. — время работы в режиме разговора 1520 минут, в режиме ожидания 380 часов.",
		" Фонарик и лазерная указка для упрощения взаимодействия."
	};

	public GameObject pointsPanel;
	Image[] stepPoints;
	public bool upped = false;


	void Start () {
		StopAnimation();
		ingPan.SetActive(false);

		InitStepIcons();

		//infoText.GetComponent<Image>().enabled = false;
		//infoText.HideSelf();

	}

	void Update () {
	
	}

	public void InitStepIcons(){

		stepPoints = new Image[texts.Length];

		GameObject tmp = pointsPanel.transform.GetChild(0).gameObject;
		offColor = tmp.GetComponent<Image>().color;
		stepPoints[0] = tmp.GetComponent<Image>();

		for (int i = 1; i < texts.Length; i++){
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

		if (panNum == 0){
			animPhone.Play("waterSafe");
			man.Show(true);
		}

		if (panNum == 2){
			animPhone.Play("waterSafe");
			man.Show(false);
		}

		if (panNum == 3){
			animPhone.Play("showBack");
		}

		AudioSource.PlayClipAtPoint(clickSnd, Vector3.zero, .5f);

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
		infoText.HideSelf();
		pointsPanel.SetActive(false);
	}


	public void ShowMan(){
		man.Show(true);
	}

	public void StopAnimation(){
		animPhone.speed = 0;
	}

	public void End(){
		//animPhone.enabled = false;
	}

	public void PhoneUpped(){
		if (upped == false){
			upped = true;
			Debug.Log("upped");
			Debug.Log(animPhone.gameObject.transform.localRotation.eulerAngles);
			//animPhone.enabled = false;

		}
	}

	public void ShowAll(){

		animPhone.speed = 1f;
		boxUp.speed = 1f;

		visible = true;
		pointsPanel.SetActive(true);

		if (first == true && mode == 2){
			first = false;
			animPhone.speed = 1;
			boxUp.Play("upme");
		}

		if (first == true && mode == 1){
			first = false;
			
			animPhone.speed = 1;
		}

		HideTip();
		mode = lastMode;

		if (mode == 1){
		
			ingPan.SetActive(true);
		}

		if (mode == 2){
			infoText.Reshow();
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
			ingPan.SetActive(false);
		}else{
			lastMode = 2;
			ingPan.SetActive(false);

		}

	}

	public void Prev()
	{
		if (mode == 1 || mode == 0) {return;}
		
		ingPan.SetActive(false);
		
		if (panNum >= 1){
			panNum--;
		}else{
			panNum = texts.Length - 1;
		}
		
		infoText.ShowText(texts[panNum]);
		
		SetActivePoint();
	}
	
	public void Next()
	{
		if (mode == 1 || mode == 0) {return;}

		ingPan.SetActive(false);

		if (panNum + 1 < texts.Length){
			panNum++;
		}else{
			panNum = 0;
		}
		
		infoText.ShowText(texts[panNum]);
	
		SetActivePoint();
	}
}
