using UnityEngine;
using System.Collections;

/* This script is used to make sure the 4 edges namely, top, bottom, left and right of
 * the UI frame - the pink square on the screen, remains the same size regardless of the size of the frame.
 * It also resizes/moves the frame and updates the coordinates when swiping or pinching.
 */

public class FrameScaling : MonoBehaviour {

	public GameObject canvas;
	public GameObject frame;
	public GameObject top;
	public GameObject left;
	public GameObject right;
	public GameObject bottom;
	private bool swiping = false;
	private Vector2 lastPosition;


	// Update is called once per frame
	void Update () {
		top.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100f, 1.0f / frame.GetComponent<RectTransform> ().localScale.y * 10f);
		bottom.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100f, 1.0f / frame.GetComponent<RectTransform> ().localScale.y * 10f);
		left.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1.0f / frame.GetComponent<RectTransform> ().localScale.x * 10f, 100f);
		right.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1.0f / frame.GetComponent<RectTransform> ().localScale.x * 10f, 100f);



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
			float movement = 0.4f;

			Vector2 directionfingerone = touchZero.position - touchZero.deltaPosition;
			Vector2 directionfingertwo = touchOne.position - touchOne.deltaPosition;

			float xDistance = Mathf.Abs (directionfingerone.x - directionfingertwo.x);
			float yDistance = Mathf.Abs (directionfingerone.y - directionfingertwo.y);

			if (deltaMagnitudeDiff < 0) { //If fingers are moving apart -> grow

				if (xDistance > yDistance) {
					frame.GetComponent<RectTransform> ().localScale = new Vector2 (frame.GetComponent<RectTransform> ().localScale.x + movement, frame.GetComponent<RectTransform> ().localScale.y);
				} else {
					frame.GetComponent<RectTransform> ().localScale = new Vector2 (frame.GetComponent<RectTransform> ().localScale.x, frame.GetComponent<RectTransform> ().localScale.y + movement);
				}
			} else { //If fingers are moving closer -> shrink

				if (xDistance > yDistance) {
					if (frame.GetComponent<RectTransform> ().localScale.x - movement >= 1f)
						frame.GetComponent<RectTransform> ().localScale = new Vector2 (frame.GetComponent<RectTransform> ().localScale.x - movement, frame.GetComponent<RectTransform> ().localScale.y);
				} else {
					if (frame.GetComponent<RectTransform> ().localScale.y - movement >= 1f)
						frame.GetComponent<RectTransform> ().localScale = new Vector2 (frame.GetComponent<RectTransform> ().localScale.x, frame.GetComponent<RectTransform> ().localScale.y - movement);
				}

			}


			//If single finger
		} else if (Input.touchCount == 1) {

			Touch touch = Input.GetTouch (0);

			if (touch.deltaPosition.sqrMagnitude != 0) {
				if (swiping == false) { //If finger just pressed
					swiping = true;
					lastPosition = touch.position;
					return;
				} else { //If finger dragging
					Vector2 direction = Input.GetTouch (0).position - lastPosition;

					float swipeSpeed = 1f;

					//Check whether a greater distance was moved across the x-axis or y-axis
					if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
						//X movement greater than Y movement
						float swipeValue = (touch.position.x - lastPosition.x) * swipeSpeed;
						//float swipeValue = swipeSpeed;

						if (direction.x > 0) { //If moving right - finger moved from lower x value to higher x value

							if (frame.GetComponent<RectTransform> ().localPosition.x <= (canvas.GetComponent<RectTransform> ().sizeDelta.x / 2) - (frame.GetComponent<RectTransform> ().sizeDelta.x / 2)) {
								frame.GetComponent<RectTransform> ().localPosition = new Vector2 (frame.GetComponent<RectTransform> ().localPosition.x + swipeValue, frame.GetComponent<RectTransform> ().localPosition.y);
							}

						} else { //If moving left - finger moved from higher x value to lower x value

							if (frame.GetComponent<RectTransform> ().localPosition.x >= 0 - (canvas.GetComponent<RectTransform> ().sizeDelta.x / 2) + (frame.GetComponent<RectTransform> ().sizeDelta.x / 2)) {
								frame.GetComponent<RectTransform> ().localPosition = new Vector2 (frame.GetComponent<RectTransform> ().localPosition.x + swipeValue, frame.GetComponent<RectTransform> ().localPosition.y);
							}
						}
					} else {
						//Y movement greater than X movement
						float swipeValue = (touch.position.y - lastPosition.y) * swipeSpeed;
						//float swipeValue = swipeSpeed;

						if (direction.y > 0) {  //If moving up - finger moved from lower y value to higher y value

							if (frame.GetComponent<RectTransform> ().localPosition.y <= (canvas.GetComponent<RectTransform> ().sizeDelta.y / 2) - (frame.GetComponent<RectTransform> ().sizeDelta.y / 2)) {
								frame.GetComponent<RectTransform> ().localPosition = new Vector2 (frame.GetComponent<RectTransform> ().localPosition.x, frame.GetComponent<RectTransform> ().localPosition.y + swipeValue);
							}

						} else {  //If moving down - finger moved from higher y value to lower y value

							if (frame.GetComponent<RectTransform> ().localPosition.y >= 0 - (canvas.GetComponent<RectTransform> ().sizeDelta.y / 2) + (frame.GetComponent<RectTransform> ().sizeDelta.y / 2)) {
								frame.GetComponent<RectTransform> ().localPosition = new Vector2 (frame.GetComponent<RectTransform> ().localPosition.x, frame.GetComponent<RectTransform> ().localPosition.y + swipeValue);
							}
						}

					}
				}
			} else {
				swiping = false;
			}
		
		}

		//Calculate coordinates
		FrameCoordinates.size = new Vector2 (frame.GetComponent<RectTransform> ().localScale.x * frame.GetComponent<RectTransform> ().sizeDelta.x,frame.GetComponent<RectTransform> ().localScale.y * frame.GetComponent<RectTransform> ().sizeDelta.y);
		FrameCoordinates.position = frame.GetComponent<RectTransform> ().localPosition;

		FrameCoordinates.top = FrameCoordinates.position.y + (canvas.GetComponent<RectTransform> ().sizeDelta.y / 2) + (FrameCoordinates.size.y / 2);
		FrameCoordinates.bottom = FrameCoordinates.top - FrameCoordinates.size.y;

		FrameCoordinates.left = FrameCoordinates.position.x + (canvas.GetComponent<RectTransform> ().sizeDelta.x / 2) - (FrameCoordinates.size.x / 2);
		FrameCoordinates.right = FrameCoordinates.left + FrameCoordinates.size.x;

		FrameCoordinates.borderWidth = frame.GetComponent<RectTransform> ().localScale.x * left.GetComponent<RectTransform> ().sizeDelta.x;
	}
}
