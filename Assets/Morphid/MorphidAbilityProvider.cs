using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MorphidAbilityProvider : AbilityProvider {
	protected override IEnumerable<Ability> abilitySource () {
		return GetComponent<ItemManager>().Abilities().Concat(innateAbilities());
	}
	
	protected IEnumerable<Ability> innateAbilities () {
		StatManager s = GetComponent<StatManager>();
		yield return new Move(s){SpeedFactor = 6f};
	}
}