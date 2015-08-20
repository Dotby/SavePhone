using UnityEngine;
using System.Collections;

public class ScreenShooter : MonoBehaviour {

	public string albumName = "";
	public string fileName = "";
	bool isScreenShotSave = false;
	public bool isScreenShotWithDateTime;
	public GameObject button;
	public AudioClip shot;

	
	void Start () {
	
	}

	void Update () {
		if (isScreenShotSave == true)
		{
			isScreenShotSave = false;
			if (shot != null){
				AudioSource.PlayClipAtPoint(shot, Vector3.zero);
			}

			button.SetActive(true);
		}
	}

	public void MakeScreenshot(){

		if (Application.platform == RuntimePlatform.Android){
			StartCoroutine(ScreenShotBridge.SaveScreenShot(fileName,albumName,isScreenShotWithDateTime,ScreenShotStatus));
		}

		if (Application.platform == RuntimePlatform.IPhonePlayer){
			StartCoroutine(ScreenShotBridge.SaveScreenShot(fileName,albumName,isScreenShotWithDateTime,ScreenShotStatus));	
		}

		isScreenShotSave = false;
		button.SetActive(false);
	}

	void ScreenShotStatus(bool status)
	{
		isScreenShotSave = status;
	}
}
