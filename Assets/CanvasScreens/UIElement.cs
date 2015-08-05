using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour {

	public UIControll _Manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySnd(string _snd){
		_Manager.PlaySnd(_snd);
	}

	public void GoToState(int _state){
		_Manager._CONTROLLER.SetModeTo(3);
	}
}
