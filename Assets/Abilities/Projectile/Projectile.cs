using UnityEngine;
using System.Collections.Generic;

public class Projectile : Ability {
	
	protected const string RESOURCE_NAME = "Projectile";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	public Damage damage = new Damage(){Magnitude = 3, Type = Element.Physical};
	public int cost;
	protected Queue<GameObject> spawning;
	protected Queue<Vector3> targets;
	
	public Projectile(StatManager s) : base(s){
		spawning = new Queue<GameObject>();
		targets = new Queue<Vector3>();
	}
	
	protected override void Cast (Vector3 t) {
		GameObject projectile = (GameObject)Object.Instantiate(prefab);
		projectile.transform.position = transform.position;
		spawning.Enqueue(projectile);
		targets.Enqueue(t);
	}
	
	protected override int Cost () {
		return cost;
	}
	
	public override void Update () {
		while (spawning.Count > 0 && spawning.Peek() != null) {
			GameObject projectile = spawning.Dequeue();
			projectile.GetDamageDealer().Owner = gameObject;
			AbilityProvider projectileProvider = projectile.GetProvider();
			Move projectileMovement = projectileProvider.GetAbility<Move>();
			projectileMovement.TryCast(true, targets.Dequeue());
			projectile.GetComponent<ProjectileDamage>().damage = damage;
		}
	}
}