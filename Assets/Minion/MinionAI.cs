using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionAI : AI {
	protected class MoveTowards : AI.Goal {
		public Vector3 Destination {
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
	
	
	protected IEnumerator SetupLongTermGoal() {
		while (goals == null) {
			yield return 0;
		}
		goals.Clear();
		goals.Push(new MoveTowards(){Destination = longTermGoal});
	}
	
	protected override Goal Reevaluate () {
		return null;
	}

	protected override void Process (Goal goal) {
		movement.TryCast(false, ((MoveTowards)goal).Destination);
	}
}