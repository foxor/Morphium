using UnityEngine;
using System.Collections;

public class Duration : MonoBehaviour {
	
	protected float lifetime;
	public float Lifetime {
		get {
			return lifetime;
		}
		set {
			lifetime = value;
			alarm = Time.time + alarm;
		}
	}
	
	protected float alarm;
	
	void Start () {
		Lifetime = 0f;
	}
	
	void Update () {
		if (Time.time > alarm) {
			Destroy(gameObject);
		}
	}
}