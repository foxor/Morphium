using UnityEngine;
using System;
using System.Collections;

public class AbilityDispatcher : MonoBehaviour {
	protected AbilityProvider Provider;
	
	public void Awake() {
		Provider = this.GetProvider();
	}
	
	public void Update() {
		Nullable<RaycastHit> raycast = ClickRaycast.GetLastHit();
		if (raycast == null || Wall.isWall(raycast.Value)) {
			return;
		}
		if (Input.GetKey(KeyCode.Q)) {
			Provider.Abilities[0].TryCast(Input.GetKeyDown(KeyCode.Q), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.W)) {
			Provider.Abilities[1].TryCast(Input.GetKeyDown(KeyCode.W), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.E)) {
			Provider.Abilities[2].TryCast(Input.GetKeyDown(KeyCode.E), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.R)) {
			Provider.Abilities[3].TryCast(Input.GetKeyDown(KeyCode.R), raycast.Value.point);
		}
		if (Input.GetMouseButton(0)) {
			Provider.Abilities[4].TryCast(Input.GetMouseButtonDown(0), raycast.Value.point);
		}
		if (Input.GetMouseButton(1)) {
			Provider.Abilities[5].TryCast(Input.GetMouseButtonDown(1), raycast.Value.point);
		}
	}
}