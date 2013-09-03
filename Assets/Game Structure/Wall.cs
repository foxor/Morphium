using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Wall : MonoBehaviour {
	
	protected const int WALLS = 4;
	
	protected static Collider[] walls = new Collider[WALLS];
	protected static int wallCount = 0;
	
	public void Awake() {
		walls[wallCount++] = collider;
	}
	
	public static bool isWall(RaycastHit test) {
		return walls.Any(x => x == test.collider);
	}
}