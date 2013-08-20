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
	public int MorphiumBoost {
		get; set;
	}
	public StatType OffStatType {
		get; set;
	}
	public Ability GrantedAbility {
		get; set;
	}
	public Texture Icon {
		get; set;
	}
	
	protected int RandomValue = Random.Range(0, 10000);
	public int Value {
		get {
			return RandomValue;
		}
	}
}