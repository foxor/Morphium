using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class AI : MonoBehaviour {
	
	public class Goal {
	}
	
	protected const float EVALUATION_TIMER = 0.3f;
	protected const float STRAFE_RADIUS = 7f;
	protected const float ATTACK_OUTER_LIMIT_SQUARED = 10f * 10f;
	
	protected Stack<Goal> goals;
	protected Move movement;
	protected Projectile projectile;
	protected float nextEvaluation;
	
	protected abstract Goal Reevaluate();
	/*{ //TODO: refactor into morphid AI
		target = TargetManager.GetTargets()
			.Where(x => x.gameObject != this.gameObject)
			.OrderBy(x => 
				(x.gameObject.transform.position - transform.position)
				.sqrMagnitude)
			.ElementAt(0);
		
		if (target == null) {
			return State.Idle;
		}
		
		Vector3 delta = target.transform.position - transform.position;
		if (delta.sqrMagnitude < ATTACK_OUTER_LIMIT_SQUARED) {
			movement.Stop();
			Vector2 strafeDelta = Random.insideUnitCircle.normalized * STRAFE_RADIUS;
			movement.TryCast(true, new Vector3(strafeDelta.x, transform.position.y, strafeDelta.y));
			return State.Attack;
		}
		return State.Approach;
	}*/
	
	protected abstract void Process(Goal goal);
	
	public void Awake() {
		nextEvaluation = 0f;
		movement = GetComponent<Move>();
		projectile = GetComponent<Projectile>();
		goals = new Stack<Goal>();
	}
	
	protected void ResetTimer() {
		nextEvaluation = Time.time + EVALUATION_TIMER + Random.Range(0f, EVALUATION_TIMER);
	}
	
	public void Update() {
		if (Time.time > nextEvaluation) {
			ResetTimer();
			Goal newGoal = Reevaluate();
			if (newGoal != null) {
				goals.Push(newGoal);
			}
		}
		
		if (goals.Any()) {
			Process(goals.Peek());
		}
	}
}