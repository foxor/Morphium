using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionSpawner : MonoBehaviour {
	
	protected const float INTRASPAWN_INTERVAL = 0.5f;
	protected const float SPAWN_TIMER = 8f;
	protected const int MELEE_PER_WAVE = 3;
	protected const int RANGED_PER_WAVE = 3;
	protected const float SPAWN_DISTANCE = 1f;
	
	public GameObject meleeMinionPrefab;
	public GameObject rangedMinionPrefab;
	public MinionSpawner[] lanes;
	
	protected float nextSpawn;
	protected Target target;
	
	public void Awake() {
		nextSpawn = Time.time + SPAWN_TIMER;
		target = GetComponent<Target>();
	}
	
	public void Update() {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + SPAWN_TIMER;
			StartCoroutine(Spawn());
		}
	}
	
	protected int totalSpawns() {
		return MELEE_PER_WAVE + RANGED_PER_WAVE;
	}
	
	protected GameObject MinionPrefab(int spawnNum) {
		return spawnNum < MELEE_PER_WAVE ? meleeMinionPrefab : rangedMinionPrefab;
	}
	
	protected IEnumerator Spawn() {
		int spawnNum = 0;
		do {
			float timer = 0f;
			while (timer < INTRASPAWN_INTERVAL) {
				yield return 0;
				timer += Time.deltaTime;
			}
			foreach (MinionSpawner lane in lanes) {
				if (lane != null) {
					SpawnMinion(MinionPrefab(spawnNum), lane);
				}
			}
		} while (++spawnNum < totalSpawns());
	}
	
	protected void SpawnMinion(GameObject minion, MinionSpawner lane) {
		GameObject spawn = (GameObject)Instantiate(minion);
		Vector3 delta = (lane.transform.position - transform.position).normalized * SPAWN_DISTANCE;
		spawn.transform.position = transform.position + delta;
		spawn.GetComponent<MinionAI>().LongTermGoal = lane.transform.position;
		spawn.GetComponent<Target>().Team = target.Team;
		spawn.renderer.material.color = renderer.material.color;
	}
}