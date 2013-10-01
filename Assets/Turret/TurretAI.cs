using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurretAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	protected const int MINION_COUNT = 8;
	
	protected class Attack : AI.Goal {
		public Target Target {
			get; set;
		}
	}
	
	protected Qualifier teamSelector;
	protected Target target;
	protected Projectile projectile;
	protected Spawn spawn;
	
	protected Ability ability;
	
	protected List<GameObject> activeMinions;
	
	public void Start() {
		activeMinions = new List<GameObject>();
		projectile = this.GetProvider().GetAbility<Projectile>();
		spawn = this.GetProvider().GetAbility<Spawn>();
		target = GetComponent<Target>();
		teamSelector = TargetManager.IsOpposing(target);
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Hit, Activate);
	}
	
	protected int Team {
		get {
			return target.Team;
		}
		set {
			target.Team = value;
		}
	}
	
	protected void Activate (CharacterEvent data) {
		ability = data.Source.Ability;
		Team = data.Source.Owner.GetComponent<Target>().Team;
		ColorChanger cc = data.Source.Owner.GetComponent<ColorChanger>();
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
			r.material.color = cc.color;
		}
		GetComponent<CharacterEventListener>().RemoveCallback(CharacterEvents.Hit, Activate);
	}
	
	protected override void Reevaluate () {
		if (goals.Any() && goals.Peek().GetType() == typeof(Attack) &&
			(((Attack)goals.Peek()).Target.transform.position - transform.position)
			.sqrMagnitude > SQUARED_AGGRO_RANGE)
		{
			goals.Pop();
		}
		
		Target target = TargetManager.GetTargets(teamSelector)
			.Where(x => x.gameObject != this.gameObject)
			.Where(x => 
				(x.gameObject.transform.position - transform.position)
				.sqrMagnitude < SQUARED_AGGRO_RANGE)
			.OrderBy(x => Random.Range(0f, 1f))
			.ElementAtOrDefault(0);
		
		if (target == null) {
			return;
		}
		
		goals.Push(new Attack(){Target = target});
	}

	protected override bool ProcessGoal (Goal goal) {
		if (goal.GetType() == typeof(Attack)) {
			if (((Attack)goal).Target == null) {
				return false;
			}
			projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		}
		return true;
	}
	
	protected override void Process () {
		if (spawn.castState == Ability.CastState.Idle) {
			activeMinions.RemoveAll(x => x == null);
			if (activeMinions.Count < MINION_COUNT && spawn.TryCast(true, Vector3.zero)) {
				activeMinions.Add(spawn.LastSpawn);
			}
		}
	}
}