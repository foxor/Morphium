using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Ability {
	protected abstract void Cast(Vector3 target);
	protected abstract int Cost();
	
	public bool requiresPress;
	
	public float cooldown;
	public float castTime;
	
	protected float castComplete;
	protected float nextIdle;
	protected StatManager statManager;
	protected GameObject gameObject;
	
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
	
	public Ability (StatManager statManager) : this(statManager, statManager.gameObject) {
	}
	
	public Ability (StatManager statManager, GameObject host) {
		this.statManager = statManager;
		this.gameObject = host;
	}
	
	public bool TryCast(bool pressedThisFrame, Vector3 target) {
		if (castState == CastState.Idle && 
				(pressedThisFrame || !requiresPress)) {
			int cost = Cost();
			if (cost == 0 || statManager == null || statManager.GetCurrent(StatType.Morphium) > cost) {
				statManager.PayCost(cost);
				castComplete = Time.time + castTime;
				nextIdle = castComplete + cooldown;
				Cast(target);
				return true;
			}
		}
		return false;
	}
	
	public abstract void Update();
	
	protected Transform transform {
		get {
			return gameObject.transform;
		}
	}
	
	protected Renderer renderer {
		get {
			return gameObject.renderer;
		}
	}
}