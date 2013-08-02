using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float cooldown;
	public float distance;
	public GameObject prefab;
	
	protected float nextSpawn;
	
	public void Update() {
		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + cooldown;
			GameObject enemy = (GameObject)Instantiate(prefab);
			AI brain = enemy.GetComponent<AI>();
			brain.Target = gameObject;
			Vector2 circle = Random.insideUnitCircle.normalized * distance;
			enemy.transform.position = new Vector3(circle.x, transform.position.y, circle.y);
		}
	}
}
