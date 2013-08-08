using UnityEngine;
using System.Collections;

public class Projectile : Ability {
	
	public GameObject prefab;
	
	public Damage damage = new Damage(){Magnitude = 3, Type = Element.Overload};
	
	public override void Cast (Vector3 t) {
		GameObject projectile = (GameObject)Instantiate(prefab);
		projectile.transform.position = transform.position;
		Move projectileMovement = projectile.GetComponent<Move>();
		projectileMovement.Cast(t);
		projectile.GetComponent<ProjectileDamage>().damage = damage;
		projectile.layer = gameObject.layer;
	}
}