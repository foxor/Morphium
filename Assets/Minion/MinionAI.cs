using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MinionAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	protected const float STRAFE_RADIUS = 7f;
	
	protected class MoveTowards : AI.Goal {
		public Vector3 Destination {
			get; set;
		}
	}
	
	protected class Attack : AI.Goal {
		public Target Target {
			get; set;
		}
	}
	
	protected Vector3 longTermGoal;
	public Vector3 LongTermGoal {
		set {
			longTermGoal = value;
			StartCoroutine(SetupLongTermGoal());
		}
	}
	
	protected Qualifier teamSelector;
	
	public void Start() {
		teamSelector = TargetManager.IsOpposing(GetComponent<Target>());
	}
	
	protected IEnumerator SetupLongTermGoal() {
		while (goals == null) {
			yield return 0;
		}
		goals.Clear();
		goals.Push(new MoveTowards(){Destination = longTermGoal});
	}
	
	protected override Goal Reevaluate () {
		Target target = TargetManager.GetTargets(teamSelector)
			.Where(x => x.gameObject != this.gameObject)
			.Where(x => 
				(x.gameObject.transform.position - transform.position)
				.sqrMagnitude < SQUARED_AGGRO_RANGE)
			.OrderBy(x => Random.Range(0f, 1f))
			.ElementAtOrDefault(0);
		
		if (target == null) {
			return null;
		}
		
		movement.Stop();
		Vector2 strafeDelta = Random.insideUnitCircle.normalized * STRAFE_RADIUS;
		movement.TryCast(true, new Vector3(strafeDelta.x, transform.position.y, strafeDelta.y));
		return new Attack(){Target = target};
	}

	protected override void Process (Goal goal) {
		if (goal is Attack) {
			projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		}
		else if (goal is MoveTowards) {
			movement.TryCast(true, ((MoveTowards)goal).Destination);
		}
	}
}