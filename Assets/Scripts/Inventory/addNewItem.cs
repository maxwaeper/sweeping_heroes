using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addNewItem : MonoBehaviour {
	public int ID;
	GameObject inventory;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find ("Inventory");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			inventory.GetComponent<Inventory> ().AddItem (ID);
			Destroy (GameObject.Find( ID.ToString() + "_item"  ));
		}
	}
}
