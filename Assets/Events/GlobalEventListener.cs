using UnityEngine;
using System.Collections.Generic;

public class InputEventData : EventData {
	public bool PressedThisFrame {
		get; set;
	}
}

public class LevelChangeEventData : EventData {
	public Level CurrentLevel {
		get; set;
	}
	public LoadState Action {
		get; set;
	}
}

public class GlobalEventListener : 
	MonoBehaviour,
	IEventListener<Level, LevelChangeEventData>,
	IEventListener<InputEvent, InputEventData>
{
	protected EventListener<Level, LevelChangeEventData> levelChangeListener;
	protected EventListener<InputEvent, InputEventData> inputListener;
	
	public void Awake() {
		levelChangeListener = new EventListener<Level, LevelChangeEventData>();
		inputListener = new EventListener<InputEvent, InputEventData>();
	}
	
	public void AddCallback (InputEvent trigger, Callback<InputEventData> callback) {
		inputListener.AddCallback(trigger, callback);
	}

	public bool RemoveCallback (InputEvent trigger, Callback<InputEventData> callback) {
		return inputListener.RemoveCallback(trigger, callback);
	}

	public void Broadcast (InputEvent trigger, InputEventData data) {
		inputListener.Broadcast(trigger, data);
	}

	public void AddCallback (Level trigger, Callback<LevelChangeEventData> callback) {
		levelChangeListener.AddCallback(trigger, callback);
	}

	public bool RemoveCallback (Level trigger, Callback<LevelChangeEventData> callback) {
		return levelChangeListener.RemoveCallback(trigger, callback);
	}

	public void Broadcast (Level trigger, LevelChangeEventData data) {
		levelChangeListener.Broadcast(trigger, data);
	}
}