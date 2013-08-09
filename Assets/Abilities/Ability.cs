using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	protected abstract void Cast(Vector3 target);
	
	public bool requiresPress;
	
	public float cooldown;
	public float castTime;
	
	protected float castComplete;
	protected float nextIdle;
	
	public enum CastState {
		Idle,
		Casting,
		Cooldown
	}
	
	public CastState castState {
		get {
			if (Time.time > nextIdle) {
				return CastState.Idle;
			}
			else if (Time.time > castComplete) {
				return CastState.Cooldown;
			}
			return CastState.Casting;
		}
	}
	
	public void TryCast(bool pressedThisFrame, Vector3 target) {
		if (castState == CastState.Idle && 
				(pressedThisFrame || !requiresPress)) {
			castComplete = Time.time + castTime;
			nextIdle = castComplete + cooldown;
			Cast(target);
		}
	}
}