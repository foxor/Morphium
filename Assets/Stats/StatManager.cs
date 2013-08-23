using UnityEngine;
using System;
using System.Collections.Generic;

public class StatManager : MonoBehaviour {
	protected Dictionary<StatType, Stat> stats;
	protected MorphidEventListener listener;
	protected DeathHandler deathHandler;
	
	public void Awake() {
		listener = GetComponent<MorphidEventListener>();
		deathHandler = GetComponent<DeathHandler>();
		stats = new Dictionary<StatType, Stat>();
	}
	
	public void Start() {
		Reset(null);
		MorphidEventListener listener = GetComponent<MorphidEventListener>();
		listener.AddCallback(MorphidEvents.Kill, AwardKill);
		listener.AddCallback(MorphidEvents.Equip, Reset);
		listener.AddCallback(MorphidEvents.Destroy, UnregisterGlobal);
		GlobalEventListener.Listener().AddCallback(Level.Shop, Reset);
	}
	
	protected void UnregisterGlobal(System.Object data) {
		GlobalEventListener.Listener().RemoveCallback(Level.Shop, Reset);
	}
	
	public void DealDamage(Damage damage, bool stopRegen, DamageDealer damageDealer) {
		if (damage.Magnitude == 0 || this == null || deathHandler.IsDead) {
			return;
		}
		Stat damaged = stats[damage.Type.Damages()];
		if (damaged.Current == damaged.Max) {
			damaged.NextRegenTick = Time.time + damaged.SingleTickRegenTimer + (stopRegen ? damaged.RegenCooldown : 0f);
		}
		damaged.Current -= damage.Magnitude;
		if (damaged.Current <= 0) {
			if (damageDealer != null && damageDealer.Owner != null) {
				MorphidEventListener killer = damageDealer.Owner.GetComponent<MorphidEventListener>();
				if (killer != null) {
					killer.Broadcast(MorphidEvents.Kill, new MorphidEvent(){other = gameObject});
				}
			}
			
			listener.Broadcast(MorphidEvents.Die, null);
		}
	}
	
	public void Reset(System.Object data) {
		Dictionary<StatType, int> boosts = GetComponent<ItemManager>().Boosts();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			stats[statType] = new Stat(){
				Max = boosts[statType],
				Current = boosts[statType],
				SingleTickRegenTimer = 0.6f,
				NextRegenTick = Time.time,
				Regenerates = statType != StatType.Health
			};
		}
	}
	
	public void AwardKill(MorphidEvent killed) {
		foreach (StatType s in Enum.GetValues(typeof(StatType))) {
			stats[s].Max *= 6;
			stats[s].Max /= 5;
			if (stats[s].Max < 0) {
				stats[s].Max = int.MaxValue;
			}
			stats[s].Current = stats[s].Max;
		}
	}
	
	public int GetMax(StatType stat) {
		return stats[stat].Max;
	}
	
	public int GetCurrent(StatType stat) {
		return stats[stat].Current;
	}
	
	public void Update() {
		foreach (Stat s in stats.Values) {
			while (s.Current < s.Max && s.Regenerates && s.NextRegenTick < Time.time) {
				s.Current ++;
				s.NextRegenTick += s.SingleTickRegenTimer;
			}
		}
	}
}