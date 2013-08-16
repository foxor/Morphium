using UnityEngine;
using System.Collections.Generic;

public class LootTrap : MonoBehaviour {
	protected const int TRAP_SIZE = 5;
	
	protected int filled;
	protected Item[] items;
	
	public void Awake() {
		filled = 0;
		items = new Item[TRAP_SIZE];
	}
	
	public void AddItem(Item item) {
		// Cases:
		// filled = 0: start at -1, drop out of loop and put the item in.  This is the first item, so it goes in
		// filled = TRAP_SIZE: walk up the trap, looking for the first item worth more than this one
		int place;
		for (place = Mathf.Min(TRAP_SIZE - 1, filled - 1); place >= 0 && item.Value > items[place].Value; place--) {
			if (place < TRAP_SIZE - 1) {
				items[place + 1] = items[place];
			}
		}
		items[place + 1] = item;
		filled = Mathf.Min(TRAP_SIZE, filled + 1);
	}
}