using UnityEngine;
using System;
using System.Collections.Generic;

public class ProjectileAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		DamageDealer dd = gameObject.GetDamageDealer();
		GameObject owner = dd.Owner;
		StatManager s = owner.GetStatManager();
		DoA doa = new DoA(s, gameObject);
		yield return new Move(s, gameObject){Speed = 30, Range = 30, ContinueToRange = true, OnArrival = doa};
	}
}