using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoActivator : MonoBehaviour {
	
	public int newTeam;
	
	public void Start() {
		StartCoroutine(Activator());
	}
	
	protected IEnumerator Activator() {
		yield return 0;
		yield return 0;
		yield return 0;
		yield return 0;
		GetComponent<TurretAI>().HackActivate(newTeam);
	}
}