using UnityEngine;
using System.Collections.Generic;

public class DamageDuringContact : MonoBehaviour {
	
	public Damage damagePerSecond = new Damage(){ Magnitude = 4, Type = Element.Physical};
	
	protected float damageTickInterval;
	protected Damage singleTickDamage;
	protected Dictionary<StatManager, float> nextDamageTime;
	
	public void Awake() {
		damageTickInterval = 1f / ((float)damagePerSecond.Magnitude);
		nextDamageTime = new Dictionary<StatManager, float>();
		singleTickDamage = new Damage() {Magnitude = 1, Type = damagePerSecond.Type};
	}
	
	public void OnTriggerEnter(Collider other) {
		StatManager manager = other.GetComponent<StatManager>();
		if (manager != null) {
			nextDamageTime[manager] = Time.time + damageTickInterval;
		}
	}
	
	public void OnTriggerStay(Collider other) {
		foreach (StatManager manager in nextDamageTime.Keys) {
			while (nextDamageTime[manager] < Time.time) {
				manager.DealDamage(singleTickDamage, true);
				nextDamageTime[manager] += damageTickInterval;
			}
		}
	}
}