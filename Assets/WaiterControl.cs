using UnityEngine;
using System.Collections;

public class WaiterControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void OnLevelWasLoaded(int level) {
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
