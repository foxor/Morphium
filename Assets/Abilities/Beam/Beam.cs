using UnityEngine;
using System.Collections;

public class Beam : Ability {
	
	protected const int HALF_BEAM_LENGTH = 15;
	protected const string RESOURCE_NAME = "Beam";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected const float DEFAULT_DPS_RATIO = 1f;
	protected const Slot DEFAULT_SLOT = Slot.Chest;
	protected const Element DEFAULT_ELEMENT = Element.Physical;
	protected const float DEFAULT_COST_PER_DAMAGE_PER_SECOND = 1.5f;
	
	protected float costPerDamagePerSecond;
	protected float damagePerSecondRatio;
	protected Slot slot;
	protected Element element;
	
	protected bool castThisFrame;
	protected GameObject beam;
	protected float costRemainder;
	
	public Beam(StatManager s) : this(s, DEFAULT_DPS_RATIO, DEFAULT_SLOT, DEFAULT_ELEMENT, DEFAULT_COST_PER_DAMAGE_PER_SECOND){}
	public Beam(StatManager s, float damagePerSecondRatio, Slot slot, Element element, float costPerDamagePerSecond) : base(s){
		this.damagePerSecondRatio = damagePerSecondRatio;
		this.element = element;
		this.slot = slot;
		this.costPerDamagePerSecond = costPerDamagePerSecond;
	}
	
	protected override int Cost () {
		float frameCost = Time.deltaTime * CostPerSecond + costRemainder;
		costRemainder = frameCost % 1;
		return (int)frameCost;
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
	
	protected override void Cast (Vector3 target) {
		if (beam == null) {
			beam = (GameObject)Object.Instantiate(prefab);
			beam.GetDamageDealer().Owner = statManager.gameObject;
		}
		castThisFrame = true;
		target.y = statManager.transform.position.y;
		Vector3 delta = target - statManager.transform.position;
		beam.transform.position = statManager.transform.position + delta.normalized * HALF_BEAM_LENGTH;
		beam.transform.rotation = Quaternion.LookRotation(delta);
		beam.GetComponent<DamageDuringContact>().DamagePerSecond = DamagePerSecond;
	}
	
	public override void Update() {
		if (!castThisFrame && beam != null) {
			Object.Destroy(beam);
		}
		castThisFrame = false;
	}
}