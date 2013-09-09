using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class DamageDealer : MonoBehaviour {
	public bool friendlyFire;
	
	public GameObject Owner {
		get; set;
	}
	
	protected virtual void Enter(GameObject other) {
	}
	public void OnTriggerEnter(Collider other) {
		if (other.gameObject != Owner && (friendlyFire || Enemy(other))) {
			Enter(other.gameObject);
		}
	}
	
	protected virtual void Stay(GameObject other) {
	}
	public void OnTriggerStay(Collider other) {
		if (other.gameObject != Owner && (friendlyFire || Enemy(other))) {
			Stay(other.gameObject);
		}
	}
	
	protected virtual void Exit(GameObject other) {
	}
	public void OnTriggerExit(Collider other) {
		if (other.gameObject != Owner && (friendlyFire || Enemy(other))) {
			Exit(other.gameObject);
		}
	}
	
	protected bool Enemy(Collider other) {
		if (Owner == null || other == null) {
			return false;
		}
		Target selfTarget = Owner.GetComponent<Target>();
		Target otherTarget = other.GetComponent<Target>();
		if (selfTarget == null || otherTarget == null) {
			return true;
		}
		return selfTarget.Team != otherTarget.Team;
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