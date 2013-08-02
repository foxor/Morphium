using UnityEngine;
using System;
using System.Collections.Generic;

public class Move : Ability {
	
	public float speed;
	public Ability onArrival;
	
	private Nullable<Vector3> delta;
	
	public override void Cast (Vector3 target) {
		target.y = transform.position.y;
		delta = target - transform.position;
	}
	
	public void Update () {
		if (delta != null) {
			Vector3 normlizedDelta = delta.Value.normalized * speed * Time.deltaTime;
			if (delta.Value.sqrMagnitude < normlizedDelta.sqrMagnitude) {
				transform.position += delta.Value;
				delta = null;
				if (onArrival != null) {
					onArrival.Cast(Vector3.zero);
				}
			}
			else {
				transform.position += normlizedDelta;
				delta -= normlizedDelta;
			}
		}
	}
}