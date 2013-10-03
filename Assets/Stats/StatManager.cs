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
	protected Target target;
	
	protected Queue<StatType> awaitingRegen;
	
	public void Awake() {
		listener = GetComponent<CharacterEventListener>();
		deathHandler = GetComponent<DeathHandler>();
		target = GetComponent<Target>();
		stats = new Dictionary<StatType, Stat>();
		awaitingRegen = new Queue<StatType>();
	}
	
	public void Start() {
		Reset(null);
		listener.AddCallback(CharacterEvents.Kill, AwardKill);
		listener.AddCallback(CharacterEvents.Equip, Reset);
		listener.AddCallback(CharacterEvents.Destroy, UnregisterGlobal);
		listener.AddCallback(CharacterEvents.Hit, DealDamage);
		GlobalEventListener.Listener().AddCallback(Level.Shop, Reset);
		GermaniumTracker.Singleton().AddCallback(GermaniumEvent.MorphiumSpend, AllocateRegen);
	}
	
	protected void UnregisterGlobal(System.Object data) {
		GlobalEventListener.Listener().RemoveCallback(Level.Shop, Reset);
	}
	
	protected void AllocateRegen(GermaniumLevel data) {
		if (data.Team == target.Team) {
			while (data.Level > 0 && awaitingRegen.Any()) {
				Stat toRegen = stats[awaitingRegen.Peek()];
				if (data.Level >= toRegen.Max - toRegen.AllocatedRegen) {
					data.Level -= toRegen.Max - toRegen.AllocatedRegen;
					toRegen.AllocatedRegen = toRegen.Max;
					awaitingRegen.Dequeue();
				}
				else {
					toRegen.AllocatedRegen += data.Level;
					data.Level = 0;
				}
			}
		}
	}
	
	protected void DealDamage(CharacterEvent data) {
		DealDamage(((HitEvent)data).Damage, true, ((HitEvent)data).Source);
	}
	
	public void PayCost(int cost) {
		if (cost > 0) {
			DealDamage(new Damage(){Magnitude = cost, Type = Element.Plasma}, false, null);
		}
	}
	
	protected void DealDamage(Damage damage, bool stopRegen, DamageDealer damageDealer) {
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
		damaged.AllocatedRegen -= damage.Magnitude;
		
		if (damaged.Current <= 0) {
			if (damageDealer != null && damageDealer.Owner != null) {
				CharacterEventListener killer = damageDealer.Owner.GetComponent<CharacterEventListener>();
				if (killer != null) {
					killer.Broadcast(CharacterEvents.Kill, new KillEvent(){Other = gameObject});
				}
			}
			
			listener.Broadcast(CharacterEvents.Die, null);
		}
		else {
			if (GermaniumTracker.Singleton()[target.Team] > damaged.Max - damaged.AllocatedRegen) {
				GermaniumTracker.Singleton()[target.Team] -= damaged.Max - damaged.AllocatedRegen;
				damaged.AllocatedRegen = damaged.Max;
			}
			else {
				damaged.AllocatedRegen += GermaniumTracker.Singleton()[target.Team];
				GermaniumTracker.Singleton()[target.Team] = 0;
				awaitingRegen.Enqueue(damage.Type.Damages());
			}
		}
	}
	
	protected abstract Dictionary<StatType, int> GetBoosts();
	
	public void Reset(System.Object data) {
		Dictionary<StatType, int> boosts = GetBoosts();
		foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
			stats[statType] = new Stat(){
				Max = boosts[statType],
				Current = boosts[statType],
				AllocatedRegen = boosts[statType],
				SingleTickRegenTimer = REGEN_TIMER / ((float)boosts[statType]),
				NextRegenTick = Time.time
			};
		}
	}
	
	public virtual void AwardKill(CharacterEvent killed) {
		GermaniumTracker.Singleton()[target.Team] += 10;
	}
	
	public int GetMax(StatType stat) {
		return stats[stat].Max;
	}
	
	public int GetCurrent(StatType stat) {
		return stats[stat].Current;
	}
	
	public void Update() {
		foreach (Stat s in stats.Values) {
			while (s.Current < s.AllocatedRegen && s.NextRegenTick < Time.time) {
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