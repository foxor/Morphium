using UnityEngine;
using System.Collections.Generic;

public class Stat {
	public int Current {
		get; set;
	}
	
	public int Max {
		get; set;
	}
}

public enum StatType {
	Torque,
	Sensors,
	Attack,
	Speed,
	Bandwidth,
	Charge,
	Health,
	Morphium
}