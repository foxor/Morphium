using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CharacterEventListener))]
[RequireComponent(typeof(DeathHandler))]
public abstract class StatManager : MonoBehaviour {
	protected const float REGEN_TIMER = 8f;
	
	protected Dictionary<StatType, Stat> stats;
	protected CharacterEventListener listener;
	protected DeathHandler deathHandler;
	
	public void Awake() {
		listener = GetComponent<CharacterEventListener>();
		deathHandler = GetComponent<DeathHandler>();
		stats = new Dictionary<StatType, Stat>();
	}
	
	public void Start() {
		Reset(null);
		listener.AddCallback(CharacterEvents.Kill, AwardKill);
		listener.AddCallback(CharacterEvents.Equip, Reset);
		listener.AddCallback(CharacterEvents.Destroy, UnregisterGlobal);
		GlobalEventListener.Listener().AddCallback(Level.Shop, Reset);
	}
	
	protected void UnregisterGlobal(System.Object data) {
		GlobalEventListener.Listener().RemoveCallback(Level.Shop, Reset);
	}
	
	public void DealDamage(Damage damage, bool stopRegen, DamageDealer damageDealer) {
		if (damage.Magnitude == 0 || this == null || deathHandler.IsDead) {
			return;
		}
		
		Stat damaged = stats[damage.Type.Damages()];
		if (damaged.Current == damaged.Max) {
			damaged.NextRegenTick = Time.time + damaged.SingleTickRegenTimer + (stopRegen ? damaged.RegenCooldown : 0f);
		}
		else if (stopRegen) {
			damaged.NextRegenTick += damaged.RegenCooldown;
		}
		
		damaged.Current -= damage.Magnitude;
		if (damaged.Current <= 0) {
			if (damageDealer != null && damageDealer.Owner != null) {
				CharacterEventListener killer = damageDealer.Owner.GetComponent<CharacterEventListener>();
				if (killer != null) {
					killer.Broadcast(CharacterEvents.Kill, new CharacterEvent(){other = gameObject});
				}
			}
			
			listener.Broadcast(CharacterEvents.Die, null);
		}
	}
	
	protected abstract Dictionary<StatType, int> GetBoosts();
	
	public void Reset(System.Object data) {
		Dictionary<StatType, int> boosts = GetBoosts();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			stats[statType] = new Stat(){
				Max = boosts[statType],
				Current = boosts[statType],
				SingleTickRegenTimer = REGEN_TIMER / ((float)boosts[statType]),
				NextRegenTick = Time.time,
				Regenerates = statType != StatType.Health
			};
		}
	}
	
	public virtual void AwardKill(CharacterEvent killed) {
	}
	
	public int GetMax(StatType stat) {
		return stats[stat].Max;
	}
	
	public int GetCurrent(StatType stat) {
		return stats[stat].Current;
	}
	
	public void Update() {
		foreach (Stat s in stats.Values) {
			while (s.Current < s.Max && s.Regenerates && s.NextRegenTick < Time.time) {
				s.Current ++;
				s.NextRegenTick += s.SingleTickRegenTimer;
			}
		}
	}
}

public static class StatHelpers {
	private static List<Type> ProviderTypes = AppDomain.CurrentDomain.GetAssemblies()
		.SelectMany(assembly => assembly.GetTypes())
		.Where(type => type.IsSubclassOf(typeof(StatManager))).ToList();
	public static StatManager GetStatManager(this GameObject go) {
		return ProviderTypes
			.Select(type => (StatManager)go.GetComponent(type))
			.Where(x => x != null).Single();
	}
	public static StatManager GetStatManager(this MonoBehaviour go) {
		return ProviderTypes.Select(type => (StatManager)go.GetComponent(type)).Where(x => x != null).Single();
	}
}