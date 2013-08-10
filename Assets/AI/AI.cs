using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AI : MonoBehaviour {
	
	public enum State {
		Approach,
		Attack,
		Idle
	}
	
	public const float EVALUATION_TIMER = 0.3f;
	public const float ATTACK_OUTER_LIMIT_SQUARED = 10f * 10f;
	
	protected Target target;
	
	protected Move movement;
	protected Projectile projectile;
	protected State mindstate;
	protected float nextEvaluation;
	
	protected State Reevaluate() {
		target = TargetManager.GetTargets()
			.Where(x => x.gameObject != this.gameObject)
			.OrderBy(x => 
				(x.gameObject.transform.position - transform.position)
				.sqrMagnitude)
			.ElementAt(0);
		
		if (target == null) {
			return State.Idle;
		}
		
		if (target.gameObject.layer == gameObject.layer) {
			Debug.Log("Friendly fire!");
		}
		
		Vector3 delta = target.transform.position - transform.position;
		if (delta.sqrMagnitude < ATTACK_OUTER_LIMIT_SQUARED) {
			movement.Stop();
			return State.Attack;
		}
		return State.Approach;
	}
	
	public void Awake() {
		nextEvaluation = 0f;
		movement = GetComponent<Move>();
		projectile = GetComponent<Projectile>();
		ResetTimer();
	}
	
	protected void ResetTimer() {
		nextEvaluation = Time.time + EVALUATION_TIMER + Random.Range(0f, EVALUATION_TIMER);
	}
	
	public void Update() {
		if (Time.time > nextEvaluation) {
			ResetTimer();
			mindstate = Reevaluate();
		}
		
		if (target == null) {
			return;
		}
		
		switch (mindstate) {
		case State.Approach:
			movement.TryCast(true, target.transform.position);
			break;
		case State.Attack:
			projectile.TryCast(true, target.transform.position);
			break;
		}
	}
}