using UnityEngine;
using System;
using System.Collections.Generic;

public enum MorphidEvents {
	Kill,
	Die
}

public class MorphidEventListener : EventListener<MorphidEvents, EventData> {
}