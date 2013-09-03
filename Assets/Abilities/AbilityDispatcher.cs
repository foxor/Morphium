using UnityEngine;
using System;
using System.Collections;

public class AbilityDispatcher : MonoBehaviour {
	public Ability QAbility;
	public Ability WAbility;
	public Ability EAbility;
	public Ability RAbility;
	public Ability LeftClickAbility;
	public Ability RightClickAbility;
	
	public void Update() {
		Nullable<RaycastHit> raycast = ClickRaycast.GetLastHit();
		if (raycast == null || Wall.isWall(raycast.Value)) {
			return;
		}
		if (Input.GetKey(KeyCode.Q)) {
			QAbility.TryCast(Input.GetKeyDown(KeyCode.Q), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.W)) {
			WAbility.TryCast(Input.GetKeyDown(KeyCode.W), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.E)) {
			EAbility.TryCast(Input.GetKeyDown(KeyCode.E), raycast.Value.point);
		}
		if (Input.GetKey(KeyCode.R)) {
			RAbility.TryCast(Input.GetKeyDown(KeyCode.R), raycast.Value.point);
		}
		if (Input.GetMouseButton(0)) {
			LeftClickAbility.TryCast(Input.GetMouseButtonDown(0), raycast.Value.point);
		}
		if (Input.GetMouseButton(1)) {
			RightClickAbility.TryCast(Input.GetMouseButtonDown(1), raycast.Value.point);
		}
	}
}