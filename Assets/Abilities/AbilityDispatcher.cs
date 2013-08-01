using UnityEngine;
using System.Collections;

public class AbilityDispatcher : MonoBehaviour {
	public Castable QAbility;
	public Castable WAbility;
	public Castable EAbility;
	public Castable RAbility;
	public Castable LeftClickAbility;
	public Castable RightClickAbility;
	
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