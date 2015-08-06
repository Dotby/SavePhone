using UnityEngine;
using System.Collections;

public class UIControll : MonoBehaviour {

	public GameObject[] _UIScreens;
	public GameObject _activeScreen = null;
	public AudioClip[] _sfxSnd;
	public GameObject _CANVAS;
	public Controller _CONTROLLER;
	public PanelsViewer pviewer;
	public bool isFirstLoad = true;
	
	void Start () {
		_CONTROLLER = GetComponent<Controller>();

//		foreach(GameObject _pan in _UIScreens){
//			_pan.SetActive(true);
//		}

		UIElement[] _uis = GameObject.FindObjectsOfType<UIElement>();
		//Debug.Log(_uis.Length);

		foreach(UIElement _ui in _uis){
			_ui.gameObject.SetActive(true);
			_ui._Manager = this;
		}

		// pviewer.InitLines();

		foreach(GameObject _pan in _UIScreens){
			_pan.SetActive(false);
		}



		if (PlayerPrefs.HasKey("firstStart")){
			Debug.Log("Founs fs = " + PlayerPrefs.GetInt("firstStart"));
			if (PlayerPrefs.GetInt("firstStart") == 0){
				isFirstLoad = false;
			}

			if (PlayerPrefs.GetInt("firstStart") == 1){
				isFirstLoad = true;
			}

			if (PlayerPrefs.GetInt("firstStart") == 2){
				isFirstLoad = false;
			}
		}
		else{
			Debug.Log("NOT Founs fs");

			isFirstLoad = true;
		}
//
//		PlayerPrefs.SetInt("firstStart", 1);
//		PlayerPrefs.Save();
	

#if UNITY_EDITOR
		//_CONTROLLER.SetModeTo(0);
		Debug.Log("Run in Editor");
		LoadFirstScene();
		//SetActiveScreen("AboutScreen");
#elif UNITY_ANDROID
		Debug.Log("Run on Android");
		LoadFirstScene();
#elif UNITY_IOS
		Debug.Log("Run on Iphone");
		if (Application.loadedLevelName == "SceneVR"){
			LoadFirstScene();
		}else{
			Invoke("LoadFirstScene", 5.0f);
		}

#endif

	}

	void SetOrientation(){
		if (Application.loadedLevelName == "SceneVR"){
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = false;
			Screen.autorotateToPortraitUpsideDown = false;
		}
		else
		{
			Screen.autorotateToLandscapeLeft = false;
			Screen.autorotateToLandscapeRight = false;
			
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = false;
			
			Screen.orientation = ScreenOrientation.Portrait;
			//SetActiveScreen("CamModeSelect");
			//SetActiveScreen("WelcomeScreen");
		}
	}

	void LoadFirstScene(){
		Debug.Log("First = " + isFirstLoad);
		if (isFirstLoad == true){
			SetActiveScreen("WelcomeScreen");
		}
		else{
			SetActiveScreen("CamModeSelect");
		}

		PlayerPrefs.SetInt("firstStart", 1);

		if (Application.loadedLevelName == "SceneVR"){
			SetActiveScreen("AboutScreen");
		}

		SetOrientation();
	}

	void StartOnDevice()
	{
		SetActiveScreen("WelcomeScreen");
	}

	public GameObject SetActiveScreen(string _name){

		//if (_CONTROLLER.pviewer.mode == -1) {_CONTROLLER.OpenMenu();}

		for (int _i = 0; _i < _UIScreens.Length; _i++){
			if (_UIScreens[_i].name == _name){
				if (_activeScreen != null){
					_activeScreen.SetActive(false);
				}
				_activeScreen = _UIScreens[_i];
				_activeScreen.SetActive(true);
			}
		}

		Debug.Log("pmode" + pviewer.ARmode);
		if (Application.loadedLevelName == "SceneVR"){
			pviewer.ARmode = 2;
		}
		if (pviewer.ARmode < 2){
			if (pviewer.visible == false){
				//_CONTROLLER.pviewer.ShowTip();
			}else{
				//_CONTROLLER.pviewer.HideTip();
			}
		}

		return _activeScreen;
	}

	public void PlaySnd(string _sName){
		if (_sfxSnd.Length == 0) {return;}

		for (int _i = 0; _i < _sfxSnd.Length; _i++){
			if (_sfxSnd[_i].name == _sName){
				AudioSource.PlayClipAtPoint(_sfxSnd[_i], Vector3.zero);
				return;
			}
		}
	}
	
	void Update () {
	
	}

}
