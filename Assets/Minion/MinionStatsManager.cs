using UnityEngine;
using System;
using System.Collections.Generic;

public class MinionStatsManager : StatManager {
	
	protected const int MINION_STAT_LEVEL = 40;
	
	protected override Dictionary<StatType, int> GetBoosts() {
		return new Dictionary<StatType, int>(){
			{StatType.Attack, MINION_STAT_LEVEL},
			{StatType.Bandwidth, MINION_STAT_LEVEL},
			{StatType.Health, MINION_STAT_LEVEL},
			{StatType.Morphium, MINION_STAT_LEVEL},
			{StatType.Sensors, MINION_STAT_LEVEL},
			{StatType.Source, MINION_STAT_LEVEL},
			{StatType.Speed, MINION_STAT_LEVEL},
			{StatType.Torque, MINION_STAT_LEVEL}
		};
	}
}