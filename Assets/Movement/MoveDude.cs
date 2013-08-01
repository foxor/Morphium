using UnityEngine;
using System;
using System.Collections.Generic;

public class MoveDude : MonoBehaviour {
	public float speed;
	public void Update() {
		if (Input.GetMouseButton(0)) {
			Nullable<RaycastHit> rayCast = ClickRaycast.GetLastHit();
			if (rayCast.HasValue) {
				Vector3 delta = rayCast.Value.point - transform.position;
				delta.y = 0f;
				Vector3 normlizedDelta = delta.normalized * speed * Time.deltaTime;
				if (delta.sqrMagnitude < normlizedDelta.sqrMagnitude) {
					transform.position += delta;
				}
				else {
					transform.position += normlizedDelta;
				}
			}
		}
	}
}