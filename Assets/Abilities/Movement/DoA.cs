using UnityEngine;
using System.Collections;

public class DoA : Ability {
	protected override void Cast (Vector3 t) {
		Destroy(gameObject);
	}
	
	protected override int Cost () {
		return 0;
	}
}