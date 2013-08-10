using UnityEngine;
using System.Collections;

public class Beam : Ability {
	
	protected const int HALF_BEAM_LENGTH = 15;
	
	public GameObject prefab;
	public int costPerSecond;
	
	protected bool castThisFrame;
	protected GameObject beam;
	protected float costRemainder;
	
	protected override int Cost () {
		float frameCost = Time.deltaTime * costPerSecond + costRemainder;
		costRemainder = frameCost % 1;
		return (int)frameCost;
	}
	
	protected override void Cast (Vector3 target) {
		if (beam == null) {
			beam = (GameObject)Instantiate(prefab);
			beam.layer = gameObject.layer;
		}
		castThisFrame = true;
		target.y = transform.position.y;
		Vector3 delta = target - transform.position;
		beam.transform.position = transform.position + delta.normalized * HALF_BEAM_LENGTH;
		beam.transform.rotation = Quaternion.LookRotation(delta);
	}
	
	public void Update() {
		if (!castThisFrame && beam != null) {
			Destroy(beam);
		}
		castThisFrame = false;
	}
}