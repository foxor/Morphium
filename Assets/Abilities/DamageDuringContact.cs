using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DamageDuringContact : DamageDealer {
	
	public Damage damagePerSecond = new Damage(){ Magnitude = 4, Type = Element.Physical};
	
	protected float damageTickInterval;
	protected Damage singleTickDamage;
	protected Dictionary<StatManager, float> nextDamageTime;
	
	public void Awake() {
		damageTickInterval = 1f / ((float)damagePerSecond.Magnitude);
		nextDamageTime = new Dictionary<StatManager, float>();
		singleTickDamage = new Damage() {Magnitude = 1, Type = damagePerSecond.Type};
	}
	
	protected override void Enter(GameObject other) {
		StatManager manager = other.GetComponent<StatManager>();
		if (manager != null) {
			nextDamageTime[manager] = Time.time + damageTickInterval;
		}
	}
	
	protected override void Exit(GameObject other) {
		StatManager manager = other.GetComponent<StatManager>();
		if (manager != null) {
			nextDamageTime.Remove(manager);
		}
	}
	
	public void Update() {
		foreach (StatManager manager in nextDamageTime.Keys.ToArray()) {
			while (nextDamageTime[manager] < Time.time) {
				manager.DealDamage(singleTickDamage, true);
				nextDamageTime[manager] += damageTickInterval;
			}
		}
	}
}