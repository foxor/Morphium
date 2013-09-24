using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
	
public delegate bool Qualifier(Target t);

public class TargetManager : MonoBehaviour {
	
	protected static TargetManager singleton;
	
	protected HashSet<Target> targets;
	
	public void Awake() {
		targets = new HashSet<Target>();
		singleton = this;
	}
	
	public static void AddTarget(Target target) {
		singleton.targets.Add(target);
		GermaniumTracker.SetupTeam(target.Team);
		WinLossWatcher.AddTeam(target.Team);
	}
	
	public static void RemoveTarget(Target target) {
		singleton.targets.Remove(target);
	}
	
	public static IEnumerable<Target> GetTargets() {
		return singleton.targets.Where(x => x != null);
	}
	
	public static IEnumerable<Target> GetTargets(Qualifier qualifier) {
		return singleton.targets.Where(x => x != null && qualifier(x));
	}
	
	public static Qualifier IsOpposing(Target myTeam) {
		return x => x.Team != myTeam.Team;
	}
}