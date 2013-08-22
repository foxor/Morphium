using UnityEngine;
using System.Collections.Generic;

public enum GlobalEvents {
}

public class GlobalEventData : EventData {
}

public class InputEventData : GlobalEventData {
	public bool PressedThisFrame {
		get; set;
	}
}

public class LevelChangeEventData : GlobalEventData {
	public Level Loading {
		get; set;
	}
}

public class GlobalEventListener : EventListener<GlobalEvents, GlobalEventData> {
}