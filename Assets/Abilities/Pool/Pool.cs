using UnityEngine;
using System.Collections;

public class Pool : Ability {
	
	public GameObject prefab;
	
	public Damage damage = new Damage(){Magnitude = 3, Type = Element.Physical};
	public int cost;
	public float duration;
	
	protected override void Cast (Vector3 t) {
		GameObject pool = (GameObject)Instantiate(prefab);
		pool.transform.position = transform.position;
		pool.GetComponent<DamageDuringContact>().damage = damage;
		pool.layer = gameObject.layer;
		pool.GetComponent<Duration>().Lifetime = duration;
	}
	
	protected override int Cost () {
		return cost;
	}
}
