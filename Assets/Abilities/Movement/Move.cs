using UnityEngine;
using System;
using System.Collections.Generic;

public class Move : Ability {
	
	public float speed;
	public Ability onArrival;
	public int cost;
	public bool continueToRange;
	public float range;
	
	private Nullable<Vector3> delta;
	
	protected override void Cast (Vector3 target) {
		target.y = transform.position.y;
		delta = target - transform.position;
		transform.rotation = Quaternion.LookRotation(delta.Value.normalized);
		if (continueToRange) {
			delta = delta.Value.normalized * range;
		}
	}
	
	protected override int Cost () {
		return cost;
	}
	
	public void Stop () {
		delta = null;
	}
	
	public void Update () {
		if (delta != null) {
			Vector3 normlizedDelta = delta.Value.normalized * speed * Time.deltaTime;
			if (delta.Value.sqrMagnitude < normlizedDelta.sqrMagnitude) {
				transform.position += delta.Value;
				delta = null;
				if (onArrival != null) {
					onArrival.TryCast(true, Vector3.zero);
				}
			}
			else {
				transform.position += normlizedDelta;
				delta -= normlizedDelta;
			}
		}
	}
}