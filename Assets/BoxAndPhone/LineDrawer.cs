using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour {

	LineRenderer line;
	public GameObject target;

	Vector3 startPosition;    //The starting position in world space
	Vector3 endPosition;    //The ending position in world space
	Vector3 bending = Vector3.up;    //Bend factor (on all axes)
	float lineSpeed = 3.0f;  
	float timeStamp;
	public float counter = 0f;
	float dist;

	public float myLineWidth = .2f;

	///0 - idle, 1 - creating, 2 - deleting;
	int state = 0;

	public void DeletLine(){
		
	}

	// Use this for initialization
	void Start () {

		if (target){
			line = GetComponent<LineRenderer>();
			line.SetWidth(myLineWidth, myLineWidth);
			//line.useWorldSpace = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, target.transform.position);
		}

		startPosition = transform.position;
		endPosition = target.transform.position;
	}

	public void Create() {
//		counter = 0f;
//		dist = Vector3.Distance(startPosition, endPosition);
//		state = 1;
	}

	void Delete() {

		dist = Vector3.Distance(startPosition, endPosition);
		counter = dist;
		state = 1;
	}
	
	// Update is called once per frame
	void Update () {

		//creating
		if (state == 1){
			if (counter > 0){
				//Debug.Log("draw");
				counter -= 0.1f / lineSpeed;
				float x = Mathf.Lerp(0, dist, counter);
				Vector3 pointA = startPosition;
				Vector3 pointB = endPosition;

				Vector3 pointAtLine = x * Vector3.Normalize(pointB - pointA) + pointA;
				line.SetPosition(1, pointAtLine);
			}
			else{
				state = 0;
				Delete();
			}
		}

		//deleting
		if (state == 2){
			
		}

//		if (target){
//			line.SetPosition(0, transform.position);
//			line.SetPosition(1, target.transform.position);
//		}
	}
}
