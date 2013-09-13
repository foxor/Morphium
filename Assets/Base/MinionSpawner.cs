using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionSpawner : MonoBehaviour {
	
	protected const float INTRASPAWN_INTERVAL = 0.5f;
	protected const float SPAWN_TIMER = 8f;
	protected const int MELEE_PER_WAVE = 3;
	protected const int RANGED_PER_WAVE = 3;
	protected const float SPAWN_DISTANCE = 1f;
	
	protected const int NUM_TURRETS = 3;
	protected const int INTRA_TURRET_SPACE = 2;
	protected const int BASE_EDGE_SPACE = 1;
	protected const int END_EDGE_SPACE = 2;
	
	public GameObject meleeMinionPrefab;
	public GameObject rangedMinionPrefab;
	public GameObject turretPrefab;
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
	
	public void Start() {
		SpawnTurrets();
	}
	
	protected int totalSpawns() {
		return MELEE_PER_WAVE + RANGED_PER_WAVE;
	}
	
	protected GameObject MinionPrefab(int spawnNum) {
		return spawnNum < MELEE_PER_WAVE ? meleeMinionPrefab : rangedMinionPrefab;
	}
	
	protected void SpawnTurrets() {
		// Lane diagram:
		//---BEETIIIITIIIITEEeetiiiitiiiiteeb---
		// Key:
		//  - Upper: spawned by me
		//  - Lower: spawned by lane opponent
		//  - "-": Empty space outside lane
		//  - B: this base
		//  - E: edge space
		//  - I: intra-turret space
		//  - T: Turret
		int totalSpace = (BASE_EDGE_SPACE + END_EDGE_SPACE + INTRA_TURRET_SPACE * (NUM_TURRETS - 1)) * 2;
		float lerp_min = ((float)BASE_EDGE_SPACE) / ((float)totalSpace);
		float lerp_max = ((float)((totalSpace / 2) - END_EDGE_SPACE)) / ((float)totalSpace);
		foreach (MinionSpawner lane in lanes) {
			for (int i = 0; i < NUM_TURRETS; i++) {
				Vector3 spawnPos = Vector3.Lerp(transform.position, lane.transform.position,
					Mathf.Lerp(lerp_min, lerp_max, ((float)i) / ((float)(NUM_TURRETS - 1)))
				);
				SpawnTurret(spawnPos);
			}
		}
	}
	
	protected void SpawnTurret(Vector3 position) {
		GameObject spawn = (GameObject)Instantiate(turretPrefab);
		spawn.transform.position = position;
		spawn.GetComponent<Target>().Team = target.Team;
		spawn.renderer.material.color = renderer.material.color;
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