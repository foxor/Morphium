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
		GlobalEventListener.Listener().AddCallback(CharacterType.Turret, OnStatusChange);
		GlobalEventListener.Listener().AddCallback(CharacterType.Morphid, OnStatusChange);
		GlobalEventListener.Listener().AddCallback(Level.Adventure, OnLevelLoad);
	}
	
	public static void AddTeam(int team) {
		if (team >= 0) {
			singleton.losingTeams[team] = false;
		}
	}
	
	protected bool HasAny(int team, CharacterType type) {
		return TargetManager.GetTargets(x => 
			x != null &&
			x.Team == team &&
			x.GetComponent<TypeProvider>().Type == type &&
			!x.GetComponent<DeathHandler>().IsDead
		).Any();
	}
	
	protected bool HasLost(int team) {
		if (team == PLAYER_TEAM) {
			return !HasAny(team, CharacterType.Morphid);
		}
		return !HasAny(team, CharacterType.Turret);
	}
	
	public void OnStatusChange(CharacterStatusEventData data) {
		if (data.EventStatus == CharacterStatusEventData.Status.Die) {
			int checkLost = data.Source.GetComponent<Target>().Team;
			if (HasLost(checkLost)) {
				losingTeams[checkLost] = true;
				if (checkLost == PLAYER_TEAM) {
					LevelManager.LoadLevel(Level.Shop);
				}
				if (losingTeams.Values.Count(x => !x) == 1) {
					LevelManager.LoadLevel(Level.Shop);
				}
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