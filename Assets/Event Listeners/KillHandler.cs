using UnityEngine;
using System;
using System.Collections.Generic;

public class KillHandler : MonoBehaviour {
	
	protected StatManager statManager;
	protected LootTrap lootTrap;
	
	public void Awake() {
		statManager = GetComponent<StatManager>();
		lootTrap = GetComponent<LootTrap>();
	}
	
	public void AwardKill(GameObject killed) {
		if (statManager != null) {
			statManager.AwardKill(killed);
		}
		if (lootTrap != null) {
			lootTrap.AddItem(new Item() {
			}, killed.transform.position);
		}
	}
}