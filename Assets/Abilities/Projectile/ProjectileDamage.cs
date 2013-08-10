using UnityEngine;
using System.Collections.Generic;

public class ProjectileDamage : MonoBehaviour {
	public Damage damage;
	
	public void OnTriggerEnter(Collider other) {
		StatManager manager = other.gameObject.GetComponent<StatManager>();
		if (manager != null) {
			manager.DealDamage(damage, true);
			Destroy(gameObject);
		}
	}
}