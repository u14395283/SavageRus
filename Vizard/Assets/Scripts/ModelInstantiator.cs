﻿//Written by Ruan "Ru" Klinkert 

using UnityEngine;
using System.Collections;
using Vuforia;

public class ModelInstantiator : MonoBehaviour {
	public TrackableBehaviour theTrackable;
	private GameObject trackableGameObject;
	private static bool reset = false;

	//Reference to model
	public Graph graph = null;

	void Start () {
		Input.simulateMouseWithTouches = true;

		if (theTrackable == null) {
			Debug.Log ("Warning: Trackable not set !!");
		} else {
			SetModel();
		}
	}
		
	//Text to be displayed in GUI
	string guitext = "";

	#region GUI
	void OnGUI() {
		//Display text on GUI
		if (guitext != "") {
			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.UpperCenter;
			int fontsize = 48;

			Vector2 size;

			//Get font size so that text does not exceed frame width
			do {

				style.fontSize = fontsize;
				size = style.CalcSize(new GUIContent(guitext));
				fontsize -= 2;

			}
			while (size.x > Screen.width);

			style.normal.textColor = Color.white;


			GUI.Label (new Rect (0, 0, Screen.width, 150), guitext, style);
		}

	}
	#endregion

	private Vector3 defaultScale = Vector3.one;
	//private bool swiping = false; -- used for moving the graph
	private Vector2 lastPosition;

	#region Update
	void Update () {
		if (reset) {
			graph = null;
			//theTrackable = null;
			//trackableGameObject = null;
			reset = false;
		}

		if (trackableGameObject.transform.childCount == 0 && theTrackable != null) {
			SetModel ();
		} 
			

		//   -- Moves and scales the graph, but it's not aesthetically pleasing
		///////////////////////
		///  Detect touch  ///
		//////////////////////


		if (graph != null) {
			if (theTrackable.CurrentStatus == TrackableBehaviour.Status.TRACKED) {


				//If two fingers on screen
				if (Input.touchCount == 2) {
					// Store both touches.
					Touch touchZero = Input.GetTouch (0);
					Touch touchOne = Input.GetTouch (1);

					// Find the position in the previous frame of each touch.
					Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
					Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

					// Find the magnitude of the vector (the distance) between the touches in each frame.
					float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
					float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

					// Find the difference in the distances between each frame.
					float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

					if (deltaMagnitudeDiff < 0) { //If fingers are moving apart -> grow
						graph.Model.transform.localScale *= 1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f);
					} else { //If fingers are moving closer -> shrink
						//if ((graph.Model.transform.localScale.x / (1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f))) >= defaultScale.x) {
						//Shrink
						graph.Model.transform.localScale /= 1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f);

						float newWidth = (graph.Model.transform.localScale.x / defaultScale.x);
						float heightRatio = graph.Model.transform.Find ("Graph Base").transform.lossyScale.z / graph.Model.transform.Find ("Graph Base").transform.lossyScale.x;
						float newHeight = (graph.Model.transform.localScale.y / defaultScale.y) * heightRatio;

						//Right edge
						if (graph.Model.transform.localPosition.x >= (newWidth / 2)) {
							graph.Model.transform.localPosition = new Vector3 (
								graph.Model.transform.localPosition.x / (1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f)),
								graph.Model.transform.localPosition.y,
								graph.Model.transform.localPosition.z);
						}

						//Left edge
						if (graph.Model.transform.localPosition.x <= -(newWidth / 2)) {
							graph.Model.transform.localPosition = new Vector3 (
								graph.Model.transform.localPosition.x / (1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f)),
								graph.Model.transform.localPosition.y,
								graph.Model.transform.localPosition.z);
						}

						//Top edge
						if (graph.Model.transform.localPosition.z >= (newHeight / 2)) {
							graph.Model.transform.localPosition = new Vector3 (
								graph.Model.transform.localPosition.x,
								graph.Model.transform.localPosition.y,
								graph.Model.transform.localPosition.z / (1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f)));
						}

						//Bottom edge
						if (graph.Model.transform.localPosition.z <= -(newHeight / 2)) {
							graph.Model.transform.localPosition = new Vector3 (
								graph.Model.transform.localPosition.x,
								graph.Model.transform.localPosition.y,
								graph.Model.transform.localPosition.z / (1f + (Mathf.Abs (deltaMagnitudeDiff) / 10f)));
						}

						//}
					}

			
					//If single finger
				}
			}
		}


				/*

				else if (Input.touchCount == 1) {

					Touch touch = Input.GetTouch (0);

					if (touch.deltaPosition.sqrMagnitude != 0){
						if (swiping == false){ //If finger just pressed
							swiping = true;
							lastPosition = touch.position;
							return;
						}
						else{ //If finger dragging
							Vector2 direction = Input.GetTouch(0).position - lastPosition;

							float swipeSpeed = 15.0f;

							if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)){

								float swipeValue = touch.position.x - lastPosition.x;
								float newWidth = (graph.Model.transform.localScale.x / defaultScale.x);

								if (direction.y > 0) {
									if (graph.Model.transform.localPosition.x + ((Mathf.Abs (swipeValue / Screen.width)) / swipeSpeed) <= (newWidth / 2)) {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x + ((Mathf.Abs (swipeValue / Screen.width)) / swipeSpeed),
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z);
									} else {
										graph.Model.transform.localPosition = new Vector3 (
											(newWidth / 2),
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z);
									}

								} else {
									if (graph.Model.transform.localPosition.x - ((Mathf.Abs (swipeValue / Screen.width)) / swipeSpeed) >= -(newWidth / 2)) {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x - ((Mathf.Abs (swipeValue / Screen.width)) / swipeSpeed),
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z);
									} else {
										graph.Model.transform.localPosition = new Vector3 (
											-(newWidth / 2),
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z);
									}
								}
							}
							else{
								float swipeValue = touch.position.y - lastPosition.y;

								float heightRatio = graph.Model.transform.Find ("Graph Base").transform.lossyScale.z / graph.Model.transform.Find ("Graph Base").transform.lossyScale.x;
								float newHeight = (graph.Model.transform.localScale.y / defaultScale.y) * heightRatio;

								if (direction.x > 0) {
									if (graph.Model.transform.localPosition.z + ((Mathf.Abs (swipeValue / Screen.height)) / swipeSpeed) <= (newHeight / 2)) {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x,
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z + ((Mathf.Abs (swipeValue / Screen.height)) / swipeSpeed));
									} else {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x,
											graph.Model.transform.localPosition.y,
											(newHeight / 2));
									}

								} else {
									if (graph.Model.transform.localPosition.z - ((Mathf.Abs (swipeValue / Screen.height)) / swipeSpeed) >= -(newHeight / 2)) {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x,
											graph.Model.transform.localPosition.y,
											graph.Model.transform.localPosition.z - ((Mathf.Abs (swipeValue / Screen.height)) / swipeSpeed));
									} else {
										graph.Model.transform.localPosition = new Vector3 (
											graph.Model.transform.localPosition.x,
											graph.Model.transform.localPosition.y,
											-(newHeight / 2));
									}
								}

							}
						}
					}
					else{
						swiping = false;
					}
				}
			}
		} */





			

	}
	#endregion

	#region Set model
	private void SetModel() {
		trackableGameObject = theTrackable.gameObject;

		trackableGameObject.transform.position = new Vector3 (0f, 0f, 0f);
		trackableGameObject.transform.eulerAngles = new Vector3 (0f, 90f, 0f);

		foreach (Transform child in trackableGameObject.transform)
			GameObject.Destroy (child.gameObject);

	
		//////////////////
		// Create Model //
		//////////////////


		if (Dataset.dataset != null) {
			graph = new Graph (Dataset.dataset);

			if (trackableGameObject != null)
				graph.Model.transform.parent = trackableGameObject.transform;

			//This is where you would change the scale
			float scale = 0.3f;

			graph.Model.transform.localScale = new Vector3 (
				graph.Model.transform.localScale.x * scale, 
				graph.Model.transform.localScale.y * scale, 
				graph.Model.transform.localScale.z * scale); 

			defaultScale = graph.Model.transform.localScale; //This is for zooming the graph - Do no change
		}
		
		
	}
	#endregion

	public static void resetGraph(){
		reset = true;
	}

}
