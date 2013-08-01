using UnityEngine;
using System.Collections;

public class AbilityDispatcher : MonoBehaviour {
	public Ability QAbility;
	public Ability WAbility;
	public Ability EAbility;
	public Ability RAbility;
	public Ability LeftClickAbility;
	public Ability RightClickAbility;
	
	public void Update() {
		if (Input.GetKey(KeyCode.Q)) {
			QAbility.TryCast(Input.GetKeyDown(KeyCode.Q));
		}
		if (Input.GetKey(KeyCode.W)) {
			WAbility.TryCast(Input.GetKeyDown(KeyCode.W));
		}
		if (Input.GetKey(KeyCode.E)) {
			EAbility.TryCast(Input.GetKeyDown(KeyCode.E));
		}
		if (Input.GetKey(KeyCode.R)) {
			RAbility.TryCast(Input.GetKeyDown(KeyCode.R));
		}
		if (Input.GetMouseButton(0)) {
			LeftClickAbility.TryCast(Input.GetMouseButtonDown(0));
		}
		if (Input.GetMouseButton(1)) {
			RightClickAbility.TryCast(Input.GetMouseButtonDown(1));
		}
	}
}