using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurretAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	
	protected class Attack : AI.Goal {
		public Target Target {
			get; set;
		}
	}
	
	protected Qualifier teamSelector;
	protected Projectile projectile;
	
	public void Start() {
		projectile = this.GetProvider().GetAbility<Projectile>();
		teamSelector = TargetManager.IsOpposing(GetComponent<Target>());
	}
	
	protected override void Reevaluate () {
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
		if (((Attack)goal).Target == null) {
			return false;
		}
		projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		return true;
	}
}