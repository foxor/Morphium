using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurretAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	protected const int MINION_COUNT = 15;
	
	protected class Attack : AI.Goal {
		public Target Target {
			get; set;
		}
	}
	
	protected Qualifier teamSelector;
	protected Target target;
	protected Projectile projectile;
	protected Spawn spawn;
	protected Lane lane;
	protected ColorChanger colorChanger;
	
	protected Ability ability;
	
	protected List<GameObject> activeMinions;
	
	public void Start() {
		activeMinions = new List<GameObject>();
		projectile = this.GetProvider().GetAbility<Projectile>();
		spawn = this.GetProvider().GetAbility<Spawn>();
		colorChanger = GetComponent<ColorChanger>();
		target = GetComponent<Target>();
		spawn.Enable(target.Team, new LaneElement[]{GetComponent<LaneElement>()}, colorChanger.color);
		teamSelector = TargetManager.IsOpposing(target);
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Hit, Activate);
		lane = GetComponent<Lane>();
	}
	
	protected int Team {
		get {
			return target.Team;
		}
		set {
			target.SetTeam(value);
		}
	}
	
	public void HackActivate (int newTeam) {
		Team = newTeam;
		spawn.Enable(Team, lane.Enumerator(), colorChanger.color);
		GetComponent<CharacterEventListener>().RemoveCallback(CharacterEvents.Hit, Activate);
	}
	
	protected void Activate (CharacterEvent data) {
		ability = ((HitEvent)data).Source.Ability;
		Team = ((HitEvent)data).Source.Owner.GetComponent<Target>().Team;
		ColorChanger cc = ((HitEvent)data).Source.Owner.GetComponent<ColorChanger>();
		colorChanger.SetColor(cc.color);
		spawn.Enable(Team, lane.Enumerator(), cc.color);
		GetComponent<CharacterEventListener>().RemoveCallback(CharacterEvents.Hit, Activate);
	}
	
	protected override void Reevaluate () {
		if (goals.Any() && goals.Peek().GetType() == typeof(Attack) &&
			(((Attack)goals.Peek()).Target == null || (((Attack)goals.Peek()).Target.transform.position - transform.position)
			.sqrMagnitude > SQUARED_AGGRO_RANGE))
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