using UnityEngine;
using System.Collections;

public class DoA : Ability {
	
	public DoA(StatManager s) : base(s){}
	public DoA(StatManager s, GameObject g) : base(s, g){}
	
	protected override void Cast (Vector3 t) {
		Object.Destroy(gameObject);
	}
	
	protected override int Cost () {
		return 0;
	}
	
	public override void Update () {}
}