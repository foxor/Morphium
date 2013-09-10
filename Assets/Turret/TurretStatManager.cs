using UnityEngine;
using System;
using System.Collections.Generic;

public class TurretStatManager : StatManager {
	
	protected const int TURRET_STAT_LEVEL = 100;
	
	protected override Dictionary<StatType, int> GetBoosts() {
		return new Dictionary<StatType, int>(){
			{StatType.Attack, TURRET_STAT_LEVEL},
			{StatType.Bandwidth, TURRET_STAT_LEVEL},
			{StatType.Health, TURRET_STAT_LEVEL},
			{StatType.Morphium, TURRET_STAT_LEVEL},
			{StatType.Sensors, TURRET_STAT_LEVEL},
			{StatType.Source, TURRET_STAT_LEVEL},
			{StatType.Speed, TURRET_STAT_LEVEL},
			{StatType.Torque, TURRET_STAT_LEVEL}
		};
	}
}