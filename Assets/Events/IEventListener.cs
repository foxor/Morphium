using UnityEngine;
using System.Collections.Generic;

public class EventData {
}

public delegate void Callback<T>(T data) where T : EventData;

// I would add "where ENUM_TYPE : enum" here, but it's not supported by C#
public interface IEventListener<ENUM_TYPE, CALLBACK_TYPE> where CALLBACK_TYPE : EventData {
	void AddCallback(ENUM_TYPE trigger, Callback<CALLBACK_TYPE> callback);
	bool RemoveCallback(ENUM_TYPE trigger, Callback<CALLBACK_TYPE> callback);
	void Broadcast(ENUM_TYPE trigger, CALLBACK_TYPE data);
}

public class EventListener<E, C> : IEventListener<E, C> where C : EventData {
	protected Dictionary<E, List<Callback<C>>> callbacks;
	
	public EventListener() {
		callbacks = new Dictionary<E, List<Callback<C>>>();
	}
	
	public void AddCallback (E trigger, Callback<C> callback) {
		List<Callback<C>> registered;
		if (callbacks.ContainsKey(trigger)) {
			registered = callbacks[trigger];
		}
		else {
			registered = new List<Callback<C>>();
			callbacks[trigger] = registered;
		}
		registered.Add(callback);
	}

	public bool RemoveCallback (E trigger, Callback<C> callback) {
		List<Callback<C>> registered = callbacks[trigger];
		return registered != null && registered.Remove(callback);
	}

	public void Broadcast (E trigger, C data) {
		if (callbacks.ContainsKey(trigger)) {
			List<Callback<C>> registered = callbacks[trigger];
			if (registered != null) {
				foreach (Callback<C> callback in registered.ToArray()) {
					callback(data);
				}
			}
		}
	}
}

public abstract class EventListenerComponent<E, C> : MonoBehaviour, IEventListener<E, C> where C : EventData {
	protected EventListener<E, C> eventBase;
	
	public void Awake() {
		eventBase = new EventListener<E, C>();
	}
	
	public void AddCallback (E trigger, Callback<C> callback) {
		eventBase.AddCallback(trigger, callback);
	}

	public bool RemoveCallback (E trigger, Callback<C> callback) {
		return eventBase.RemoveCallback(trigger, callback);
	}

	public void Broadcast (E trigger, C data) {
		eventBase.Broadcast(trigger, data);
	}
}