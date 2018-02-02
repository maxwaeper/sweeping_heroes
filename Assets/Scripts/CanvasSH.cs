using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSH : MonoBehaviour {

	public GameObject inventory;
	private bool isShowing = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			isShowing = !isShowing;
			inventory.SetActive (isShowing);
			Debug.Log ("Key down");
		}
	}
}
