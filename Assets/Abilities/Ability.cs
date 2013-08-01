using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	public abstract void Cast();
	
	public Nullable<Vector3> Delta {
		get {
			Nullable<RaycastHit> rayCast = ClickRaycast.GetLastHit();
			if (rayCast.HasValue) {
				Vector3 val = rayCast.Value.point - transform.position;
				val.y = 0f;
				return val;
			}
			return null;
		}
	}
}