using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BillboardPanel : MonoBehaviour
{
	Camera referenceCamera;
	
	public enum Axis {up, down, left, right, forward, back};
	public bool reverseFace = false; 
	public Axis axis = Axis.up; 

	public GameObject pointPos;

	int mode = 0;
	float counter;
	Color surf;

	// return a direction based upon chosen axis
	public Vector3 GetAxis (Axis refAxis)
	{
		switch (refAxis)
		{
		case Axis.down:
			return Vector3.down; 
		case Axis.forward:
			return Vector3.forward; 
		case Axis.back:
			return Vector3.back; 
		case Axis.left:
			return Vector3.left; 
		case Axis.right:
			return Vector3.right; 
		}
		
		// default is Vector3.up
		return Vector3.up; 		
	}
	
	void  Awake ()
	{
		// if no camera referenced, grab the main camera
		if (!referenceCamera)
			referenceCamera = Camera.main; 

		//surf = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color;
	}

	void Start(){
		//RectTransform rect = GetComponent<RectTransform>();
//		pointPos.transform.SetParent(gameObject.transform.parent.transform);
//		pointPos.transform.localPosition = Vector3.zero;
//		//GetComponentInChildren<LineDrawer>().target = pointPos;
		//Debug.Log(rect.sizeDelta);
	}

	public void HideSelf(){
		counter = .7f;
		mode = 2;

		//float a2 = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text>().color.a;
		//a1 = 0f;
		//a2 = 0f;
	}

	public void ShowSelf(){
		counter =.0f;
		mode = 1;
		transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(surf.r,surf.g,surf.b, 0f);
		//float a2 = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text>().color.a;
		//a1 = 0f;
		//a2 = 0f;
	}
	
	void  LateUpdate()
	{
		// rotates the object relative to the camera
		Vector3 targetPos = transform.position + referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back) ;
		Vector3 targetOrientation = referenceCamera.transform.rotation * GetAxis(axis);
		transform.LookAt (targetPos, targetOrientation);

		if (mode == 1){
			if (counter < 0.7f){
				counter += 0.02f;
				//surf.a = counter;
				transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(surf.r,surf.g,surf.b,counter);
			}
			else{
				pointPos.GetComponent<LineDrawer>().Create();
				mode = 0;
			}
		}

		if (mode == 2){
			if (counter > 0f){
				counter -= 0.02f;
				//surf.a = counter;
				transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(surf.r,surf.g,surf.b,counter);
			}
			else{
				gameObject.SetActive(false);
			}
		}
	}
}