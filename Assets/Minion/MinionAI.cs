using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MinionAI : AI {
	
	protected const float SQUARED_AGGRO_RANGE = 81f;
	protected const float STRAFE_RADIUS = 7f;
	protected const float STRAFE_RADIUS_SQUARED = STRAFE_RADIUS * STRAFE_RADIUS;
	protected const float SQUARED_STOP_RANGE = 0.5f;
	protected const float WAYPOINT_SPACING = 15f;
	
	protected class MoveTowards : AI.Goal {
		public Vector3 Destination {
			get; set;
		}
		
		public bool Priority {
			get; set;
		}
	}
	
	protected class Attack : AI.Goal {
		public Target Target {
			get; set;
		}
	}
	
	protected Target longTermGoal;
	public Target LongTermGoal {
		set {
			longTermGoal = value;
			StartCoroutine(SetupLongTermGoal());
		}
	}
	// Essentially a vector2, using our y position
	public Vector3 Destination {
		get {
			return new Vector3(longTermGoal.transform.position.x, transform.position.y, longTermGoal.transform.position.z);
		}
	}
	
	protected Qualifier teamSelector;
	protected Target target;
	protected Move movement;
	protected Projectile projectile;
	protected Vector3 home;
	
	public void Start() {
		target = GetComponent<Target>();
		teamSelector = TargetManager.IsOpposing(target);
		AbilityProvider provider = this.GetProvider();
		movement = provider.GetAbility<Move>();
		projectile = provider.GetAbility<Projectile>();
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.PickUp, OnGermaniumPickup);
	}
	
	protected void OnGermaniumPickup(CharacterEvent data) {
		goals.Push(new MoveTowards(){Destination = home, Priority = true});
	}
	
	protected IEnumerator SetupLongTermGoal() {
		while (goals == null) {
			yield return 0;
		}
		goals.Clear();
		home = transform.position;
		Vector3 destination = Destination;
		if (longTermGoal != null && (longTermGoal.transform.position - transform.position).magnitude > WAYPOINT_SPACING) {
			float lerpDelta = WAYPOINT_SPACING / (destination - transform.position).magnitude;
			for (float lerp = 1f; lerp >= 0f; lerp -= lerpDelta) {
				goals.Push(new MoveTowards(){Destination = Vector3.Lerp(transform.position, destination, lerp)});
			}
		}
	}
	
	protected bool AttackInvalid(Attack goal) {
		Target currentTarget = goal.Target;
		return currentTarget == null || 
				(currentTarget.transform.position - transform.position).sqrMagnitude > SQUARED_AGGRO_RANGE ||
				!projectile.CanPay;
	}
	
	protected override void Reevaluate () {
		// If we don't have anything to do, either patrol near your destination, or move in that direction
		if (!goals.Any()) {
			if ((longTermGoal.transform.position - transform.position).sqrMagnitude < STRAFE_RADIUS_SQUARED) {
				Vector2 strafeDelta = Random.insideUnitCircle.normalized * STRAFE_RADIUS;
				goals.Push(new MoveTowards(){Destination = transform.position + new Vector3(strafeDelta.x, 0f, strafeDelta.y)});
			}
			else {
				goals.Push(new MoveTowards(){Destination = Destination});
			}
		}
		
		if (goals.Peek() is Attack) {
			// If we have someone to attack, check that they haven't wandered off
			if (AttackInvalid((Attack)goals.Peek())) {
				goals.Pop();
			}
		}
		else {
			//If we don't have anyone to attack, check and see if we do
			Target target = TargetManager.GetTargets(teamSelector)
				.Where(x => x.gameObject != this.gameObject)
				.Where(x => 
					(x.gameObject.transform.position - transform.position)
					.sqrMagnitude < SQUARED_AGGRO_RANGE)
				.OrderBy(x => Random.Range(0f, 1f))
				.ElementAtOrDefault(0);
			
			if (target != null) {
				if (!(goals.Peek() is MoveTowards && ((MoveTowards)goals.Peek()).Priority)) {
					Vector2 strafeDelta = Random.insideUnitCircle.normalized * STRAFE_RADIUS;
					goals.Push(new MoveTowards(){Destination = transform.position + new Vector3(strafeDelta.x, 0f, strafeDelta.y)});
				}
				goals.Push(new Attack(){Target = target});
			}
		}
	}

	protected override bool ProcessGoal (Goal goal) {
		if (goal is Attack) {
			if (AttackInvalid((Attack)goal)) {
				return false;
			}
			return !projectile.TryCast(true, ((Attack)goal).Target.transform.position);
		}
		else if (goal is MoveTowards) {
			Vector3 destination = ((MoveTowards)goal).Destination;
			if ((destination - transform.position).sqrMagnitude < SQUARED_STOP_RANGE) {
				movement.Stop();
				return false;
			}
			movement.TryCast(true, destination);
		}
		return true;
	}
	
	protected override void Process () { }
}