using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public void OnGUI() {
		if (GUILayout.Button("Launch")) {
			LevelManager.LoadLevel(Level.Adventure);
		}
	}
}