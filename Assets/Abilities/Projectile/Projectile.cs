using UnityEngine;
using System.Collections.Generic;

public class Projectile : Ability {
	
	protected const Element DEFAULT_ELEMENT = Element.Physical;
	protected const Slot DEFAULT_SLOT = Slot.Arm;
	protected const float DEFAULT_RATIO = 1f / 3f;
	protected const int DEFAULT_COST = 2;
	
	protected const string RESOURCE_NAME = "Projectile";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected int cost;
	protected float dmgRatio;
	protected Slot slot;
	protected Element element;
	protected Queue<GameObject> spawning;
	protected Queue<Vector3> targets;
	
	public Projectile(StatManager s) : this(s, DEFAULT_SLOT, DEFAULT_ELEMENT, DEFAULT_RATIO, DEFAULT_COST) {}
	
	public Projectile(StatManager s, Slot slot, Element element, float dmgRatio, int cost) : base(s){
		this.cost = cost;
		this.slot = slot;
		this.element = element;
		this.dmgRatio = dmgRatio;
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
			StatType stat = slot.Boosts();
			Damage damage = new Damage(){Magnitude = Mathf.FloorToInt(dmgRatio * statManager.GetCurrent(stat)), Type = element};
			projectile.GetComponent<ProjectileDamage>().damage = damage;
		}
	}
}