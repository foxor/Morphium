using UnityEngine;
using System.Collections.Generic;

public class ProjectileDamage : DamageDealer {
	public Damage damage;
	
	protected override void Enter(GameObject other) {
		StatManager manager = other.GetComponent<StatManager>();
		if (manager != null) {
			manager.DealDamage(damage, true);
			Destroy(gameObject);
		}
	}
}