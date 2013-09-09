using UnityEngine;
using System.Collections.Generic;

public class BaseStatManager : StatManager {
	protected const int BASE_STAT_LEVEL = 100;
	
	protected override Dictionary<StatType, int> GetBoosts () {
		return new Dictionary<StatType, int>() {
			{StatType.Attack, BASE_STAT_LEVEL},
			{StatType.Bandwidth, BASE_STAT_LEVEL},
			{StatType.Health, BASE_STAT_LEVEL},
			{StatType.Morphium, BASE_STAT_LEVEL},
			{StatType.Sensors, BASE_STAT_LEVEL},
			{StatType.Source, BASE_STAT_LEVEL},
			{StatType.Speed, BASE_STAT_LEVEL},
			{StatType.Torque, BASE_STAT_LEVEL}
		};
	}
}