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
	protected Dictionary<CharacterEventListener, float> nextDamageTime;
	
	public void Awake() {
		nextDamageTime = new Dictionary<CharacterEventListener, float>();
	}
	
	protected override void Enter(GameObject other) {
		CharacterEventListener listener = other.GetComponent<CharacterEventListener>();
		if (listener != null) {
			nextDamageTime[listener] = Time.time + damageTickInterval;
		}
	}
	
	protected override void Exit(GameObject other) {
		CharacterEventListener listener = other.GetComponent<CharacterEventListener>();
		if (listener != null) {
			nextDamageTime.Remove(listener);
		}
	}
	
	public void Update() {
		foreach (CharacterEventListener listener in nextDamageTime.Keys.ToArray()) {
			while (nextDamageTime[listener] < Time.time) {
				listener.Broadcast(CharacterEvents.Hit, new HitEvent() {Damage = singleTickDamage, Source = this});
				nextDamageTime[listener] += damageTickInterval;
			}
		}
	}
}