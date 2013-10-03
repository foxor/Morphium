using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum GermaniumEvent {
	Gain,
	Lose,
	MorphiumSpend,
	BuildSpend,
	SpawnSpend
}

public class GermaniumLevel : EventData {
	public int Level {
		get; set;
	}
	public int Team {
		get; set;
	}
}

public class GermaniumTracker : EventListenerComponent<GermaniumEvent, GermaniumLevel> {
	
	protected const int INITIAL_GERMANIUM = 0;
	
	protected static GermaniumTracker singleton;
	
	protected Dictionary<int, int> germaniumTotals;
	
	public new void Awake() {
		singleton = this;
		germaniumTotals = new Dictionary<int, int>();
		base.Awake();
	}
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(Level.Shop, Reset);
	}
	
	protected void Reset(LevelChangeEventData data) {
		if (data.Action == LoadState.Loaded) {
			foreach (int i in germaniumTotals.Keys.ToArray()) {
				germaniumTotals[i] = INITIAL_GERMANIUM;
			}
		}
	}
	
	public static GermaniumTracker Singleton() {
		return singleton;
	}
	
	public static void SetupTeam(int team) {
		if (!singleton.germaniumTotals.ContainsKey(team)) {
			singleton.germaniumTotals[team] = INITIAL_GERMANIUM;
		}
	}
	
	public int this[int team] {
		get {
			return germaniumTotals[team];
		}
		set {
			GermaniumLevel newVal = new GermaniumLevel(){Level = value, Team = team};
			if (value > germaniumTotals[team]) {
				if (germaniumTotals[team] == 0) {
					Broadcast(GermaniumEvent.MorphiumSpend, newVal);
					Broadcast(GermaniumEvent.BuildSpend, newVal);
					Broadcast(GermaniumEvent.SpawnSpend, newVal);
				}
				Broadcast(GermaniumEvent.Gain, newVal);
			}
			else {
				Broadcast(GermaniumEvent.Lose, newVal);
			}
			germaniumTotals[team] = newVal.Level;
		}
	}
}