using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	protected Dictionary<Slot, Item> items;
	
	public Ability omniGrant;
	public Texture omniIcon;
	
	public void Awake() {
		items = new Dictionary<Slot, Item>();
		foreach (Slot slot in Enum.GetValues(typeof(Slot))) {
			items[slot] = new Item(){
				StatBoost = 2,
				OffStatBoost = 2,
				HealthBoost = 2,
				ChargeBoost = 2,
				OffStatType = StatType.Sensors,
				GrantedAbility = omniGrant,
				Icon = omniIcon
			};
		}
	}
	
	public Dictionary<StatType, int> Boosts() {
		Dictionary<StatType, int> boosts = new Dictionary<StatType, int>();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			boosts[statType] = 0;
		}
		foreach (Slot slot in items.Keys) {
			boosts[slot.Boosts()] += items[slot].StatBoost;
			boosts[items[slot].OffStatType] += items[slot].OffStatBoost;
			boosts[StatType.Health] += items[slot].HealthBoost;
			boosts[StatType.Charge] += items[slot].ChargeBoost;
		}
		return boosts;
	}
}