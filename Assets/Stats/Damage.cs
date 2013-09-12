using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Damage {
	public Element Type {
		get; set;
	}
	
	public int Magnitude {
		get; set;
	}
}