using UnityEngine;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	
	public GameObject Target {
		get;
		set;
	}
	
	protected Move movement;
	
	public void Awake() {
		movement = GetComponent<Move>();
	}
	
	public void Update() {
		movement.Cast(Target.transform.position);
	}
}