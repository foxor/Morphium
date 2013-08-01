using UnityEngine;
using System.Collections;

public class Projectile : Ability<ProjectileData> {
	
	public GameObject prefab;
	public MovementData projectileMovementConfiguration;
	
	public override void Cast () {
		GameObject projectile = (GameObject)Instantiate(prefab);
		Move projectileMovement = projectile.GetComponent<Move>();
		projectileMovement.Configuration = projectileMovementConfiguration;
		projectileMovement.Cast();
	}
}

public class ProjectileData : AbilityData {
}