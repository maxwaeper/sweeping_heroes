using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addNewItem : MonoBehaviour {
	public int ID;
	GameObject inventory;
	GameObject player;
	bool isTrigger;

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find ("Inventory");
		player = GameObject.FindWithTag ("Player");

		Debug.Log(this.transform.GetComponent<Collider2D>().isTrigger.ToString());
		Debug.Log ("Я родился!");
		isTrigger = false;
		Debug.Log (this.GetComponentInParent<SpriteRenderer> ().sprite);
		this.GetComponentInParent<SpriteRenderer>().sprite = inventory.GetComponent<ItemDatabase> ().FetchItemByID (ID).Sprite;
	}
		
	//void OnTriggerExit(Collider2D suchPlayer){
	//	Debug.Log ("тебе повезло");
	//}

	void OnTriggerStay2D(Collider2D other){
		isTrigger = true;
	}

	void OnTriggerExit2D(Collider2D other){
		isTrigger = false;
	}

	void Update(){
		if (isTrigger) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				inventory.GetComponent<Inventory> ().AddItem (ID);
				Destroy (this.transform.parent.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		isTrigger = true;
	}
}
