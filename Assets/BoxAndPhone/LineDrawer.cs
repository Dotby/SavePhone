using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour {

	LineRenderer line;
	public GameObject target;

	Vector3 startPosition;    //The starting position in world space
	Vector3 endPosition;    //The ending position in world space
	Vector3 bending = Vector3.up;    //Bend factor (on all axes)
	public float lineSpeed = 0.01f;  
	float timeStamp;
	public float counter = 1f;
	float dist;

	public PanelsViewer manager;

	public float myLineWidth = .2f;

	///0 - idle, 1 - creating, 2 - deleting;
	public int state = 0;


	// Use this for initialization
	void Start () {
		Debug.LogWarning("Start");
		if (target){
			line = GetComponent<LineRenderer>();
			line.SetWidth(myLineWidth, myLineWidth);
			line.useWorldSpace = true;
			line.SetPosition(0, transform.position);
			//line.SetPosition(1, target.transform.position);
		}

		startPosition = transform.position;
		endPosition = target.transform.position;
		//Debug.Log("targetPos: " +  endPosition);
		dist = Vector3.Distance(startPosition, endPosition);
	}

	public void Create() {
		Debug.Log("CREATE");

		startPosition = transform.position;
		endPosition = target.transform.position;
		//Debug.Log("targetPos: " +  endPosition);
		dist = Vector3.Distance(startPosition, endPosition);

		line.SetPosition(0, transform.position);

		counter = 1f;
		//dist = Vector3.Distance(startPosition, endPosition);
		state = 1;
	}

	public void Delete() {
		counter = 1f;
		state = 2;
	}
	
	// Update is called once per frame
	void Update () {

		//creating
		if (state == 1){
			if (counter > 0){

				counter -= lineSpeed;

				Debug.Log(dist + " - "  + Mathf.Lerp(0, dist, counter) + " = " + (dist - Mathf.Lerp(0, dist, counter)));
				float x = dist - Mathf.Lerp(0, dist, counter);

				Vector3 pointA = startPosition;
				Vector3 pointB = endPosition;

				Vector3 pointAtLine = x * Vector3.Normalize(pointB - pointA) + pointA;
				line.SetPosition(1, pointAtLine);
			}
			else{
				state = 0;
				counter = 1f;

				//Delete();
			}
		}

		//deleting
		if (state == 2){
			if (counter > 0){
				
				counter -= lineSpeed;
				float x = Mathf.Lerp(0, dist, counter);
				
				Vector3 pointA = startPosition;
				Vector3 pointB = endPosition;
				
				Vector3 pointAtLine = x * Vector3.Normalize(pointB - pointA) + pointA;
				line.SetPosition(1, pointAtLine);
			}
			else{
				counter = 1f;
				state = 0;
				manager.EndHideLine();
			}
		}

//		if (target){
//			line.SetPosition(0, transform.position);
//			line.SetPosition(1, target.transform.position);
//		}
	}
}
