using UnityEngine;
using System;
using System.Collections.Generic;

public class CameraMotion : MonoBehaviour {
	public GameObject follow;
	
	protected Vector3 offset;
	
	public void Awake() {
		offset = transform.localPosition;
	}
	
	public void LateUpdate() {
		transform.position = follow.transform.position + offset;
	}
}