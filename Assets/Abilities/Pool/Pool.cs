using UnityEngine;
using System.Collections;

public class Pool : Ability {
	protected const string RESOURCE_NAME = "Pool";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected const float DEFAULT_DPS_RATIO = 1f;
	protected const Slot DEFAULT_SLOT = Slot.Chest;
	protected const Element DEFAULT_ELEMENT = Element.Hack;
	protected const float DEFAULT_COST_PER_DAMAGE_PER_SECOND = 1.5f;
	
	protected float costPerDamagePerSecond;
	protected float damagePerSecondRatio;
	protected Slot slot;
	protected Element element;
	
	public float duration;
	
	public Pool(StatManager s) : this(s, DEFAULT_COST_PER_DAMAGE_PER_SECOND, DEFAULT_DPS_RATIO, DEFAULT_SLOT, DEFAULT_ELEMENT){}
	public Pool(StatManager s, float costPerDamagePerSecondRatio, float damagePerSecondRatio, Slot slot, Element element) : base(s){
		this.costPerDamagePerSecond = costPerDamagePerSecondRatio;
		this.damagePerSecondRatio = damagePerSecondRatio;
		this.slot = slot;
		this.element = element;
	}
	
	protected override void Cast (Vector3 t) {
		GameObject pool = (GameObject)Object.Instantiate(prefab);
		pool.transform.position = transform.position;
		pool.GetComponent<DamageDuringContact>().DamagePerSecond = DamagePerSecond;
		pool.GetDamageDealer().Owner = gameObject;
		pool.GetComponent<Duration>().Lifetime = duration;
	}
	
	protected override int Cost () {
		return (int)(CostPerSecond * duration);
	}
	
	protected float CostPerSecond {
		get {
			return DamagePerSecond.Magnitude * costPerDamagePerSecond;
		}
	}
	
	protected Damage DamagePerSecond {
		get {
			StatType stat = slot.Boosts();
			int magnitude = Mathf.FloorToInt(damagePerSecondRatio * statManager.GetCurrent(stat));
			return new Damage(){Magnitude = magnitude, Type = element};
		}
	}
	
	public override void Update () {}
}
