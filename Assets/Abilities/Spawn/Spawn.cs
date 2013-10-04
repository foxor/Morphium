using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : Ability {
	
	protected const string RESOURCE_NAME = "Minion";
	protected static GameObject minionPrefab = (GameObject)Resources.Load(RESOURCE_NAME);
	
	protected Target target;
	protected IEnumerable<LaneElement> goal;
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
	
	public void Enable(int Team, IEnumerable<LaneElement> goal, Color color) {
		target.Team = Team;
		this.goal = goal;
		this.color = color;
	}
	
	protected override int Cost() {
		return 0;
	}
	
	protected override void Cast(Vector3 direction) {
		spawn = (GameObject)GameObject.Instantiate(minionPrefab);
		spawn.transform.position = transform.position;
		spawn.GetComponent<Target>().Team = target.Team;
		spawn.GetComponent<ColorChanger>().SetColor(color);
		if (goal != null) {
			spawn.GetComponent<MinionAI>().LongTermGoal = goal;
		}
	}
	
	public override void Update () {
	}
}