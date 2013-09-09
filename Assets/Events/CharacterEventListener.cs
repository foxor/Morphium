using UnityEngine;
using System;
using System.Collections.Generic;

public enum CharacterEvents {
	Kill,
	Die,
	Equip,
	Destroy
}

public class CharacterEvent : EventData {
	public GameObject other;
}

public class CharacterEventListener : EventListenerComponent<CharacterEvents, CharacterEvent> {
}