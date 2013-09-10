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

public class CharacterStatusEventData : EventData {
	public enum Status {
		Spawn,
		Die
	}
	public enum CharacterType {
		Base,
		Minion,
		Morphid
	}
	
	public Status EventStatus {
		get; set;
	}
	public CharacterType EventCharacterType {
		get; set;
	}
	public GameObject Source {
		get; set;
	}
}

public class GlobalEventListener : 
	MonoBehaviour,
	IEventListener<Level, LevelChangeEventData>,
	IEventListener<InputEvent, InputEventData>,
	IEventListener<CharacterType, CharacterStatusEventData>
{
	protected static GlobalEventListener singleton;
	
	protected EventListener<Level, LevelChangeEventData> levelChangeListener;
	protected EventListener<InputEvent, InputEventData> inputListener;
	protected EventListener<CharacterType, CharacterStatusEventData> characterListener;
	
	public void Awake() {
		levelChangeListener = new EventListener<Level, LevelChangeEventData>();
		inputListener = new EventListener<InputEvent, InputEventData>();
		characterListener = new EventListener<CharacterType, CharacterStatusEventData>();
		singleton = this;
	}
	
	public static GlobalEventListener Listener() {
		return singleton;
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
	
	public void AddCallback (CharacterType trigger, Callback<CharacterStatusEventData> callback) {
		characterListener.AddCallback(trigger, callback);
	}

	public bool RemoveCallback (CharacterType trigger, Callback<CharacterStatusEventData> callback) {
		return characterListener.RemoveCallback(trigger, callback);
	}

	public void Broadcast (CharacterType trigger, CharacterStatusEventData data) {
		characterListener.Broadcast(trigger, data);
	}
}