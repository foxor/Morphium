using UnityEngine;
using System;
using System.Collections.Generic;

public class Move : Ability {
	
	public float speed;
	public Ability onArrival;
	
	private Nullable<Vector3> delta;
	
	public override void Cast () {
		delta = Delta;
	}
	
	public void Update () {
		if (delta != null) {
			Vector3 normlizedDelta = delta.Value.normalized * speed * Time.deltaTime;
			if (delta.Value.sqrMagnitude < normlizedDelta.sqrMagnitude) {
				transform.position += delta.Value;
				delta = null;
				if (onArrival != null) {
					onArrival.Cast();
				}
			}
			else {
				transform.position += normlizedDelta;
				delta -= normlizedDelta;
			}
		}
	}
}