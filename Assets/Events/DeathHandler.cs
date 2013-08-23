using UnityEngine;
using System.Collections.Generic;

public class DeathHandler : MonoBehaviour {
	
	protected bool isPlayer;
	
	protected bool isDead;
	public bool IsDead {
		get {
			return isDead;
		}
	}
	
	public void Start() {
		isPlayer = (GetComponent<HealthBar>() != null);
		isDead = false;
		GetComponent<MorphidEventListener>().AddCallback(MorphidEvents.Die, OnDeath);
	}
	
	public void OnDeath(MorphidEvent data) {
		if (isPlayer) {
			LevelManager.LoadLevel(Level.Shop);
		}
		else {
			Destroy(gameObject);
			isDead = true;
		}
	}
}