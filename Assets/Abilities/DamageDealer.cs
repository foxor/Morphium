using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class DamageDealer : MonoBehaviour {
	public GameObject Owner {
		get; set;
	}
	
	protected virtual void Enter(GameObject other) {
	}
	public void OnTriggerEnter(Collider other) {
		if (other.gameObject != Owner) {
			Enter(other.gameObject);
		}
	}
	
	protected virtual void Stay(GameObject other) {
	}
	public void OnTriggerStay(Collider other) {
		if (other.gameObject != Owner) {
			Stay(other.gameObject);
		}
	}
	
	protected virtual void Exit(GameObject other) {
	}
	public void OnTriggerExit(Collider other) {
		if (other.gameObject != Owner) {
			Exit(other.gameObject);
		}
	}
}

public static class DamageDealerExtension {
	public static DamageDealer GetDamageDealer(this GameObject x) {
		DamageDealer r = x.GetComponent<DamageDuringContact>();
		if (r != null) {
			return r;
		}
		
		r = x.GetComponent<ProjectileDamage>();
		if (r != null) {
			return r;
		}
		
		throw new Exception("Unable to find damage dealer");
	}
}