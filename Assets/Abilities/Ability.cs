using UnityEngine;
using System;
using System.Collections;

public abstract class Castable : MonoBehaviour {
	public abstract void Cast();
}

public abstract class Ability<T> : Castable where T : AbilityData {
	protected T configuration;
	public T Configuration {
		get {
			return configuration;
		}
		set {
			configuration = value;
			OnConfigurationChange();
		}
	}
	
	public virtual void OnConfigurationChange(){}
	
	public Nullable<Vector3> Delta {
		get {
			Nullable<RaycastHit> rayCast = ClickRaycast.GetLastHit();
			if (rayCast.HasValue) {
				Vector3 val = rayCast.Value.point - transform.position;
				val.y = 0f;
				return val;
			}
			return null;
		}
	}
}

[Serializable]
public abstract class AbilityData {
}