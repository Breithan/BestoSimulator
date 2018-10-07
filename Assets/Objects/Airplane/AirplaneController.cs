using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour {

	AirplanePhysics airplanePhysics = null;

	// ---------- Actores internos que alteral el movimiento ---------- //
	public Vector3 speed;
	public Vector3 rotation;

	void Start () {
		speed = new Vector3();
		rotation = new Vector3();
		airplanePhysics = GetComponent<AirplanePhysics>();
	}
	
	void Update () {
		speed = airplanePhysics.Speed();
		rotation = airplanePhysics.Rotation();
		transform.Translate(speed);
		transform.position += airplanePhysics.Phenomena();
		transform.rotation = Quaternion.Euler(rotation);
	}
}
