using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbilityProvider : MonoBehaviour {
	private List<Ability> abilities;
	public List<Ability> Abilities {
		get {
			if (abilities == null) {
				abilities = abilitySource().ToList();
			}
			return abilities;
		}
	}
	
	protected abstract IEnumerable<Ability> abilitySource();
	
	public void Update() {
		foreach (Ability a in Abilities) {
			a.Update();
		}
	}
	
	public void Reset() {
		abilities = abilitySource().ToList();
	}
	
	public T GetAbility<T>() where T : Ability {
		return (T)Abilities.Where(ability => typeof(T).IsAssignableFrom(ability.GetType())).Single();
	}
}

public static class AbilityHelpers {
	private static List<Type> ProviderTypes = AppDomain.CurrentDomain.GetAssemblies()
		.SelectMany(assembly => assembly.GetTypes())
		.Where(type => type.IsSubclassOf(typeof(AbilityProvider))).ToList();
	public static AbilityProvider GetProvider(this GameObject go) {
		return ProviderTypes.Select(type => (AbilityProvider)go.GetComponent(type)).Where(x => x != null).Single();
	}
	public static AbilityProvider GetProvider(this MonoBehaviour go) {
		return ProviderTypes.Select(type => (AbilityProvider)go.GetComponent(type)).Where(x => x != null).Single();
	}
}