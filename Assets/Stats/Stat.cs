using UnityEngine;
using System.Collections.Generic;

public class Stat {
	public int Current {
		get; set;
	}
	
	public int Max {
		get; set;
	}
	
	public float SingleTickRegenTimer {
		get; set;
	}
	
	public float NextRegenTick {
		get; set;
	}
	
	public float RegenCooldown {
		get; set;
	}
	
	public int AllocatedRegen {
		get; set;
	}
}

public enum StatType {
	Torque,
	Sensors,
	Attack,
	Speed,
	Bandwidth,
	Source,
	Health,
	Morphium
}