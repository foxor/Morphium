using UnityEngine;
using System.Collections.Generic;

public enum Level {
	[LevelId(0)]
	Start,
	
	[LevelId(1)]
	Shop,
	
	[LevelId(2)]
	Adventure
}

public static class LevelExtension {
	public static int GetId(this Level e) {
		FieldInfo fi = typeof(Level).GetField(e.ToString());
		foreach (LevelId stat in fi.GetCustomAttributes(typeof(LevelId), false) as LevelId[]) {
			return stat.stat;
		}
		throw new Exception("Element has no associated stat");
	}
}

[AttributeUsage (AttributeTargets.Field)]
public class LevelId : Attribute {
    public int id;
    public LevelId(int id) {
        this.id = id;
    }
}