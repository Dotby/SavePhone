using UnityEngine;
using System.Collections;
using Vuforia;

public class Controller : MonoBehaviour {

	public int foundCount = 0;
	public PanelsViewer pviewer;
	public SmoothCamera sc;
	UIControll _UI;
	public QCARBehaviour ARCam;

	// Use this for initialization
	void Start () {
		_UI = GetComponent<UIControll>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Found(){
		if (pviewer.mode == -1) {return;}
		foundCount++;
		pviewer.ShowAll();
		sc.enabled = true;
	}

	public void OpenMenu(){
		SetModeTo(0);
	}

	public void Lost(){
		pviewer.HideAll();
		sc.enabled = false;
	}

	public void SetModeTo(int _mode){

		if (_mode == -1){
			Lost();
		}
		else
		{
			if (_mode == 0){
				_UI.SetActiveScreen("SelectMode");
			}
			else{
				_UI.SetActiveScreen("AboutScreen");
				if (_mode == 1){
					pviewer.IngMode();
				}

				if (_mode == 2){
					pviewer.UserMode();
				}
			}

			if (ARCam.enabled == false)
			{
				ARCam.enabled = true;
			}
		}

		//pviewer.mode = _mode;
	}
}
