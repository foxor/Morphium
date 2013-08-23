using UnityEngine;
using System;
using System.Collections.Generic;

public class Startup : MonoBehaviour {
	public void Start() {
		LevelManager.LoadLevel(Level.Shop);
	}
}