using UnityEngine;
using System;
using System.Collections.Generic;

public class MoveDude : Ability {
	
	public float speed;
	
	public override void Cast () {
		Nullable<Vector3> delta = Delta;
		if (delta.HasValue) {
			Vector3 normlizedDelta = delta.Value.normalized * speed * Time.deltaTime;
			if (delta.Value.sqrMagnitude < normlizedDelta.sqrMagnitude) {
				transform.position += delta.Value;
			}
			else {
				transform.position += normlizedDelta;
			}
		}
	}
}