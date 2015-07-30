using UnityEngine;
using System.Collections;

public class UIControll : MonoBehaviour {

	public GameObject[] _UIScreens;
	public GameObject _activeScreen = null;
	public AudioClip[] _sfxSnd;
	public GameObject _CANVAS;
	public Controller _CONTROLLER;
	
	void Start () {
		_CONTROLLER = GetComponent<Controller>();

		foreach(GameObject _pan in _UIScreens){
			_pan.SetActive(true);
		}

		UIElement[] _uis = GameObject.FindObjectsOfType<UIElement>();
		Debug.Log(_uis.Length);

		foreach(UIElement _ui in _uis){
			_ui.gameObject.SetActive(true);
			_ui._Manager = this;
		}

		foreach(GameObject _pan in _UIScreens){
			_pan.SetActive(false);
		}
	

#if UNITY_EDITOR
		_CONTROLLER.SetModeTo(0);
		Debug.Log("Run in Editor");
		//SetActiveScreen("WelcomeScreen");
#elif UNITY_ANDROID
		Debug.Log("Run on Android");
		SetActiveScreen("WelcomeScreen");
#elif UNITY_IOS
		Debug.Log("Run on Iphone");
		Invoke("StartOnDevice", 5.0f);
#endif

	}

	void StartOnDevice()
	{
		SetActiveScreen("WelcomeScreen");
	}

	public GameObject SetActiveScreen(string _name){

		if (_CONTROLLER.pviewer.mode == -1) {_CONTROLLER.OpenMenu();}

		for (int _i = 0; _i < _UIScreens.Length; _i++){
			if (_UIScreens[_i].name == _name){
				if (_activeScreen != null){
					_activeScreen.SetActive(false);
				}
				_activeScreen = _UIScreens[_i];
				_activeScreen.SetActive(true);
			}
		}

		if (_CONTROLLER.sc.enabled == false){
			_CONTROLLER.pviewer.ShowTip();
		}else{
			//_CONTROLLER.pviewer.HideTip();
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
