using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	protected Dictionary<Slot, Item> items;
	
	protected ItemGenerator itemGenerator;
	
	public Texture omniIcon;
	
	public void Awake() {
		items = new Dictionary<Slot, Item>();
	}
	
	public void Start() {
		StatManager s = GetComponent<StatManager>();
		foreach (Slot slot in Enum.GetValues(typeof(Slot))) {
			items[slot] = ItemGenerator.GenerateStartingItem(slot, s);
		}
	}
	
	public void SwitchItem(Item item) {
		items[item.FilledSlot] = item;
	}
	
	public Item GetSlotEquipped(Slot slot) {
		return items[slot];
	}
	
	public IEnumerable<Ability> Abilities() {
		foreach (Slot slot in Enum.GetValues(typeof(Slot))) {
			yield return items[slot].GrantedAbility;
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
			boosts[StatType.Morphium] += items[slot].MorphiumBoost;
		}
		return boosts;
	}
}