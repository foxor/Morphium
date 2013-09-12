using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class AI : MonoBehaviour {
	
	public class Goal {
	}
	
	protected const float EVALUATION_TIMER = 0.3f;
	
	protected Stack<Goal> goals;
	protected float nextEvaluation;
	
	protected abstract void Reevaluate();
	
	protected abstract bool Process(Goal goal);
	
	public void Awake() {
		nextEvaluation = 0f;
		goals = new Stack<Goal>();
	}
	
	protected void ResetTimer() {
		nextEvaluation = Time.time + EVALUATION_TIMER + Random.Range(0f, EVALUATION_TIMER);
	}
	
	public void Update() {
		if (Time.time > nextEvaluation) {
			ResetTimer();
			Reevaluate();
		}
		
		if (goals.Any()) {
			if (!Process(goals.Peek())) {
				goals.Pop();
			}
		}
	}
}