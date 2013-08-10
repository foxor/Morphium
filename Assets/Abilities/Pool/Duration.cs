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
			alarm = Time.time + lifetime;
		}
	}
	
	protected float alarm;
	
	void Update () {
		if (Time.time > alarm) {
			Destroy(gameObject);
		}
		Debug.Log(Time.time - alarm + " : " + Time.time);
	}
}