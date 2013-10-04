using UnityEngine;
using System.Collections.Generic;

public class Lane : MonoBehaviour {
	public LaneElement[] targets;
	
	public LaneElement Next() {
		foreach (LaneElement t in targets) {
			if (t != null) {
				return t;	
			}
		}
		return null;
	}
}