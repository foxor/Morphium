using UnityEngine;
using System;
using System.Collections.Generic;

public class StatManager : MonoBehaviour {
	protected Dictionary<StatType, Stat> stats;
	
	public void Start() {
		stats = new Dictionary<StatType, Stat>();
		Dictionary<StatType, int> boosts = GetComponent<ItemManager>().Boosts();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			stats[statType] = new Stat(){
				Max = boosts[statType],
				Current = boosts[statType],
				SingleTickRegenTimer = 0.6f,
				NextRegenTick = Time.time
			};
		}
	}
	
	public void DealDamage(Damage damage, bool stopRegen) {
		if (damage.Magnitude == 0) {
			return;
		}
		Stat damaged = stats[damage.Type.Damages()];
		if (damaged.Current == damaged.Max) {
			damaged.NextRegenTick = Time.time + damaged.SingleTickRegenTimer + (stopRegen ? damaged.RegenCooldown : 0f);
		}
		damaged.Current -= damage.Magnitude;
		if (damaged.Current <= 0) {
			Destroy(gameObject);
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
			while (s.Current < s.Max && s.NextRegenTick < Time.time) {
				s.Current ++;
				s.NextRegenTick += s.SingleTickRegenTimer;
			}
		}
	}
}