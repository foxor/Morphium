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
	
	public void Awake() {
		isPlayer = (GetComponent<HealthBar>() != null);
		isDead = false;
	}
	
	public void OnDeath() {
		if (isPlayer) {
			GetComponent<StatManager>().Reset();
			Application.LoadLevel("Shop");
		}
		else {
			Destroy(gameObject);
			isDead = true;
		}
	}
}