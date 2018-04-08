﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public int slotID;
	private Inventory inv;

	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();
		if (inv.items [slotID].ID == -1) {
			inv.items [droppedItem.slot] = new Item ();
			inv.items [slotID] = droppedItem.item;
			droppedItem.slot = slotID;
		} else {
			Transform item = this.transform.GetChild (0);
			item.GetComponent<ItemData>().slot = droppedItem.slot;
			item.transform.SetParent (inv.slots [droppedItem.slot].transform);
			item.transform.position = inv.slots [droppedItem.slot].transform.position;

			droppedItem.slot = slotID;
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items [droppedItem.slot] = item.GetComponent<ItemData> ().item;
			inv.items [slotID] = droppedItem.item;
		}
	}

	public void deleteInv(){
		inv.items [slotID].ID = -1;
		//inv = null;
	}

	public bool isEpty(){
		if (inv.items [slotID].ID == -1) {
			return true;
		} else
			return false;
	}

	// Use this for initialization
	void Start () {
		inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
