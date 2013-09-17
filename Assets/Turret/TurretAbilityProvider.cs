using UnityEngine;
using System;
using System.Collections.Generic;

public class TurretAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		StatManager s = this.GetStatManager();
		yield return new Projectile(s){castTime = 0.2f, cooldown = 1f, cost = 2};
	}
}