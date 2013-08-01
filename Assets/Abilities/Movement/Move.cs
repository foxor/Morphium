using UnityEngine;
using System;
using System.Collections.Generic;

public class Move : Ability<MovementData> {
	
	public float speed;
	
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
			}
			else {
				transform.position += normlizedDelta;
				delta -= normlizedDelta;
			}
		}
	}
}

[Serializable]
public class MovementData : AbilityData {
}