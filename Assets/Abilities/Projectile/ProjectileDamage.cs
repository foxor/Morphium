using UnityEngine;
using System.Collections.Generic;

public class ProjectileDamage : MonoBehaviour {
	public Damage damage;
	
	public void OnTriggerEnter(Collider other) {
		other.gameObject.GetComponent<StatManager>().DealDamage(damage, true);
		Destroy(gameObject);
	}
}