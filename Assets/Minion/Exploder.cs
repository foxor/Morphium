using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour {
	public GameObject explosion;
	
	public void Start() {
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Die, OnDeath);
	}
	
	public void OnDeath(CharacterEvent data) {
		GameObject ex = (GameObject)Instantiate(explosion);
		ex.transform.position = transform.position;
	}
}