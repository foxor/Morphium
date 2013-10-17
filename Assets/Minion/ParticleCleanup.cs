using UnityEngine;
using System.Collections.Generic;

public class ParticleCleanup : MonoBehaviour {
	protected ParticleSystem particles;
	protected bool hasBeenActive;
	
	public void Awake() {
		particles = GetComponent<ParticleSystem>();
	}
	
	public void Update() {
		if (hasBeenActive && particles.particleCount == 0) {
			Destroy(gameObject);
		}
		else {
			hasBeenActive = true;
		}
	}
}