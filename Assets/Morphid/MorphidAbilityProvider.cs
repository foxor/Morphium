using UnityEngine;
using System;
using System.Collections.Generic;

public class MorphidAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		StatManager s = this.GetStatManager();
		yield return new Projectile(s){castTime = 0.2f, cooldown = 1f};
		yield return new Beam(s);
		yield return new Pool(s);
		yield return new Move(s){Speed = 30, cooldown = 2f, cost = 9};
		yield return new Move(s){Speed = 10};
		yield return new Move(s){Speed = 10};
	}
}