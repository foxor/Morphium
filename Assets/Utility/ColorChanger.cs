using UnityEngine;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour {
	public Color color;
	
	public void Awake() {
		renderer.material.color = color;
	}
}