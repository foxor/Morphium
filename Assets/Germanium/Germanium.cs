using UnityEngine;
using System.Collections.Generic;

public class Germanium : MonoBehaviour {
	protected const float BASE_SIZE = 0.3f;
	protected const float SIZE_RANGE = 4f;
	
	protected const int DEFAULT_SIZE = 10000;
	
	protected int startingResources;
	
	protected int resource;
	public int Resource {
		get {
			return resource;
		}
		set {
			if (value <= 0) {
				return;
			}
			if (startingResources == 0) {
				startingResources = value;
			}
			resource = value;
			float scale = Scale;
			transform.localScale = new Vector3(scale, scale, scale);
			transform.position = new Vector3(transform.position.x, scale, transform.position.z);
		}
	}
	
	protected float Scale {
		get {
			return Easing(((float)resource) / ((float)startingResources)) * SIZE_RANGE + BASE_SIZE;
		}
	}
	
	public void Start() {
		if (Resource == 0) {
			Resource = DEFAULT_SIZE;
		}
		GetComponent<CharacterEventListener>().AddCallback(CharacterEvents.Hit, OnHit);
	}
	
	protected void OnHit(CharacterEvent data) {
	}
	
	static float Easing(float interp) {
		return (Erf(interp * 4f - 2f) + 1f) / 2f;
	}
	
	static float Erf(float x) {
        // constants
        float a1 = 0.254829592f;
        float a2 = -0.284496736f;
        float a3 = 1.421413741f;
        float a4 = -1.453152027f;
        float a5 = 1.061405429f;
        float p = 0.3275911f;

        // Save the sign of x
        int sign = 1;
        if (x < 0)
            sign = -1;
        x = Mathf.Abs(x);

        // A&S formula 7.1.26
        float t = 1f / (1f + p*x);
        float y = 1f - (((((a5*t + a4)*t) + a3)*t + a2)*t + a1)*t*Mathf.Exp(-x*x);

        return sign*y;
    }
}