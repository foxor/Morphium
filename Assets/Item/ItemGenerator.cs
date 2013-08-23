using UnityEngine;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour {
	
	protected LootTrap trap;
	
	public void Awake() {
		trap = GetComponent<LootTrap>();
	}
	
	public void Start() {
		GetComponent<MorphidEventListener>().AddCallback(MorphidEvents.Kill, OnKill);
	}
	
	protected void OnKill(MorphidEvent data) {
		trap.AddItem(new Item(), data.other.transform.position);
	}
}