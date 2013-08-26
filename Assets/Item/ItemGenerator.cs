using UnityEngine;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour {
	
	protected LootTrap trap;
	protected ItemManager itemManager;
	
	public void Awake() {
		trap = GetComponent<LootTrap>();
		itemManager = GetComponent<ItemManager>();
	}
	
	public void Start() {
		GetComponent<MorphidEventListener>().AddCallback(MorphidEvents.Kill, OnKill);
	}
	
	protected Item Generate(int itemValue) {
		float totalWeight = 0f, mainStat, offStat, health, morphium;
		totalWeight += (mainStat = Random.Range(0f, 1f));
		totalWeight += (offStat = Random.Range(0f, 1f));
		totalWeight += (health = Random.Range(0f, 1f));
		totalWeight += (morphium = Random.Range(0f, 1f));
		return new Item() {
			FilledSlot = System.Enum.GetValues(typeof(Slot)).Pick<Slot>(),
			StatBoost = (int)((((float)itemValue) * mainStat) / totalWeight),
			OffStatBoost = (int)((((float)itemValue) * offStat) / totalWeight),
			HealthBoost = (int)((((float)itemValue) * health) / totalWeight),
			MorphiumBoost = (int)((((float)itemValue) * morphium) / totalWeight),
			OffStatType = System.Enum.GetValues(typeof(StatType)).Pick<StatType>()
		};
	}
	
	protected int NextValue() {
		float totalValue = 0f;
		foreach (Slot slot in  System.Enum.GetValues(typeof(Slot))) {
			totalValue += (float)itemManager.GetSlotEquipped(slot).Value;
		}
		return (int)Random.Range(totalValue * 0.5f, totalValue * 1.1f);
	}
	
	protected void OnKill(MorphidEvent data) {
		trap.AddItem(Generate(NextValue()), data.other.transform.position);
	}
}