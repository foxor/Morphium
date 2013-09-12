using UnityEngine;
using System;
using System.Collections.Generic;

public class Move : Ability {
	
	public float Speed {
		get; set;
	}
	public Ability OnArrival {
		get; set;
	}
	public int cost {
		get; set;
	}
	public bool ContinueToRange {
		get; set;
	}
	public float Range {
		get; set;
	}
	
	private Nullable<Vector3> delta;
	
	public Move(StatManager s) : base(s){}
	public Move(StatManager s, GameObject toMove) : base(s, toMove){}
	
	protected override void Cast (Vector3 target) {
		target.y = transform.position.y;
		delta = target - transform.position;
		transform.rotation = Quaternion.LookRotation(delta.Value.normalized);
		if (ContinueToRange) {
			delta = delta.Value.normalized * Range;
		}
	}
	
	protected override int Cost () {
		return cost;
	}
	
	public void Stop () {
		delta = null;
	}
	
	public override void Update () {
		if (delta != null) {
			Vector3 normlizedDelta = delta.Value.normalized * Speed * Time.deltaTime;
			if (delta.Value.sqrMagnitude < normlizedDelta.sqrMagnitude) {
				transform.position += delta.Value;
				delta = null;
				if (OnArrival != null) {
					OnArrival.TryCast(true, Vector3.zero);
				}
			}
			else {
				transform.position += normlizedDelta;
				delta -= normlizedDelta;
			}
		}
	}
}