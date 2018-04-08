﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database= new List<Item>();
	private JsonData itemData;

	void Start(){

		itemData = JsonMapper.ToObject ( File.ReadAllText( Application.dataPath + "/Items.json" ) );
		ConstructItemDatabase ();

		Debug.Log (FetchItemByID(0).title);
	}

	public Item FetchItemByID(int id){
		for (int i = 0; i < database.Count; i++)
			if (database [i].ID == id)
				return database [i];
		return null;
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["impact"], itemData[i]["slug"].ToString(), (bool)itemData[i]["stackable"] ));
		}
	}
}

public class Item {
	public int ID { get; set; }
	public string title { get; set; }
	public float impact { get; set; }
	public string slug { get; set; }
	public Sprite Sprite { get; set; }
	public bool Stackable { get; set; }

	public Item(int id, string title, float impact, string slug, bool Stackable){
		this.ID = id;
		this.title = title;
		this.impact = impact;
		this.slug = slug;
		this.Sprite = Resources.Load<Sprite> ( "Sprites/Items/" + slug);
		this.Stackable = Stackable;
	}

	public Item(){
		this.ID = -1;

	}
}