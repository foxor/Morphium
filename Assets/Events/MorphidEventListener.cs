using UnityEngine;
using System;
using System.Collections.Generic;

public enum MorphidEvents {
	Kill,
	Die,
	Equip,
	Destroy
}

public class MorphidEvent : EventData {
	public GameObject other;
}

public class MorphidEventListener : EventListenerComponent<MorphidEvents, MorphidEvent> {
}