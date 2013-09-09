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
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Die, OnDeath);
	}
	
	public void OnDeath(CharacterEvent data) {
		if (isPlayer) {
			LevelManager.LoadLevel(Level.Shop);
		}
		else {
			Destroy(gameObject);
			isDead = true;
		}
	}
	
	public void OnDestroy() {
		GetComponent<CharacterEventListener>().Broadcast(CharacterEvents.Destroy, null);
	}
}