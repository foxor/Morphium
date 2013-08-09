using UnityEngine;
using System.Collections.Generic;

public class ChargeBar : StatBar {
	protected override StatType BarStat {
		get {
			return StatType.Charge;
		}
	}
	
	protected Rect drawLocation;
	protected override Rect DrawLocation {
		get {
			return drawLocation;
		}
	}
	
	public void Start() {
		drawLocation = new Rect(Screen.width * (5f / 12f), Screen.height * (8f / 9f), Screen.width * (1f / 6f), Screen.height / 18f);
	}
}