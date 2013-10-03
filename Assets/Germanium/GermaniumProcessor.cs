using UnityEngine;
using System.Collections.Generic;

public class GermaniumProcessor : MonoBehaviour {
	protected const float EFFICIENCY = 0.9f;
	
	protected Target target;
	
	public void Awake() {
		target = GetComponent<Target>();
	}
	
	public void OnTriggerEnter(Collider other) {
		GermaniumPicker picker = other.GetComponent<GermaniumPicker>();
		if (picker == null) {
			return;
		}
		Germanium nugget = picker.TakeForProcessing();
		if (nugget != null) {
			GermaniumTracker.Singleton()[target.Team] += nugget.Resource;
			Destroy(nugget.gameObject);
		}
	}
}