using UnityEngine;
using System.Collections.Generic;

public class ProjectileDamage : DamageDealer {
	public Damage damage;
	
	protected override void Enter(GameObject other) {
		CharacterEventListener listener = other.GetComponent<CharacterEventListener>();
		if (listener != null) {
			listener.Broadcast(CharacterEvents.Hit, new CharacterEvent(){Damage = damage, Source = this});
			Destroy(gameObject);
		}
	}
}