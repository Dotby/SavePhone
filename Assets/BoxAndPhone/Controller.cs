using UnityEngine;
using System.Collections;
using Vuforia;

public class Controller : MonoBehaviour {

	public int foundCount = 0;
	public PanelsViewer pviewer;
	public SmoothCamera sc;
	public UIControll _UI;
	public VuforiaBehaviour ARCam;
	public VuforiaBehaviour StereoCam;

	void Start () {
		_UI = GetComponent<UIControll>();
	}

	void Update () {
	
	}

	public void Found(){
		if (pviewer.mode == -1) {return;}
		//foundCount++;
		pviewer.ShowAll();
		//sc.enabled = true;
	}

	public void OpenMenu(){
		SetModeTo(0);
	}

	public void Lost(){
		pviewer.HideAll();
		//sc.enabled = false;
	}

	public void SetModeTo(int _mode){

		if (_mode == -1){
			Lost();
		}
		else
		{
			if (_mode == 0){
				pviewer.ARmode = 0;
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

				if (_mode == 3){
					_UI.SetActiveScreen("CamModeSelect");
				}
			}

			if (ARCam.enabled == false && pviewer.ARmode == 1)
			{
				ARCam.enabled = true;
			}
		}
	}
}
