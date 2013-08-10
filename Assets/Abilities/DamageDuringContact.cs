using UnityEngine;
using System.Collections;

public class DamageDuringContact : MonoBehaviour {
	
	public Damage damage;
	
	protected void DealDamage(GameObject other) {
		StatManager manager = other.GetComponent<StatManager>();
		if (manager != null) {
			manager.DealDamage(damage, true);
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		DealDamage(other.gameObject);
	}
	
	public void OnTriggerStay(Collider other) {
		DealDamage(other.gameObject);
	}
}
