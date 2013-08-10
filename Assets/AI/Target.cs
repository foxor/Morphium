using UnityEngine;
using System;
using System.Collections.Generic;

public class Target : MonoBehaviour {
	public void Start() {
		TargetManager.AddTarget(this);
	}
	
	public void OnDestroy() {
		TargetManager.RemoveTarget(this);
	}
}