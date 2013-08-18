using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public void Awake() {
		EnemySpawner.Disable();
	}
	
	public void OnGUI() {
		if (GUILayout.Button("Launch")) {
			EnemySpawner.Enable();
			Application.LoadLevel("Adventure");
		}
	}
}