﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Analytics;
using System.Collections.Generic;
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

	public bool slideShow = false;

	public InfoTextPanel infoText;

	//public GameObject[] partsToShow;

	public AudioClip clickSnd;

	public GameObject holeObj;

	GameObject activePart = null;
	GameObject destPart = null;
	public int scenePart = 1;

	public GameObject[] animationsParts;

	public GameObject askPan;
	public GameObject waiter;

	public GameObject bigIcons;
	public Texture2D[] bigIconsTex;

	public int ARmode = 0;
	public Texture2D markerToSave;

	public bool canReturn = false;

	public GameObject WaiterCanvas;

	public float slideShowTime = 0f;

	string[] texts = new string[]{
		"Обеспечивает коммуникации по каналам операторов мобильной связи.",
		"Обеспечивает шифрованные мобильные переговоры.",
		"Встроенная рация позволяет общаться в пределах рабочей группы на расстоянии до 5 км.",
		"Встроенный GPS позволяет прокладывать маршрут.",
		"Пыленепроницаемый: пыль не проникает внутрь корпуса.",
		"Ударопрочный: стойкость к падению с большой высоты.",
		"Водонепроницаемый: выдерживает погружение на глубину до 1 м.",
		///
		"Устойчивый 4,5 дюймовый экран Corning Gorilla Glass 2 сопротивляется царапинам и повреждениям при падении.",
		"Резиновый корпус смягчает механические удары, защищает телефон от воды и пыли.",

		"Две встроенные фото- и видеокамеры по 13 Мп. позволяют делать высококачественную фото- и видеосъемку при сильном или слабом свете.",
		//10...
		"Емкая батарея Li-Ion 4200 мАч. — время работы в режиме разговора 1520 минут, в режиме ожидания 380 часов.",
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
	
		bigIcons.SetActive(false);

		if (GameObject.Find("slide1") != null){
			GameObject.Find("slide1").GetComponent<Button>().interactable = true;
		}

		if (Application.loadedLevelName == "SceneVR"){
			//_CONTROLLER._UI.SetActiveScreen("AboutScreen");
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

		if (askPan != null){
			askPan.SetActive(false);
		}
		infoText.ShowText("Защищенное корпоративное устройство\nпод управлением системы\nSafePhonePLUS");
	}

	public void GoToMain(){
//		Debug.Log("canReturn: " + canReturn);
//		if (canReturn == false) {return;} 
		//if (mode == -1) {return;}
		//Application.loadedLevelName == "Scene"
	//	if (ARmode > 0){

			Instantiate(WaiterCanvas);

			PlayerPrefs.SetInt("firstStart", 0);
			waiter.SetActive(true);
			Application.LoadLevel("Scene");
		//}
	}

	public void SwitchAutoPlay(float time){

		GameObject.Find("slide1").GetComponent<Button>().interactable = true;
		GameObject.Find("slide2").GetComponent<Button>().interactable = true;
		GameObject.Find("slide3").GetComponent<Button>().interactable = true;

		switch((int)time){
			case 0: GameObject.Find("slide1").GetComponent<Button>().interactable = false; break;
			case 3: GameObject.Find("slide2").GetComponent<Button>().interactable = false; break;
			case 5: GameObject.Find("slide3").GetComponent<Button>().interactable = false; break;
			default: break;
		}

		if (time == 0){
			slideShow = false;
		}else{
			slideShow = true;
		}

		slideShowTime = time;

		//slideShow = !slideShow;
		if (mode > 0){
			if (slideShow == true && mode > -1){
				SlideShowNext();
			}else{
				CancelInvoke();
			}
		}
	}

	public void SwitchARToMode(int fmode){
		if (fmode > 0 && fmode <= 2){
			ARmode = fmode;
			lastMode = ARmode;
		}

		if (fmode == 1){

			if (GameObject.Find("slide1") != null){
				GameObject.Find("slide1").GetComponent<Button>().interactable = true;
			}

			_CONTROLLER.ARCam.enabled = true;
			holeObj.SetActive(true);

			Renderer[] rendererComponents = gameObject.GetComponentsInChildren<Renderer>(true);

			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

		}

		if (fmode == 0){

			if (GameObject.Find("slide1") != null){
				GameObject.Find("slide1").GetComponent<Button>().interactable = true;
			}

			_CONTROLLER.ARCam.enabled = false;
			_CONTROLLER.ARCam.gameObject.GetComponent<SmoothCamera>().enabled = false;
			_CONTROLLER.ARCam.gameObject.transform.position = new Vector3(-15.9f, 47.5f, -53.4f);//-39.62f, 24.3f, -40.2f);
			_CONTROLLER.ARCam.gameObject.transform.rotation = Quaternion.Euler(36.7f, 330.3f, 355f);//29.6f, 353f, 0f);

			animPhone.gameObject.SetActive(true);

			visible = true;



			Renderer[] rendererComponents = gameObject.GetComponentsInChildren<Renderer>(true);
						
						// Enable rendering:
						foreach (Renderer component in rendererComponents)
						{
							component.enabled = true;
						}

			holeObj.SetActive(false);
		}else{
			_CONTROLLER.ARCam.gameObject.GetComponent<SmoothCamera>().enabled = true;
		}

		if (fmode == 2){
				PlayerPrefs.SetInt("firstStart", 0);
				waiter.SetActive(true);
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
	
	void SaveMarkerOnAndroid(){
		askPan.transform.GetChild(1).gameObject.SetActive(true);
		string path = ParseAndroidPath();
		Debug.Log(path);
		File.WriteAllBytes(Application.persistentDataPath + "/markerVR.jpg", markerToSave.EncodeToJPG());
		if (path != ""){
			askPan.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = askPan.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text + "\n\nСохранено в: \n" + path;
		}else{
			askPan.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "Возникла ошибка при сохраении маркера!";
		}
	}
	
	string ParseAndroidPath(){

		string startPath = Application.persistentDataPath;
		string endPath = "";
		string[] locations = startPath.Split('/');
		int allCount = locations.Length;

		for (int i = 0; i < locations.Length; i++){
			//"android"
			if (locations[i].ToLower() == "android"){
				for (int k = i; k < locations.Length; k++)
				{
					endPath = endPath + locations[k] + "/";
				}

				return endPath;
			}
		}

		return "";
	}
	
	public void SaveMarker(){
		#if UNITY_IOS
			XCodeSaveMarker.SaveMarkerToAlbum();
		#elif UNITY_ANDROID
			SaveMarkerOnAndroid();
		#endif

		askPan.transform.GetChild(0).gameObject.SetActive(false);
		askPan.transform.GetChild(1).gameObject.SetActive(true);
	}
	
	public void IHaveMArker(){
		//askPan.SetActive(false);
		PlayerPrefs.SetInt("firstStart", 0);

		waiter.SetActive(true);
		Application.LoadLevel("SceneVR");
		//askPan.transform.GetChild(0).gameObject.SetActive(true);
		//askPan.transform.GetChild(1).gameObject.SetActive(false);
	}
	
	public void AskUserForMarker(){
		if (askPan != null){
			askPan.SetActive(true);
			askPan.transform.GetChild(0).gameObject.SetActive(true);
			askPan.transform.GetChild(1).gameObject.SetActive(false);
		}
	}
	
	public void AnalTest(){
//		Analytics.Transaction("12345abcde", 150.55m, "RUB", null, null);
//
////		int totalPotions = 5;
////		int totalCoins = 100;
////		string weaponID = "Weapon_102";
////		Analytics.CustomEvent("gameOver", new Dictionary<string, object>
////		                      {
////			{ "potions", totalPotions },
////			{ "coins", totalCoins },
////			{ "activeWeapon", weaponID }
////		});
//
//		Gender gender = Gender.Female;
//		Analytics.SetUserGender(gender);
//		
//		int birthYear = 2014;
//		Analytics.SetUserBirthYear(birthYear);
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

		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				GoToMain();
			}
		}
			
			if (ARmode != 2){

			if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				//Debug.Log("touch: "+touchDeltaPosition);
				if (touchDeltaPosition.x > 40f)
				{
					Prev();
				}

				if (touchDeltaPosition.x  < -40f)
				{
					Next();
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

		//man.DownEnd();

		foreach(Image img in stepPoints){
			img.color = offColor;
		}
		//Привет парень) Не хочешь сделать эту часть кода....... защищенной..? м??
		stepPoints[panNum].color = onColor;

		AudioSource.PlayClipAtPoint(clickSnd, Vector3.zero, .5f);

		bool changed = false;

		if (mode == 2){
//			if (panNum < 7){
//				if (scenePart == 2){
//				//	animPhone.Play("fastDown");
//					animPhone.Play("fastUp");
//					changed = true;
//				}
//
//				scenePart = 1;
//			}
//			else{
//				if (scenePart == 1){
//					//animPhone.Play("fastUp");
//					animPhone.Play("fastDown");
//					changed = true;
//				}
//
//				scenePart = 2;
//			}
//
//			ShowScreen(-1);

			if (bigIconsTex[panNum] != null){
				bigIcons.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = bigIconsTex[panNum];
				bigIcons.SetActive(true);
			}else{
				bigIcons.SetActive(false);
			}
			
			switch(panNum){
			case 0: ShowScreen(3); man.ShowAnim("Talk1"); break;
			case 1: ShowScreen(3); man.ShowAnim("Talk1"); break;
			case 3: ShowScreen(2); man.ShowAnim("Gps"); break;
			case 2: ShowScreen(6); man.ShowAnim("WalkieTalkie"); break;
			case 4: ShowScreen(8); man.ShowAnim("Clean"); break;
			case 5: ShowScreen(8); man.ShowAnim("axe"); break;

			case 6: ShowScreen(8); man.ShowAnim("water"); break;
			case 7: ShowScreen(4); man.ShowAnim("Screw"); break;
			case 8: ShowScreen(8); man.ShowAnim("Body"); break; //animPhone.Play("showLeft");
			case 9: ShowScreen(5); animPhone.Play("idleDown"); man.ShowAnim("Photo"); break;
			case 10: ShowScreen(0); animPhone.Play("showBack");  break; // changed = true; 
			case 11: ShowScreen(7); animPhone.Play("idleDown"); man.ShowAnim("Sos"); break; //animPhone.Play("showLeft"); changed = true; // showleft
			case 12: man.ShowAnim("LaserLight"); break; animPhone.Play("fastDown");
			
				
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
				animPhone.Play("idleDown");
				//animPhone.Play("idleUp");
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
		ShowTip();
	}

	public void Focus(){

	}


//	public void ShowMan(){
//		man.Show(true);
//	}

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

			if (Application.loadedLevelName == "SceneVR"){
				mode = 2;
				lastMode = 2;
				ARmode = 2;
			}

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

		canClick = true;

		//man.anim.speed = 0;
		man.HideForce();

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


		man.gameObject.SetActive(true);

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

		man.gameObject.SetActive(true);

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
		//canClick = false;

		if (slideShow == true){
			CancelInvoke();
			Invoke("SlideShowNext", 5f);
		}

		//ingPan.SetActive(false);
		
		if (mode == 2){

			if (panNum - 1 >= 0){
				panNum--;
			}else{
				panNum = texts.Length - 1;
			}
			
			infoText.ShowText(texts[panNum]);

			//Invoke("AcceptClic", 2f);
		}else{
			if (panNum - 1 >= 0){
				panNum--;
			}else{
				panNum = texts2.Length - 1;
			}
			infoText.ShowText(texts2[panNum]);
			//Invoke("AcceptClic", 1f);
		}
		
		SetActivePoint();
	}

	void SlideShowNext(){
		if (slideShow == true){
			Invoke("SlideShowNext", slideShowTime);
		}else{
			CancelInvoke();
			return;
		}

		if (mode == 2){
			SetActivePoint();
			if (panNum + 1 < texts.Length){
				panNum++;
			}else{
				panNum = 0;
			}
			
			infoText.ShowText(texts[panNum]);
			//Invoke("AcceptClic", 2f);
		}else{
			SetActivePoint();
			if (panNum + 1 < texts2.Length){
				panNum++;
			}else{
				panNum = 0;
			}
			infoText.ShowText(texts2[panNum]);
			//Invoke("AcceptClic", 1f);
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
		
		//SetActivePoint();
	}
	
	public void Next()
	{
		if (mode == 0 || canClick == false)  {return;} // || canClick == false) 
		//canClick = false;
		//Invoke("AcceptClic", 2f);

		if (slideShow == true){
			CancelInvoke();
			Invoke("SlideShowNext", slideShowTime);
		}

		//ingPan.SetActive(false);


		if (mode == 2){

			if (panNum + 1 < texts.Length){
				panNum++;
			}else{
				panNum = 0;
			}

			infoText.ShowText(texts[panNum]);
			//Invoke("AcceptClic", 2f);
		}else{
			if (panNum + 1 < texts2.Length){
				panNum++;
			}else{
				panNum = 0;
			}
			infoText.ShowText(texts2[panNum]);
			//Invoke("AcceptClic", 1f);
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
