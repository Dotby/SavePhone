using UnityEngine;
using System.Collections;

public class ScreenShooter : MonoBehaviour {

	public string albumName = "";
	public string fileName = "";
	bool isScreenShotSave = false;
	public bool isScreenShotWithDateTime;
	public GameObject button;
	public AudioClip shot;

	public Texture2D texture;
	bool saved = false;
	bool saved2 = false;


	void Start () {
		ScreenshotManager.ScreenshotFinishedSaving += ScreenshotSaved;
		ScreenshotManager.ImageFinishedSaving += ImageSaved;
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

	void ScreenshotSaved()
	{
		Debug.Log ("screenshot finished saving");
		saved = true;
	}
	
	void ImageSaved()
	{
		Debug.Log (texture.name + " finished saving");
		saved2 = true;
	}
	
	public void MakeScreenshot(){

		StartCoroutine(ScreenshotManager.Save("MyScreenshot", "MyApp", true));

//		if (Application.platform == RuntimePlatform.Android){
//			//StartCoroutine(ScreenShotBridge.SaveScreenShot(fileName,albumName,isScreenShotWithDateTime,ScreenShotStatus));
//		}
//
//		if (Application.platform == RuntimePlatform.IPhonePlayer){
//			//StartCoroutine(ScreenShotBridge.SaveScreenShot(fileName,albumName,isScreenShotWithDateTime,ScreenShotStatus));	
//		}

		//isScreenShotSave = false;
		//button.SetActive(false);
	}

	void ScreenShotStatus(bool status)
	{
		isScreenShotSave = status;
	}
}
