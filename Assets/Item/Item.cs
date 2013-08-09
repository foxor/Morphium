using UnityEngine;
using System.Collections.Generic;

public class Item {
	public int StatBoost {
		get; set;
	}
	public int OffStatBoost {
		get; set;
	}
	public int HealthBoost {
		get; set;
	}
	public int ChargeBoost {
		get; set;
	}
	public StatType OffStatType {
		get; set;
	}
	public Ability GrantedAbility {
		get; set;
	}
}