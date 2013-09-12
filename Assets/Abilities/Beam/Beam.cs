using UnityEngine;
using System.Collections;

public class Beam : Ability {
	
	protected const int HALF_BEAM_LENGTH = 15;
	protected const string RESOURCE_NAME = "Beam";
	protected static GameObject prefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	public int costPerSecond;
	
	protected bool castThisFrame;
	protected GameObject beam;
	protected float costRemainder;
	
	public Beam(StatManager s) : base(s){}
	
	protected override int Cost () {
		float frameCost = Time.deltaTime * costPerSecond + costRemainder;
		costRemainder = frameCost % 1;
		return (int)frameCost;
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
	}
	
	public override void Update() {
		if (!castThisFrame && beam != null) {
			Object.Destroy(beam);
		}
		castThisFrame = false;
	}
}