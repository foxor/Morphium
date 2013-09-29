using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : Ability {
	
	protected const string RESOURCE_NAME = "Minion";
	protected static GameObject minionPrefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected Target target;
	public Target enemy;
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
	
	public void Enable(int Team, Target enemy, Color color) {
		target.Team = Team;
		this.enemy = enemy;
		this.color = color;
		statManager.GetComponent<ColorChanger>().color = color;
	}
	
	protected override int Cost() {
		return 0;
	}
	
	protected override void Cast(Vector3 direction) {
		spawn = (GameObject)GameObject.Instantiate(minionPrefab);
		Vector3 delta;
		if (enemy == null) {
			delta = Vector3.zero;
		}
		else {
			delta = (enemy.transform.position - transform.position).normalized * SPAWN_DISTANCE;
			spawn.GetComponent<MinionAI>().LongTermGoal = enemy.transform.position;
		}
		spawn.transform.position = transform.position + delta;
		spawn.GetComponent<Target>().Team = target.Team;
		foreach (Renderer r in spawn.GetComponentsInChildren<Renderer>()) {
			r.material.color = color;
		}
	}
	
	public override void Update () {
	}
}