using UnityEngine;
using System.Collections.Generic;

public class ItemCard : MonoBehaviour {
	
	protected const int WIDTH = 300;
	protected const int HALF_WIDTH = WIDTH / 2;
	protected const int HEIGHT = 150;
	protected const int HALF_HEIGHT = HEIGHT / 2;
	
	protected LootTrap lootTrap;
	protected GearSlots gearSlots;
	
	public GUIStyle style;
	
	public void Awake() {
		lootTrap = GetComponent<LootTrap>();
		gearSlots = GetComponent<GearSlots>();
	}
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(Level.Shop, Toggle);
	}
	
	protected void Toggle(LevelChangeEventData data) {
		enabled = data.Action == LoadState.Loaded;
	}
	
	public void OnGUI() {
		TrapEntry entry = lootTrap.MouseOver();
		if (entry == null) {
			entry = gearSlots.MouseOver();
		}
		
		if (entry != null) {
			Vector2 center = new Vector2(
				Mathf.Clamp(InputExtender.MousePos().x, HALF_WIDTH, Screen.width - HALF_WIDTH),
				Mathf.Clamp(InputExtender.MousePos().y, HALF_HEIGHT, Screen.height - HALF_HEIGHT)
			);
			GUI.depth = 0;
			GUILayout.BeginArea(new Rect(center.x - HALF_WIDTH, center.y - HALF_HEIGHT, WIDTH, HEIGHT));
			GUILayout.Label("Health: " + entry.TrappedItem.HealthBoost, style);
			GUILayout.Label("Morphium: " + entry.TrappedItem.MorphiumBoost, style);
			GUILayout.Label(entry.TrappedItem.FilledSlot.Boosts().ToString() + " (Mainstat): " + entry.TrappedItem.StatBoost, style);
			GUILayout.Label(entry.TrappedItem.OffStatType.ToString() + " (Offstat): " + entry.TrappedItem.OffStatBoost, style);
			GUILayout.Label("Slot: " + entry.TrappedItem.FilledSlot.ToString(), style);
			GUILayout.EndArea();
		}
	}
}