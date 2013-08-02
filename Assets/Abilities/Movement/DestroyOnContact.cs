using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {
	public void OnTriggerEnter(Collider other) {
		GameObject.Destroy(gameObject);
	}
}