using UnityEngine;
using System;
using System.Collections.Generic;

public class MatchTimer : MonoBehaviour {
	protected float beginningTime;
	protected float endTime;
	protected bool inLevel;
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(Level.Adventure, OnAdventureLoad);
	}
	
	protected void OnAdventureLoad(LevelChangeEventData data) {
		if (data.Action == LoadState.Loaded) {
			inLevel = true;
			beginningTime = Time.time;
		}
		if (data.Action == LoadState.Unloaded) {
			inLevel = false;
			endTime = Time.time;
		}
	}
	
	public void OnGUI() {
		if (inLevel) {
			endTime = Time.time;
		}
		float deltaTime = endTime - beginningTime;
		int minutes = Mathf.FloorToInt(deltaTime / 60f);
		int seconds = Mathf.FloorToInt(deltaTime % 60f);
		GUILayout.BeginArea(new Rect(0f, 150f, 300f, 100f));
		GUILayout.Label("Match length: " + minutes + ":" + seconds);
		GUILayout.EndArea();
	}
}