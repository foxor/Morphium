using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : Ability {
	
	protected const float SPAWN_DISTANCE = 1f;
	
	protected const string RESOURCE_NAME = "Minion";
	protected static GameObject minionPrefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected Target target;
	public Target enemy;
	protected Color color;
	
	public Spawn(StatManager s) : base(s) {}
	
	public void Enable(int Team, Target enemy, Color color) {
		target = statManager.GetComponent<Target>();
		target.Team = Team;
		this.enemy = enemy;
		this.color = color;
		statManager.GetComponent<ColorChanger>().color = color;
	}
	
	protected override int Cost() {
		return 0;
	}
	
	protected override void Cast(Vector3 direction) {
		GameObject spawn = (GameObject)GameObject.Instantiate(minionPrefab);
		Vector3 delta = (enemy.transform.position - transform.position).normalized * SPAWN_DISTANCE;
		spawn.transform.position = transform.position + delta;
		spawn.GetComponent<MinionAI>().LongTermGoal = enemy.transform.position;
		spawn.GetComponent<Target>().Team = target.Team;
		foreach (Renderer r in spawn.GetComponentsInChildren<Renderer>()) {
			r.material.color = color;
		}
	}
	
	public override void Update () {
	}
}