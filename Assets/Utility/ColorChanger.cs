using UnityEngine;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour {
	public Color color;
	
	public void Awake() {
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
			r.material.color = color;
		}
	}
}