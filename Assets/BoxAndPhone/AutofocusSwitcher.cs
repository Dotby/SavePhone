using UnityEngine;
using System.Collections;
using Vuforia;

public class AutofocusSwitcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		VuforiaBehaviour.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		Vuforia.VuforiaBehaviour.Instance.RegisterOnPauseCallback(OnPaused);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnVuforiaStarted()
	{
		Debug.Log("startAutofocus");
		CameraDevice.Instance.SetFocusMode(
			CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	
	private void OnPaused(bool paused)
	{
		if (!paused) // resumed
		{
			Debug.Log("resumeAutofocus");
			// Set again autofocus mode when app is resumed
			CameraDevice.Instance.SetFocusMode(
				CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
	}
}
