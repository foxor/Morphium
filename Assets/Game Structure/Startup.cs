using UnityEngine;
using System;
using System.Collections.Generic;

public class Startup : MonoBehaviour {
	public void Awake() {
		LevelManager.LoadLevel(Level.Shop);
	}
}