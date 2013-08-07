using UnityEngine;
using System;
using System.Collections.Generic;

public class StatManager : MonoBehaviour {
	public Dictionary<StatType, Stat> stats;
	
	public void Awake() {
		stats = new Dictionary<StatType, Stat>();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			stats[statType] = new Stat(){Max = 5, Current = 5};
		}
	}
	
	public void DealDamage(Damage damage) {
		Stat damaged = stats[damage.Type.Damages()];
		damaged.Current -= damage.Magnitude;
		if (damaged.Current <= 0) {
			Destroy(gameObject);
		}
	}
}