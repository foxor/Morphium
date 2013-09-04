using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class AI : MonoBehaviour {
	
	public class Goal {
	}
	
	protected const float EVALUATION_TIMER = 0.3f;
	
	protected Stack<Goal> goals;
	protected Move movement;
	protected Projectile projectile;
	protected float nextEvaluation;
	
	protected abstract Goal Reevaluate();
	
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