using UnityEngine;
using System;
using System.Collections.Generic;

public class ClickRaycast : MonoBehaviour {
	private static ClickRaycast singleton;
	
	private RaycastHit lastHit;
	private bool hitSomething;
	
	public static Nullable<RaycastHit> GetLastHit() {
		return singleton.hitSomething ? (Nullable<RaycastHit>)singleton.lastHit : null;
	}
	
	public void Start() {
		singleton = this;
	}
	
	public void Update() {
		if (Camera.main != null) {
			hitSomething = Physics.Raycast(
				Camera.main.ScreenPointToRay(Input.mousePosition),
				out lastHit
			);
		}
	}
}
