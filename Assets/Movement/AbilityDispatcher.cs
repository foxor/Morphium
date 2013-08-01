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
			QAbility.Cast();
		}
		if (Input.GetKey(KeyCode.W)) {
			WAbility.Cast();
		}
		if (Input.GetKey(KeyCode.E)) {
			EAbility.Cast();
		}
		if (Input.GetKey(KeyCode.R)) {
			RAbility.Cast();
		}
		if (Input.GetMouseButton(0)) {
			LeftClickAbility.Cast();
		}
		if (Input.GetMouseButton(1)) {
			RightClickAbility.Cast();
		}
	}
}