using UnityEngine;
using System.Collections.Generic;

public class GermaniumPicker : MonoBehaviour {
	protected Germanium held;
	
	public void OnTriggerEnter(Collider other) {
		Germanium germanium = other.GetComponent<Germanium>();
		if (germanium != null && held == null && germanium.Carryable) {
			held = germanium;
			other.transform.parent = transform;
			other.enabled = false;
			GetComponent<CharacterEventListener>().Broadcast(CharacterEvents.PickUp, new CharacterEvent());
		}
	}
	
	public Germanium TakeForProcessing() {
		Germanium g = held;
		held = null;
		return g;
	}
}