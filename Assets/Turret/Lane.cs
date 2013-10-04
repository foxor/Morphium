using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lane : MonoBehaviour {
	public LaneElement[] targets;
	
	public IEnumerable<LaneElement> Enumerator() {
		foreach (LaneElement t in targets) {
			if (t != null) {
				yield return t;
			}
		}
	}
}