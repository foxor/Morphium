using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	
	public GUIStyle style;
	
	public void OnGUI() {
		if (GUILayout.Button("Launch", style)) {
			LevelManager.LoadLevel(Level.Adventure);
		}
	}
}