using UnityEngine;
using System.Collections.Generic;

public class DeathHandler : MonoBehaviour {
	
	protected bool isPlayer;
	
	public void Awake() {
		isPlayer = (GetComponent<HealthBar>() != null);
	}
	
	public void OnDeath() {
		if (isPlayer) {
			GetComponent<StatManager>().Reset();
			Application.LoadLevel("Adventure");
		}
		else {
			Destroy(gameObject);
		}
	}
}