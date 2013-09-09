using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GearSlots : MonoBehaviour {
	
	protected const int GEAR_NUM = 5;
	protected const int ENTRY_SIZE = 60;
	
	public Texture omniTexture;
	
	protected LootTrap lootTrap;
	protected ItemManager itemManager;
	protected CharacterEventListener listener;
	
	protected Rect[] destinations;
	protected TrapEntry[] entries;
	
	protected TrapEntry held;
	protected TrapEntry Held {
		get {
			return held;
		}
		set {
			bool equipped = false;
			if (held != null && value == null) {
				for (int place = 0; place < GEAR_NUM; place++) {
					if (held.TrappedItem.FilledSlot == entries[place].TrappedItem.FilledSlot && entries[place].TestIntersect()) {
						equipped = true;
						held.OccupiedRect = new Rect(entries[place].OccupiedRect.x, entries[place].OccupiedRect.y, ENTRY_SIZE, ENTRY_SIZE);
						lootTrap.AddItem(entries[place]);
						entries[place] = held;
						itemManager.SwitchItem(Held.TrappedItem);
						listener.Broadcast(CharacterEvents.Equip, null);
					}
				}
				if (!equipped) {
					lootTrap.AddItem(held);
				}
			}
			held = value;
		}
	}
	
	public void Awake() {
		lootTrap = GetComponent<LootTrap>();
		itemManager = GetComponent<ItemManager>();
		listener = GetComponent<CharacterEventListener>();
	}
	
	public void Start() {
		GlobalEventListener.Listener().AddCallback(Level.Shop, OnLoadShop);
		enabled = false;
		destinations = new Rect[] {
			new Rect(Screen.width / 2f - ENTRY_SIZE / 2f, Screen.height / 4f - ENTRY_SIZE / 2f, ENTRY_SIZE, ENTRY_SIZE),
			new Rect(Screen.width / 3f - ENTRY_SIZE / 2f, Screen.height / 3f - ENTRY_SIZE / 2f, ENTRY_SIZE, ENTRY_SIZE),
			new Rect(2f * Screen.width / 3f - ENTRY_SIZE / 2f, Screen.height / 3f - ENTRY_SIZE / 2f, ENTRY_SIZE, ENTRY_SIZE),
			new Rect(Screen.width / 3f - ENTRY_SIZE / 2f, 2f * Screen.height / 3f - ENTRY_SIZE / 2f, ENTRY_SIZE, ENTRY_SIZE),
			new Rect(2f * Screen.width / 3f - ENTRY_SIZE / 2f, 2f * Screen.height / 3f - ENTRY_SIZE / 2f, ENTRY_SIZE, ENTRY_SIZE),
		};
		ItemManager items = GetComponent<ItemManager>();
		entries = new TrapEntry[] {
			new TrapEntry() {OccupiedRect = destinations[0], TrappedItem = items.GetSlotEquipped(Slot.Head)},
			new TrapEntry() {OccupiedRect = destinations[1], TrappedItem = items.GetSlotEquipped(Slot.Arm)},
			new TrapEntry() {OccupiedRect = destinations[2], TrappedItem = items.GetSlotEquipped(Slot.Engine)},
			new TrapEntry() {OccupiedRect = destinations[3], TrappedItem = items.GetSlotEquipped(Slot.Chest)},
			new TrapEntry() {OccupiedRect = destinations[4], TrappedItem = items.GetSlotEquipped(Slot.Leg)}
		};
	}
	
	public void OnLoadShop(LevelChangeEventData data) {
		enabled = data.Action == LoadState.Loaded;
	}
	
	protected Rect destination (int place) {
		return destinations[place];
	}
	
	public void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Held = lootTrap.MouseOver();
			if (Held != null) {
				lootTrap.RemoveItem(Held);
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			Held = null;
		}
		if (Held != null) {
			Rect temp = Held.OccupiedRect;
			temp.center = InputExtender.MousePos();
			Held.OccupiedRect = temp;
		}
	}
	
	public void OnGUI() {
		GUI.depth = 1;
		for (int i = 0; i < GEAR_NUM; i++) {
			GUI.DrawTexture(entries[i].OccupiedRect, omniTexture);
		}
		if (Held != null) {
			GUI.DrawTexture(Held.OccupiedRect, omniTexture);
		}
	}
	
	public TrapEntry MouseOver() {
		foreach (TrapEntry entry in entries) {
			if (entry.TestIntersect()) {
				return entry;
			}
		}
		return null;
	}
}

public static class InputExtender {
	public static Vector2 MousePos() {
		return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}
}