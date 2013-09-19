using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DamageDuringContact : DamageDealer {
	
	protected const float MINIMUM_TICK_TIME = 0.1f;
	
	protected Damage damagePerSecond;
	public Damage DamagePerSecond {
		get {
			return damagePerSecond;
		}
		set {
			damagePerSecond = value;
			damageTickInterval = 1f / ((float)damagePerSecond.Magnitude);
			singleTickDamage = new Damage() {Magnitude = 1, Type = damagePerSecond.Type};
			while (damageTickInterval < MINIMUM_TICK_TIME) {
				damageTickInterval *= 2f;
				singleTickDamage.Magnitude *= 2;
			}
		}
	}
	
	protected float damageTickInterval;
	protected Damage singleTickDamage;
	protected Dictionary<StatManager, float> nextDamageTime;
	
	public void Awake() {
		nextDamageTime = new Dictionary<StatManager, float>();
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
				manager.DealDamage(singleTickDamage, true, this);
				nextDamageTime[manager] += damageTickInterval;
			}
		}
	}
}