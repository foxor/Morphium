using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	protected const int MORPHID_STARTING_ITEM_STATS = 2;
	
	protected Dictionary<Slot, Item> items;
	
	public Ability omniGrant;
	public Texture omniIcon;
	
	public void Awake() {
		items = new Dictionary<Slot, Item>();
		foreach (Slot slot in Enum.GetValues(typeof(Slot))) {
			items[slot] = new Item(){
				StatBoost = MORPHID_STARTING_ITEM_STATS,
				OffStatBoost = MORPHID_STARTING_ITEM_STATS,
				HealthBoost = MORPHID_STARTING_ITEM_STATS,
				MorphiumBoost = MORPHID_STARTING_ITEM_STATS,
				OffStatType = StatType.Attack,
				GrantedAbility = omniGrant,
				Icon = omniIcon,
				FilledSlot = slot
			};
		}
	}
	
	public void SwitchItem(Item item) {
		items[item.FilledSlot] = item;
	}
	
	public Item GetSlotEquipped(Slot slot) {
		return items[slot];
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
			boosts[StatType.Morphium] += items[slot].MorphiumBoost;
		}
		return boosts;
	}
}