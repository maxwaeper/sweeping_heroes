﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	ItemDatabase database;
	GameObject slotPanel;

	GameObject inventoryPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<Item> items = new List<Item> ();
	public List<GameObject> slots = new List<GameObject> ();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		slotAmount = 20;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.Find ("Slot Panel").gameObject;
		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate (inventorySlot));
			slots [i].transform.SetParent (slotPanel.transform);
		}

		AddItem (0);
		AddItem (1);
	}

	public void AddItem(int id){
		Item itemToAdd = database.FetchItemByID (id);
		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == -1) {
				items [i] = itemToAdd;
				GameObject itemObj = Instantiate (inventoryItem);
				itemObj.transform.SetParent (slots [i].transform);
				itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
				itemObj.transform.position = Vector2.zero;
				itemObj.name = itemToAdd.title;
				break;
			}
		}
	}
}

