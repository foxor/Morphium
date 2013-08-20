using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootTrap : MonoBehaviour {
	protected const int TRAP_SIZE = 5;
	protected const int ENTRY_SIZE = 60;
	protected const float TWEEN_TIME = .5f;

	public class TrapEntry {
		public Item TrappedItem {
			get; set;
		}
		public Rect OccupiedRect {
			get; set;
		}
	}
	
	public Texture omniTexture;
	
	protected Rect boundary;
	protected int filled;
	protected TrapEntry[] entries;
	
	public void Awake() {
		filled = 0;
		entries = new TrapEntry[TRAP_SIZE];
		boundary = new Rect(Screen.width * 7f / 8f, Screen.height / 4f, Screen.width / 8f, Screen.height / 2f);
	}
	
	protected Rect destination(int place) {
		return new Rect(boundary.x, boundary.y + boundary.height * (((float)place) / ((float)TRAP_SIZE)), ENTRY_SIZE, ENTRY_SIZE);
	}
	
	public void AddItem(Item item, Vector3 startPos) {
		// Cases:
		// filled = 0: start at -1, drop out of loop and put the item in.  This is the first item, so it goes in
		// filled = TRAP_SIZE, valuable item: walk up the trap, looking for the first item worth more than this one
		// filled = TRAP_SIZE, worthless item: do nothing
		int place;
		for (place = Mathf.Min(TRAP_SIZE - 1, filled - 1); place >= 0 && item.Value > entries[place].TrappedItem.Value; place--) {
			if (place < TRAP_SIZE - 1) {
				entries[place + 1] = entries[place];
				StartCoroutine(TweenPos(entries[place], destination(place + 1), TWEEN_TIME));
			}
		}
		if (++place < TRAP_SIZE) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(startPos);
			entries[place] = new TrapEntry(){TrappedItem = item, OccupiedRect = 
				new Rect(screenPos.x, Screen.height - screenPos.y, ENTRY_SIZE, ENTRY_SIZE)
			};
			StartCoroutine(TweenPos(entries[place], destination(place), TWEEN_TIME));
		}
		filled = Mathf.Min(TRAP_SIZE, filled + 1);
	}
	
	public IEnumerator TweenPos(TrapEntry entry, Rect eventual, float dt) {
		float totalTime = dt;
		float pct;
		float startX = entry.OccupiedRect.x;
		float startY = entry.OccupiedRect.y;
		Rect current = entry.OccupiedRect;
		while (dt > 0f) {
			yield return 0;
			dt = Mathf.Max(0f, dt - Time.deltaTime);
			pct = 1f - (dt / totalTime);
			current.x = Mathf.Lerp(startX, eventual.x, pct);
			current.y = Mathf.Lerp(startY, eventual.y, pct);
			entry.OccupiedRect = current;
		}
	}
	
	public void OnGUI() {
		for (int i = 0; i < filled; i++) {
			GUI.DrawTexture(entries[i].OccupiedRect, omniTexture);
		}
	}
}