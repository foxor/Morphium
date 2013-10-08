using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemGenerator : MonoBehaviour {
	
	protected LootTrap trap;
	protected ItemManager itemManager;
	
	public void Awake() {
		trap = GetComponent<LootTrap>();
		itemManager = GetComponent<ItemManager>();
	}
	
	public void Start() {
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Kill, OnKill);
	}
	
	protected Ability GenerateAbility(Slot slot) {
		StatManager s = GetComponent<StatManager>();
		switch (slot) {
		case Slot.Arm:
			return new Projectile(s){castTime = 0.2f, cooldown = 1f};
		case Slot.Chest:
			return new Beam(s);
		case Slot.Engine:
			return new Pool(s){duration = 2f, cooldown = 3f};
		case Slot.Leg:
			return new Move(s){SpeedFactor = 11f, cooldown = 2f, cost = 9};
		case Slot.Head:
			return new Projectile(s){castTime = 0.2f, cooldown = 1f};
		}
		throw new UnityException("Unknown item slot has no ability types");
	}
	
	protected Item Generate(int itemValue) {
		float totalWeight = 0f, mainStat, offStat, health, morphium;
		totalWeight += (mainStat = Random.Range(0f, 1f));
		totalWeight += (offStat = Random.Range(0f, 1f));
		totalWeight += (health = Random.Range(0f, 1f));
		totalWeight += (morphium = Random.Range(0f, 1f));
		Slot slot = System.Enum.GetValues(typeof(Slot)).Pick<Slot>();
		return new Item() {
			FilledSlot = slot,
			StatBoost = (int)((((float)itemValue) * mainStat) / totalWeight),
			OffStatBoost = (int)((((float)itemValue) * offStat) / totalWeight),
			HealthBoost = (int)((((float)itemValue) * health) / totalWeight),
			MorphiumBoost = (int)((((float)itemValue) * morphium) / totalWeight),
			OffStatType = System.Enum.GetValues(typeof(StatType))
				.Cast<StatType>()
				.Where(x => x != slot.Boosts())
				.Pick<StatType>(),
			GrantedAbility = GenerateAbility(slot)
		};
	}
	
	protected int NextValue() {
		float totalValue = 0f;
		foreach (Slot slot in  System.Enum.GetValues(typeof(Slot))) {
			totalValue += (float)itemManager.GetSlotEquipped(slot).Value;
		}
		return (int)Random.Range(totalValue * 0.5f, totalValue * 1.1f);
	}
	
	protected void OnKill(CharacterEvent data) {
		trap.AddItem(Generate(NextValue()), ((KillEvent)data).Other.transform.position);
	}
}