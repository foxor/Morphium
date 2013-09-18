using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerSnapListener : MonoBehaviour {
	protected Vector3 originalPosition;
	
	public void Awake() {
		originalPosition = transform.position;
	}
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(Level.Shop, OnShop);
	}
	
	protected void OnShop(LevelChangeEventData data) {
		if (data.Action == LoadState.Loaded) {
			transform.position = originalPosition;
		}
	}
}