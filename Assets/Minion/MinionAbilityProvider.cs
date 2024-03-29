using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent (typeof(StatManager))]
public class MinionAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		StatManager s = this.GetStatManager();
		yield return new Projectile(s, Slot.Arm, Element.Physical, 0.5f, 0f){castTime = 0.2f, cooldown = 1f};
		yield return new Move(s){SpeedFactor = 10f / Mathf.Log(10f)};
	}
}