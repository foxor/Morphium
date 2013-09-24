using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class WinLossWatcher : MonoBehaviour {
	protected const int PLAYER_TEAM = 0;
	
	protected static WinLossWatcher singleton;
	
	protected Dictionary<int, bool> losingTeams;
	
	public void Awake() {
		losingTeams = new Dictionary<int, bool>();
		singleton = this;
	}
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(CharacterType.Base, OnStatusChange);
		GlobalEventListener.Listener().AddCallback(CharacterType.Morphid, OnStatusChange);
		GlobalEventListener.Listener().AddCallback(Level.Adventure, OnLevelLoad);
	}
	
	public static void AddTeam(int team) {
		singleton.losingTeams[team] = false;
	}
	
	public void OnStatusChange(CharacterStatusEventData data) {
		if (data.EventStatus == CharacterStatusEventData.Status.Die) {
			int justLost = data.Source.GetComponent<Target>().Team;
			losingTeams[justLost] = true;
			if (justLost == PLAYER_TEAM) {
				LevelManager.LoadLevel(Level.Shop);
			}
			if (losingTeams.Values.Count(x => !x) == 1) {
				LevelManager.LoadLevel(Level.Shop);
			}
		}
	}
	
	public void OnLevelLoad(LevelChangeEventData data) {
		if (data.Action == LoadState.Loaded) {
			foreach (int team in losingTeams.Keys.ToArray()) {
				losingTeams[team] = false;
			}
		}
	}
}