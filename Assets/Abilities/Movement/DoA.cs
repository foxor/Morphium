using UnityEngine;
using System.Collections;

public class DoA : Ability {
	public override void Cast () {
		Destroy(gameObject);
	}
}