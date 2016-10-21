using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	bool isPaused = false;
	float startTime ; //(in seconds) 
	static float timeRemaining; //(in seconds)
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) { 
			DoCountDown();
		}
	}

	void DoCountDown() { 
		timeRemaining = startTime - Time.timeSinceLevelLoad;
		Debug.Log("time remaining = " + timeRemaining); 
		if (timeRemaining <0) { 
			timeRemaining = 0;
			isPaused = true;
			TimeIsUp(); 
		} 
	}

	void TimeIsUp() { 
		Debug.Log("Time is Up"); 
		startTime = 12;
		Application.LoadLevel("Navigation"); 
	}
}





