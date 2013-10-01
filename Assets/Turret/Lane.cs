using UnityEngine;
using System.Collections.Generic;

public class Lane : MonoBehaviour {
	public Target[] targets;
	
	public Target Next() {
		foreach (Target t in targets) {
			if (t != null) {
				return t;	
			}
		}
		return null;
	}
}