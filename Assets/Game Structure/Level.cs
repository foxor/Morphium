using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public enum Level {
	[LevelId(0)]
	Start,
	
	[LevelId(1)]
	Shop,
	
	[LevelId(2)]
	Adventure
}

public enum LoadState {
	Begin,
	Loaded,
	Unloaded
}

public static class LevelExtension {
	public static int GetId(this Level e) {
		FieldInfo fi = typeof(Level).GetField(e.ToString());
		foreach (LevelId id in fi.GetCustomAttributes(typeof(LevelId), false) as LevelId[]) {
			return id.id;
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