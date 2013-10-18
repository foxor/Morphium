using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityDispatcher : MonoBehaviour {
	protected delegate bool trigger();
	
	protected AbilityProvider Provider;
	protected Dictionary<trigger, int> SkillMapping;
	
	public void Awake() {
		Provider = this.GetProvider();
		SkillMapping = new Dictionary<trigger, int>() {
			{CaptureKey(KeyCode.Q), 0},
			{CaptureKey(KeyCode.W), 2},
			{CaptureKey(KeyCode.E), 3},
			{CaptureKey(KeyCode.R), 4},
			{CaptureMouse(1), 1},
			{CaptureMouse(0), 5}
		};
	}
	
	protected trigger CaptureKey(KeyCode key) {
		return () => Input.GetKey(key);
	}
	
	protected trigger CaptureMouse(int button) {
		return () => Input.GetMouseButton(button);
	}
	
	public void Start() {
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Equip, ResetAbilityProvider);
	}
	
	public void ResetAbilityProvider(CharacterEvent data) {
		Provider.Reset();
	}
	
	public void Update() {
		Nullable<RaycastHit> raycast = ClickRaycast.GetLastHit();
		if (raycast == null || Wall.isWall(raycast.Value)) {
			return;
		}
		foreach (trigger trigger in SkillMapping.Keys) {
			if (trigger() && Provider.Abilities[SkillMapping[trigger]] != null) {
				Provider.Abilities[SkillMapping[trigger]].TryCast(raycast.Value.point);
			}
		}
	}
}