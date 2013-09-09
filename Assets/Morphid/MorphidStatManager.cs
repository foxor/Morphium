using UnityEngine;
using System.Collections.Generic;

public class MorphidStatManager : StatManager {
	
	protected override Dictionary<StatType, int> GetBoosts() {
		return GetComponent<ItemManager>().Boosts();
	}
}