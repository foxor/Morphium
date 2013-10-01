using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : Ability {
	
	protected const string RESOURCE_NAME = "Minion";
	protected static GameObject minionPrefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected Target target;
	protected Target goal;
	protected Color color;
	protected GameObject spawn;
	
	public GameObject LastSpawn {
		get {
			return spawn;
		}
	}
	
	public Spawn(StatManager s) : base(s) {
		target = statManager.GetComponent<Target>();
	}
	
	public void Enable(int Team, Target goal, Color color) {
		target.Team = Team;
		this.target = goal;
		this.color = color;
		statManager.GetComponent<ColorChanger>().color = color;
	}
	
	protected override int Cost() {
		return 0;
	}
	
	protected override void Cast(Vector3 direction) {
		spawn = (GameObject)GameObject.Instantiate(minionPrefab);
		if (goal != null) {
			spawn.GetComponent<MinionAI>().LongTermGoal = goal;
		}
		spawn.transform.position = transform.position;
		spawn.GetComponent<Target>().Team = target.Team;
		foreach (Renderer r in spawn.GetComponentsInChildren<Renderer>()) {
			r.material.color = color;
		}
	}
	
	public override void Update () {
	}
}