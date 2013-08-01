using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	public abstract void Cast();
	
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
	
	public void TryCast(bool pressedThisFrame) {
		if (castState == CastState.Idle && 
				(pressedThisFrame || !requiresPress)) {
			castComplete = Time.time + castTime;
			nextIdle = castComplete + cooldown;
			Cast();
		}
	}
	
	public Nullable<Vector3> Delta {
		get {
			Nullable<RaycastHit> rayCast = ClickRaycast.GetLastHit();
			if (rayCast.HasValue) {
				Vector3 val = rayCast.Value.point - transform.position;
				val.y = 0f;
				return val;
			}
			return null;
		}
	}
}