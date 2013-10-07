using UnityEngine;
using System;
using System.Collections.Generic;

public class Target : MonoBehaviour {
	public int Team;
	
	public void SetTeam(int newTeam) {
		Team = newTeam;
		TargetManager.AddTarget(this);
	}
	
	public void Start() {
		TargetManager.AddTarget(this);
	}
	
	public void OnDisable() {
		TargetManager.RemoveTarget(this);
	}
	
	public void OnDestroy() {
		TargetManager.RemoveTarget(this);
	}
}