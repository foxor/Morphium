using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public enum Slot {
	[AssociatedStatType(StatType.Sensors)]
	Head,
	
	[AssociatedStatType(StatType.Attack)]
	Arm,
	
	[AssociatedStatType(StatType.Torque)]
	Engine,
	
	[AssociatedStatType(StatType.Speed)]
	Leg,
	
	[AssociatedStatType(StatType.Bandwidth)]
	Chest
}

public static class SlotExtension {
	public static StatType Boosts(this Slot e) {
		FieldInfo fi = typeof(Slot).GetField(e.ToString());
		foreach (AssociatedStatType stat in fi.GetCustomAttributes(typeof(AssociatedStatType), false) as AssociatedStatType[]) {
			return stat.stat;
		}
		throw new Exception("Element has no associated stat");
	}
}