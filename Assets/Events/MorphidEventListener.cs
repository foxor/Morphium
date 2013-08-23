using UnityEngine;
using System;
using System.Collections.Generic;

public enum MorphidEvents {
	Kill,
	Die
}

public class MorphidEvent : EventData {
	public GameObject other;
}

public class MorphidEventListener : EventListenerComponent<MorphidEvents, MorphidEvent> {
}