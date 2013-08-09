using UnityEngine;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	
	public enum State {
		Approach,
		Attack,
	}
	
	public const float EVALUATION_TIMER = 1f;
	public const float ATTACK_OUTER_LIMIT_SQUARED = 10f * 10f;
	
	public GameObject Target {
		get;
		set;
	}
	
	protected Move movement;
	protected Projectile projectile;
	protected State mindstate;
	protected float nextEvaluation;
	
	protected State Reevaluate() {
		Vector3 delta = Target.transform.position - transform.position;
		if (delta.sqrMagnitude < ATTACK_OUTER_LIMIT_SQUARED) {
			return State.Attack;
		}
		return State.Approach;
	}
	
	public void Awake() {
		nextEvaluation = 0f;
		movement = GetComponent<Move>();
		projectile = GetComponent<Projectile>();
	}
	
	public void Update() {
		if (Time.time > nextEvaluation) {
			nextEvaluation = Time.time + EVALUATION_TIMER + Random.Range(0f, EVALUATION_TIMER);
			mindstate = Reevaluate();
		}
		
		switch (mindstate) {
		case State.Approach:
			movement.TryCast(true, Target.transform.position);
			break;
		case State.Attack:
			projectile.TryCast(true, Target.transform.position);
			break;
		}
	}
}