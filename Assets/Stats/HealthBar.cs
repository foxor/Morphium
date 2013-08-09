using UnityEngine;
using System.Collections.Generic;

public class HealthBar : StatBar {
	protected override StatType BarStat {
		get {
			return StatType.Health;
		}
	}
	
	protected Rect drawLocation;
	protected override Rect DrawLocation {
		get {
			return drawLocation;
		}
	}
	
	public void Start() {
		drawLocation = new Rect(Screen.width * (5f / 12f), Screen.height * (17f / 18f), Screen.width * (1f / 6f), Screen.height / 18f);
	}
}