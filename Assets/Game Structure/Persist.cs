using UnityEngine;
using System.Collections.Generic;

public class Persist : MonoBehaviour {
	public void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}