using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MinionAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	protected const float STRAFE_RADIUS = 7f;
	protected const float SQUARED_STOP_RANGE = 0.5f;
	protected const float WAYPOINT_SPACING = 15f;
	
	protected class MoveTowards : AI.Goal {
		public Vector3 Destination {
			get; set;
		}
	}
	
	protected class Juke : AI.Goal {
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
		float lerpDelta = WAYPOINT_SPACING / (longTermGoal - transform.position).magnitude;
		for (float lerp = 1f; lerp >= 0f; lerp -= lerpDelta) {
			goals.Push(new MoveTowards(){Destination = Vector3.Lerp(transform.position, longTermGoal, lerp)});
		}
	}
	
	protected override void Reevaluate () {
		if (!goals.Any()) {
			goals.Push(new MoveTowards(){Destination = longTermGoal});
		}
		if (goals.Peek() is Attack) {
			Target currentTarget = ((Attack)goals.Peek()).Target;
			if (currentTarget == null || (currentTarget.transform.position - transform.position).sqrMagnitude > SQUARED_AGGRO_RANGE) {
				goals.Pop();
			}
		}
		
		if (goals.Peek() is MoveTowards) {
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
			Vector2 strafeDelta = Random.insideUnitCircle.normalized * STRAFE_RADIUS;
			goals.Push(new Juke(){Destination = transform.position + new Vector3(strafeDelta.x, 0f, strafeDelta.y)});
		}
	}

	protected override bool Process (Goal goal) {
		if (goal is Attack) {
			if (((Attack)goal).Target == null) {
				return false;
			}
			projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		}
		else if (goal is MoveTowards || goal is Juke) {
			Vector3 destination = goal is MoveTowards ? ((MoveTowards)goal).Destination : ((Juke)goal).Destination;
			if ((destination - transform.position).sqrMagnitude < SQUARED_STOP_RANGE) {
				movement.Stop();
				return false;
			}
			movement.TryCast(true, destination);
		}
		return true;
	}
}