using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using Vuforia;

public class PanelsViewer : MonoBehaviour {

	public bool canClick = false;

	public int panNum = -1;

	public Texture[] screens;
	public MeshRenderer screenTexture;

	public int mode = -1;
	public int lastMode = 0;
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

	//public GameObject[] partsToShow;

	public AudioClip clickSnd;

	public GameObject holeObj;

	GameObject activePart = null;
	GameObject destPart = null;
	public int scenePart = 1;

	public GameObject[] animationsParts;

	public int ARmode = 0;

	string[] texts = new string[]{
		"Обеспечивает коммуникации по каналам операторов мобильной связи.",
		"Обеспечивает шифрованные мобильные переговоры.",
		"Встроенная рация позволяет общаться в пределах рабочей группы на расстоянии до 5 км.",
		"Встроенный GPS позволяет прокладывать маршрут.",
		"Пыленепроницаемый: пыль не проникает внутрь корпуса.",
		"Ударопрочный: стойкость к падению с большой высоты.",
		"Водонепроницаемый: выдерживает погружение на глубину до 1 м.",
		////
		"Устойчивый 4,5 дюймовый экран Corning Gorilla Glass 2 сопротивляется царапинам и повреждениям при падении.",
		"Резиновый корпус смягчает механические удары, защищает телефон от воды и пыли.",
		"Две встроенные фото- и видеокамеры по 13 Мп. позволяют делать высококачественную фото- и видеосъемку при сильном или слабом свете.",
		"Емкая батарея Li-Ion 4200 мАч. — время работы в режиме разговора 1520 минут, в режиме ожидания 380 часов.",
		//"Профессиональная рация PTT позволяет общаться на расстоянии до 5 км.",
		"Кнопка SOS для определения местоположения и отправки сигнала на заданный номер.",
		"Фонарик и лазерная указка для упрощения взаимодействия."
	};

	string[] texts2 = new string[]{
		"Защита информации на мобильном устройстве от несанкционированного доступа.",
		"Организация защищенной корпоративной мобильной связи.",
		"Управление всем парком мобильных устройств из одной точки.",
		"Удаленная настройка и смена корпоративных политик безопасности.",
		"Возможность централизованной установки приложений из банка доверенного ПО.",
		"Блокирование несанкционированного использования любых интерфейсов и USB.",
		////
		"Кастомизированная сертифицированная мобильная операционная система.",
		"Лицензия на клиентское приложение системы SafePhone.",
		"Лицензия на клиентское приложение СКЗИ ViPNet.",
		"Лицензия на приложение для зашифрованных VoIP коммуникаций.",
		"Корпоративные мобильные сервисы и интеграция с системами безопасности на предприятии.",
	};

	public GameObject pointsPanel;
	Image[] stepPoints;
	public bool upped = false;

	public void SwapDetect(EventSystem ev){
		Debug.Log("move " + ev.currentInputModule.name);

	}

	void Start () {

		if (Application.loadedLevelName == "SceneVR"){
			_CONTROLLER._UI.SetActiveScreen("AboutScreen");
			mode = 2;
			lastMode = 2;
			ARmode = 2;

			IngMode();
				
			pointsPanel.SetActive(true);
			InitStepIcons();
			//Next();

			visible = true;
			//animPhone.speed = 1f;
			boxUp.gameObject.SetActive(true);
			//boxUp.speed = 1f;
			//boxUp.Play("upme");


		}

		//StopAnimation();
		//ingPan.SetActive(false);
		ShowScreen(-1);
		screenTexture.sharedMaterial.mainTexture = new Texture();
		screenTexture.enabled = false;


		foreach(GameObject obj in animationsParts){
			if (obj != null){
				obj.SetActive(false);
			}

		}
		//infoText.GetComponent<Image>().enabled = false;
		infoText.ShowText("Защищенное корпоративное устройство\nпод управлением системы\nSafePhonePLUS");
	}

	public void SwitchARToMode(int fmode){
		if (fmode > 0 && fmode <= 2){
			ARmode = fmode;
			lastMode = ARmode;
		}

		if (fmode == 1){

			_CONTROLLER.ARCam.enabled = true;
			holeObj.SetActive(true);

			Renderer[] rendererComponents = gameObject.GetComponentsInChildren<Renderer>(true);

			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

		}

		if (fmode == 0){
			_CONTROLLER.ARCam.enabled = false;
			_CONTROLLER.ARCam.gameObject.transform.position = new Vector3(120f, 215f, -345f);
			_CONTROLLER.ARCam.gameObject.transform.rotation = Quaternion.Euler(29f, 333f, 0f);

			animPhone.gameObject.SetActive(true);

			visible = true;



			Renderer[] rendererComponents = gameObject.GetComponentsInChildren<Renderer>(true);
						
						// Enable rendering:
						foreach (Renderer component in rendererComponents)
						{
							component.enabled = true;
						}

			holeObj.SetActive(false);
		}

		if (fmode == 2){
			Application.LoadLevel("SceneVR");
			return;
//			Debug.Log("TRY SWITCH ON STEREO...");
//			_CONTROLLER.ARCam.enabled = false;
//
//			if (_CONTROLLER.StereoCam){
//
//				_CONTROLLER.StereoCam.transform.GetChild(0).gameObject.SetActive(true);
//				_CONTROLLER.StereoCam.transform.GetChild(1).gameObject.SetActive(true);
//
//				_CONTROLLER.StereoCam.enabled = true;
//			}
		}

		if (lastMode == 0){
			_CONTROLLER._UI.SetActiveScreen("SelectMode");
		}

		if (lastMode == 1 || lastMode == 2){
			_CONTROLLER._UI.SetActiveScreen("AboutScreen");
		}
		//SetLightToARCamera(onoff);
	}

	public void VButton(int bt){
		if (bt == 0){
			Prev();
			//animPhone.Play("showBack");
		}

		if (bt == 1){

		}

		if (bt == 2){
			Next ();
			//animPhone.Play("fastDown");
		}
	}

	void Update () {
		if (ARmode != 2){

			if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				Debug.Log("touch: "+touchDeltaPosition);
				if (touchDeltaPosition.x > 40f)
				{
					Next();
				}

				if (touchDeltaPosition.x  < -40f)
				{
					Prev();
				}
			}
		}

	}
	
	public void InitStepIcons(){

		if (stepPoints != null){
			for (int i = 1; i < stepPoints.Length; i++){
				DestroyImmediate(stepPoints[i].gameObject);
			}
		}

		if (lastMode == 2){
			Debug.Log("init step icons 2");
			stepPoints = new Image[texts.Length];

			GameObject tmp = pointsPanel.transform.GetChild(0).gameObject;
			offColor = tmp.GetComponent<Image>().color;
			stepPoints[0] = tmp.GetComponent<Image>();

			for (int i = 1; i < texts.Length; i++){
				GameObject tmpNew = (GameObject)Instantiate(tmp as Object);
				tmpNew.transform.SetParent(pointsPanel.transform);
				tmpNew.transform.localScale = tmpNew.transform.lossyScale;
				tmpNew.GetComponent<RectTransform>().localScale = Vector3.one;
				tmpNew.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero); 
				tmpNew.GetComponent<RectTransform>().localPosition = Vector3.zero;
				stepPoints[i] = tmpNew.GetComponent<Image>();
			}
		}

		if (lastMode == 1){
			Debug.Log("init step icons 1");
			stepPoints = new Image[texts2.Length];
			
			GameObject tmp = pointsPanel.transform.GetChild(0).gameObject;
			offColor = tmp.GetComponent<Image>().color;
			stepPoints[0] = tmp.GetComponent<Image>();
			
			for (int i = 1; i < texts2.Length; i++){
				GameObject tmpNew = (GameObject)Instantiate(tmp as Object);
				tmpNew.transform.SetParent(pointsPanel.transform);
				tmpNew.transform.localScale = tmpNew.transform.lossyScale;
				tmpNew.GetComponent<RectTransform>().localScale = Vector3.one;
				tmpNew.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero); 
				tmpNew.GetComponent<RectTransform>().localPosition = Vector3.zero;
				stepPoints[i] = tmpNew.GetComponent<Image>();
			}
		}
	}

	public void SetActivePoint(){
		foreach(Image img in stepPoints){
			img.color = offColor;
		}
		//Привет парень) Не хочешь сделать эту часть кода....... защищенной..? м??
		stepPoints[panNum].color = onColor;

		AudioSource.PlayClipAtPoint(clickSnd, Vector3.zero, .5f);

		bool changed = false;

		if (mode == 2){
			if (panNum < 7){
				if (scenePart == 2){
					animPhone.Play("fastDown");
					changed = true;
				}

				scenePart = 1;
			}
			else{
				if (scenePart == 1){
					animPhone.Play("fastUp");
					changed = true;
				}

				scenePart = 2;
			}

			ShowScreen(-1);
			
			
			switch(panNum){
			case 0: ShowScreen(3); break;
			case 3: ShowScreen(2); break;
			case 2: ShowScreen(6); break;
			case 5: man.Show(true); break;
			case 6: man.Show(false); break;
			case 8: animPhone.Play("showLeft"); break;
			case 9: ShowScreen(5); break;
			case 10: ShowScreen(0); animPhone.Play("showBack"); changed = true; break;
			case 11: ShowScreen(7); animPhone.Play("showLeft"); changed = true; break;
			case 7: ShowScreen(4); break;
				
			default: 
				break;
			}
		}
		else{
			ShowScreen(8);
		}

		if (changed == false && mode == 2) {
			if (scenePart == 1){
				animPhone.Play("idleDown");
			}
			if (scenePart == 2){
				animPhone.Play("idleUp");
			}
		}

		if (mode == 2){
			AnimPlayObj();
		}else{
			if (activePart != null)
			{
				activePart.SetActive(false);
			}
		}
	}

	void ShowScreen(int num){

		Debug.Log("SHOW SCREEN");
		if (num == -1){
			screenTexture.enabled = false;
			return;
		}

		screenTexture.enabled = true;
		screenTexture.sharedMaterial.mainTexture = screens[num];
	}

	void AnimPlayObj(){

//		if (destPart != null){
//			destPart.SetActive(false);
//		}

		if (panNum < 0) {return;}

		foreach(GameObject obj in animationsParts){
			if (obj) {
				obj.SetActive(false);
			}
		}

		if (animationsParts[panNum] != null){
			animationsParts[panNum].SetActive(true);

			activePart = animationsParts[panNum];
			if (activePart.GetComponent<Animator>() != null){
				animationsParts[panNum].GetComponent<Animator>().Play("show");
			}
		}
	}
	
	public void HideAll(){

		if (ARmode == 0) {return;}

		canClick = false;
		ShowTip();
		animPhone.speed = 0f;
		boxUp.speed = 0f;
		if (prevPan != null) {prevPan.SetActive(false);}
		//ingPan.SetActive(false);
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
			animPhone.Play("fastDown");
			//Debug.Log(animPhone.gameObject.transform.localRotation.eulerAngles);
			//animPhone.enabled = false;

			canClick = true;

			Next();
		}
	}

	public void ShowAll(){
		mode = lastMode;
		if (ARmode == 0) {return;}

		canClick = true;

		animPhone.speed = 1f;
		boxUp.speed = 1f;

		visible = true;
		pointsPanel.SetActive(true);

		if (first == true && mode == 2){
			Debug.Log("start _______________--");
			first = false;
			animPhone.speed = 1;
			boxUp.Play("upme");
			InitStepIcons();
		}

		if (first == true && mode == 1){
			first = false;
			
			animPhone.speed = 1;
			//InitStepIcons();
			Next();
		}

		HideTip();


		if (mode == 2 || mode == 1){
			infoText.Reshow();
		}
	}

	public void HideTip(){
		tip.GetComponent<Animator>().Play("hide");
	}

	public void ShowTip(){
		if (ARmode > 0){
			tip.GetComponent<Animator>().Play("show");
		}
	}

	public void IngMode(){
		if (mode == 1){return;}

		if (ARmode == 0){
			visible = true;
			boxUp.Play("upme");
			pointsPanel.SetActive(true);
			animPhone.speed = 1f;
			boxUp.speed = 1f;
		}

		//first = true;

		panNum = -1;

		if (first == true){
			first = false;
			boxUp.enabled = true;
			animPhone.speed = 1;
			boxUp.Play("upme");
		}else{
			animPhone.Play("fastUp");
		}



		if (activePart != null){
			activePart.SetActive(false);
		}

		ShowScreen(8);
		//InitStepIcons();

		if (visible == true){
			mode = 1;
			lastMode = mode;
			InitStepIcons();
			Next();
			//boxUp.gameObject.SetActive(false);
			//ingPan.SetActive(true);
		}else{
			lastMode = 1;
			InitStepIcons();

		}

//		foreach (GameObject obj in animationsParts){
//			obj.SetActive(false);
//		}


	}

	public void UserMode(){
		if (mode == 2){return;}

		if (ARmode == 0){
			visible = true;
			boxUp.Play("upme");
			pointsPanel.SetActive(true);
			animPhone.speed = 1f;
			boxUp.speed = 1f;

			//upped = true;
			//canClick = true;
		}

		first = true;
		panNum = -1;

		if (first == true){
			first = false;
			boxUp.enabled = true;
			animPhone.speed = 1;
			boxUp.Play("upme");
		}else{
			//fastDown
			animPhone.Play("idleDown");
		}


		ShowScreen(-1);

		
		if (visible == true){
			mode = 2;
			lastMode = mode;
			InitStepIcons();
			Next();
			//ingPan.SetActive(false);
		}else{
			lastMode = 2;
			InitStepIcons();

			//ingPan.SetActive(false);
			
		}

	}

	public void Prev()
	{
		if (mode == 0 || canClick == false) {return;}
		canClick = false;


		//ingPan.SetActive(false);
		
		if (mode == 2){
			if (panNum - 1 >= 0){
				panNum--;
			}else{
				panNum = texts.Length - 1;
			}
			
			infoText.ShowText(texts[panNum]);

			Invoke("AcceptClic", 2f);
		}else{
			if (panNum - 1 >= 0){
				panNum--;
			}else{
				panNum = texts2.Length - 1;
			}
			infoText.ShowText(texts2[panNum]);
			Invoke("AcceptClic", 1f);
		}
		
		SetActivePoint();
	}
	
	public void Next()
	{
		if (mode == 0 || canClick == false) {return;}
		canClick = false;
		//Invoke("AcceptClic", 2f);

		//ingPan.SetActive(false);



		if (mode == 2){
			if (panNum + 1 < texts.Length){
				panNum++;
			}else{
				panNum = 0;
			}

			infoText.ShowText(texts[panNum]);
			Invoke("AcceptClic", 2f);
		}else{
			if (panNum + 1 < texts2.Length){
				panNum++;
			}else{
				panNum = 0;
			}
			infoText.ShowText(texts2[panNum]);
			Invoke("AcceptClic", 1f);
		}
	
		if (activePart != null){
			if (activePart.GetComponent<Animator>() != null){
				activePart.GetComponent<Animator>().Play("hide");
				destPart = activePart;
				StartCoroutine(WaitThenDoThings(activePart.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length, destPart));
			}else{
				destPart = activePart;
				destPart.SetActive(false);
			}
		}

		SetActivePoint();
	}

	void AcceptClic(){
		canClick = true;
	}

	IEnumerator WaitThenDoThings(float time, GameObject obj)
	{
		yield return new WaitForSeconds(time);
		
		destPart.SetActive(false);
	}
}
