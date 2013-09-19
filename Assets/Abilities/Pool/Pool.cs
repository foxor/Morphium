using UnityEngine;
using System.Collections;

public class Pool : Ability {
	protected const string RESOURCE_NAME = "Pool";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	public Damage damage = new Damage(){Magnitude = 3, Type = Element.Physical};
	public int cost;
	public float duration;
	
	public Pool(StatManager s) : base(s){}
	
	protected override void Cast (Vector3 t) {
		GameObject pool = (GameObject)Object.Instantiate(prefab);
		pool.transform.position = transform.position;
		//FIXME
		//pool.GetComponent<DamageDuringContact>().damagePerSecond = damage;
		pool.GetDamageDealer().Owner = gameObject;
		pool.GetComponent<Duration>().Lifetime = duration;
	}
	
	protected override int Cost () {
		return cost;
	}
	
	public override void Update () {}
}
