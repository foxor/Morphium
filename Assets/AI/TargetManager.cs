using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class TargetManager : MonoBehaviour {
	protected static TargetManager singleton;
	
	protected HashSet<Target> targets;
	
	public void Awake() {
		targets = new HashSet<Target>();
		singleton = this;
	}
	
	public static void AddTarget(Target target) {
		singleton.targets.Add(target);
	}
	
	public static void RemoveTarget(Target target) {
		singleton.targets.Remove(target);
	}
	
	public static IEnumerable<Target> GetTargets() {
		return singleton.targets.Where(x => x != null);
	}
}