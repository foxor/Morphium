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
	
	protected class Push : AI.Goal {
		public Spawn Spawn {
			get; set;
		}
	}
	
	protected Qualifier teamSelector;
	protected Projectile projectile;
	protected Spawn spawn;
	
	protected int activeMinions;
	
	public void Start() {
		activeMinions = 0;
		projectile = this.GetProvider().GetAbility<Projectile>();
		spawn = this.GetProvider().GetAbility<Spawn>();
		teamSelector = TargetManager.IsOpposing(GetComponent<Target>());
		goals.Push(new Push(){Spawn = null});
	}
	
	protected override void Reevaluate () {
		if (goals.Peek().GetType() = typeof(Attack) &&
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

	protected override bool Process (Goal goal) {
		if (goal.GetType() == typeof(Attack)) {
			if (((Attack)goal).Target == null) {
				return false;
			}
			projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		}
		if (goal.GetType() == typeof(Push)) {
			
		}
		return true;
	}
}