using UnityEngine;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour {
	public Color color;
	
	public void Awake() {
	}
	
	public void SetColor(Color color) {
		this.color = color;
		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
			r.material.color = color;
		}
	}
}