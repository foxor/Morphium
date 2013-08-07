using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public enum Element {
	[AssociatedStatType(StatType.Charge)]
	Plasma,
	
	[AssociatedStatType(StatType.Speed)]
	Cold,
	
	[AssociatedStatType(StatType.Health)]
	Physical,
	
	[AssociatedStatType(StatType.Sensors)]
	Overload,
	
	[AssociatedStatType(StatType.Bandwidth)]
	Hack,
	
	[AssociatedStatType(StatType.Morphium)]
	Electric
}

public static class ElementExtension {
	public static StatType Damages(this Element e) {
		FieldInfo fi = typeof(Element).GetField(e.ToString());
		foreach (AssociatedStatType stat in fi.GetCustomAttributes(typeof(AssociatedStatType), false) as AssociatedStatType[]) {
			return stat.stat;
		}
		throw new Exception("Element has no associated stat");
	}
}

[AttributeUsage (AttributeTargets.Field)]
public class AssociatedStatType : Attribute {
    public StatType stat;
    public AssociatedStatType(StatType stat) {
        this.stat = stat;
    }
}