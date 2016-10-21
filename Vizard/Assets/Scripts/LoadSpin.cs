using UnityEngine;
using System.Collections;

public class LoadSpin : MonoBehaviour {

	// Use this for initialization

	public GameObject loadIcon;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 100 * Time.deltaTime, 0, Space.World);
	}
}
