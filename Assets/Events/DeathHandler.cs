using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(TypeProvider))]
public class DeathHandler : MonoBehaviour {
	
	protected bool isPlayer;
	
	protected bool isDead;
	public bool IsDead {
		get {
			return isDead;
		}
	}
	
	public void Start() {
		isPlayer = (GetComponent<HealthBar>() != null);
		isDead = false;
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Die, OnDeath);
	}
	
	public void OnDeath(CharacterEvent data) {
		if (!isPlayer) {
			Destroy(gameObject);
			isDead = true;
		}
		GlobalEventListener.Listener().Broadcast(GetComponent<TypeProvider>().Type, new CharacterStatusEventData(){
			EventCharacterType = CharacterStatusEventData.CharacterType.Morphid,
			EventStatus = CharacterStatusEventData.Status.Die,
			Source = gameObject
		});
	}
	
	public void OnDestroy() {
		GetComponent<CharacterEventListener>().Broadcast(CharacterEvents.Destroy, null);
	}
}