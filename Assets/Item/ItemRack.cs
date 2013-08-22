using UnityEngine;
using System.Collections.Generic;

public class ItemRack : MonoBehaviour {
	
	public Item[] items;
	
	public void Start() {
		items = new Item[5];
	}
}