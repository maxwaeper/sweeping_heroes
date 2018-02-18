using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	ItemDatabase database;
	GameObject slotPanel;
	GameObject currentSlotPanel;

	private bool isShowing = true;

	Tooltip tooltip;

	GameObject inventoryPanel;
	GameObject currentInventoryPanel;

	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount, currentSlotAmount;
	public List<Item> items = new List<Item> ();
	public List<GameObject> slots = new List<GameObject> ();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		slotAmount = 9;
		currentSlotAmount = 3;

		inventoryPanel = GameObject.Find ("Inventory Panel");

		currentSlotPanel = GameObject.Find ("Current Slot Panel").gameObject; //над потом более аккуратный поиск замутить
		for (int i = 0; i < currentSlotAmount; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate (inventorySlot));
			slots [i].GetComponent<Slot>().slotID = i;
			slots [i].transform.SetParent (currentSlotPanel.transform);
		}

		slotPanel = inventoryPanel.transform.Find ("Slot Panel").gameObject;
		for (int i = currentSlotAmount; i < (currentSlotAmount+slotAmount) ; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate (inventorySlot));
			slots [i].GetComponent<Slot>().slotID = i;
			slots [i].transform.SetParent (slotPanel.transform);
		}

		//currentInventoryPanel = t.Find


		tooltip = this.GetComponent<Tooltip> ();

		AddItem (0);
		AddItem (1);
		AddItem (0);
		AddItem (0);
		AddItem (1);
		AddItem (1);
	}

	public void AddItem(int id){
		Item itemToAdd = database.FetchItemByID (id);
		if (itemToAdd.Stackable && CheckIfItemIsInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();

					data.amount ++;

					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					break;
				}
			}
		} else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.GetComponent<ItemData> ().item = itemToAdd;
					itemObj.GetComponent<ItemData> ().slot = i;
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
					itemObj.transform.position = Vector2.zero;
					itemObj.name = itemToAdd.title;
					if (itemToAdd.Stackable) {
						ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
						data.amount = 1;
					}
					break;
				}
			}
		}
	}

	public void RemoveOneItem(int id){
		Item itemToRemove = database.FetchItemByID (id);
		if (itemToRemove.Stackable && CheckIfItemIsInInventory (itemToRemove) ) {
			
			for (int i = 0; i < slots.Count; i++) {
				
				if ( !slots [i].GetComponent<Slot>().isEpty() ) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					if (data.item.ID == id) {
						if (data.amount > 2) {
							data.amount--;
							data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
							break;
						}
						if (data.amount == 2) {
							data.amount--;
							data.transform.GetChild (0).GetComponent<Text> ().text = "";
							break;
						}
						if (data.amount <= 1) {
							data.amount--;
							RemoveFullStockOfItems (id);
							break;
						}
						break;
					}
				}
			}
		} else {
			if (!CheckIfItemIsInInventory (itemToRemove)) {
				Debug.Log ("Такого предмета в инвентаре нет");
			} else
				if ( !itemToRemove.Stackable ){
					Debug.Log ("Ошибка типа. Инвентарь на стакается, хотя вызвана функция для удаления одного объекта из стакающегося инвентаря. \n Поэтому объект просто удаляется");
					RemoveFullStockOfItems (id);
				}
		}
	}

	public void RemoveFullStockOfItems(int id){ // Удаление первого попавшегося с данным id
		Item itemToRemove = database.FetchItemByID (id);
		for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					Destroy ( slots [i].transform.GetChild(0).gameObject );
					items[i] = new Item();
					break;
			}
		}
	}

	bool CheckIfItemIsInInventory(Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == item.ID)
				return true;
		}
		return false;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.I)) {
			isShowing = !isShowing;
			inventoryPanel.SetActive (isShowing);
			tooltip.Deactivate ();
			//tooltip.SetActive (isShowing);
			Debug.Log ("Key down");
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			RemoveOneItem (0);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			RemoveOneItem (1);
		}
	}
}