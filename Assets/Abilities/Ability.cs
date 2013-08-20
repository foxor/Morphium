using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {
	protected abstract void Cast(Vector3 target);
	protected abstract int Cost();
	
	public bool requiresPress;
	
	public float cooldown;
	public float castTime;
	
	protected float castComplete;
	protected float nextIdle;
	protected StatManager statManager;
	
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
	
	public void Awake() {
		statManager = GetComponent<StatManager>();
	}
	
	public void TryCast(bool pressedThisFrame, Vector3 target) {
		if (castState == CastState.Idle && 
				(pressedThisFrame || !requiresPress)) {
			int cost = Cost();
			if (cost == 0 || statManager == null || statManager.GetCurrent(StatType.Morphium) > cost) {
				if (statManager != null && cost > 0) {
					statManager.DealDamage(new Damage(){Magnitude = cost, Type = Element.Plasma}, false, null);
				}
				castComplete = Time.time + castTime;
				nextIdle = castComplete + cooldown;
				Cast(target);
			}
		}
	}
}