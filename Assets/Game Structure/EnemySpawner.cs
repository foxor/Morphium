using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float cooldown;
	public float distance;
	public GameObject prefab;
	
	protected float nextSpawn;
	
	public void Start() {
		GlobalEventListener listener = GetComponent<GlobalEventListener>();
		listener.AddCallback(Level.Adventure, OnLoadAdventure);
		enabled = false;
	}
	
	public void OnLoadAdventure(LevelChangeEventData data) {
		enabled = data.Action == LoadState.Loaded;
	}
	
	public void Update() {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + cooldown;
			GameObject enemy = (GameObject)Instantiate(prefab);
			Vector2 circle = Random.insideUnitCircle.normalized * distance;
			enemy.transform.position = transform.position + new Vector3(circle.x, 0f, circle.y);
		}
	}
}
