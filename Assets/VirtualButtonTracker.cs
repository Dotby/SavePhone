using UnityEngine;
using Vuforia;

public class VirtualButtonTracker : MonoBehaviour, IVirtualButtonEventHandler
{

	public PanelsViewer pViewer;
	public float startTime = 0f;
	/// <summary>
	/// Called when the scene is loaded
	/// </summary>
	void Start() { 
		if (GetComponent<PanelsViewer>() != null){
			pViewer = GetComponent<PanelsViewer>();
		}

		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vbs.Length; ++i) {
			// Register with the virtual buttons TrackableBehaviour
			vbs[i].RegisterEventHandler(this);
		}
	}
	
	/// <summary>
	/// Called when the virtual button has just been pressed:
	/// </summary>
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
		if (pViewer == null) {return;}
		switch(vb.VirtualButtonName) {
		case "left":
			pViewer.VButton(0);
			Debug.Log("LEFT_B");
			break;
		case "center":
			startTime = Time.time;
			//pViewer.VButton(1);
			Debug.Log("CENTER_B");
			break;
		case "right":
			pViewer.VButton(2);
			Debug.Log("RIGHT_B");
			break;
		default:
			//throw new UnityException("Button not supported: " + vb.VirtualButtonName);
			break;
		}

	}
	
	/// <summary>
	/// Called when the virtual button has just been released:
	/// </summary>
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) { 
		if (vb.VirtualButtonName == "center"){
			//pViewer.VButton(1);
			Debug.Log("CENTER PUSH: " + (Time.time - startTime));
			if (Time.time - startTime >= 3f)
			{
				Application.LoadLevel("Scene");
			}
		}
	}
}