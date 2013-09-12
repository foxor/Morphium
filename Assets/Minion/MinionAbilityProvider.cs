using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent (typeof(StatManager))]
public class MinionAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		StatManager s = this.GetStatManager();
		yield return new Projectile(s){castTime = 0.2f, cooldown = 1f, cost = 2};
		yield return new Move(s){Speed = 10};
	}
}