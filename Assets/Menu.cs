using UnityEngine;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public void Awake() {
		Application.LoadLevel("Adventure");
	}
}