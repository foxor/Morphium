using UnityEngine;
using System;
using System.Collections.Generic;

public enum CharacterEvents {
	Kill,
	Die,
	Equip,
	Destroy,
	Hit
}

public class CharacterEvent : EventData {
	public GameObject Other {
		get; set;
	}
	
	public Damage Damage {
		get; set;
	}
	
	public DamageDealer Source {
		get; set;
	}
}

public class CharacterEventListener : EventListenerComponent<CharacterEvents, CharacterEvent> {
}