using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	protected static LevelManager singleton;
	
	protected Level _level;
	protected Level level {
		get {
			return _level;
		}
		set {
			if (_level != value) {
				GlobalEventListener.Listener().Broadcast(_level, new LevelChangeEventData() {Action = LoadState.Unloaded, CurrentLevel = _level});
				_level = value;
				Application.LoadLevel(_level.GetId());
				GlobalEventListener.Listener().Broadcast(_level, new LevelChangeEventData() {Action = LoadState.Begin, CurrentLevel = _level});
			}
		}
	}
	
	public static void LoadLevel(Level level) {
		singleton.level = level;
	}
	
	public void Awake() {
		_level = Level.Start;
		singleton = this;
	}
	
	public void OnLevelWasLoaded() {
		if (_level.GetId() != Application.loadedLevel) {
			throw new Exception("Changed level outside of LevelManager");
		}
		GlobalEventListener.Listener().Broadcast(_level, new LevelChangeEventData() {Action = LoadState.Loaded, CurrentLevel = _level});
	}
}