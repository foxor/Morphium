using UnityEngine;
using System;
using System.Collections.Generic;

public enum CharacterEvents {
	Kill,
	Die,
	Equip,
	Destroy,
	Hit,
	PickUp
}

public class CharacterEvent : EventData {
}

public class KillEvent : CharacterEvent {
	public GameObject Other {
		get; set;
	}
}

public class HitEvent : CharacterEvent {
	public Damage Damage {
		get; set;
	}
	
	public DamageDealer Source {
		get; set;
	}
}

public class CharacterEventListener : EventListenerComponent<CharacterEvents, CharacterEvent> {
}