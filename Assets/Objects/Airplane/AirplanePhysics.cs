using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePhysics : MonoBehaviour {

	FunctionBank functionBank = null;

	// ---------- Actores internos al avión que alteran el movimiento ---------- //
	public float potencyTurbineLeft { get; set; }
	public float potencyTurbineRight { get; set; }
	public float positionWingLeft  { get; set; }
	public float positionWingRight { get; set; }
	public float positionWingTail { get; set; }
	public float takeoffTilt { get; set; }
	public bool takeoff { get; set; }

	// ---------- Actores externos al avión que alteran el movimiento ---------- //
	public float mass;
	float gravity;
	float potencyTurbine;
	float positionWing;
	Vector3 speed;
	Vector3 rotation;
	Vector3 aceleration;
	Vector3 wind;
	Vector3 friction;

	void Start () {
		functionBank = gameObject.AddComponent<FunctionBank>();
		potencyTurbineLeft = 0;
		potencyTurbineRight = 0;
		positionWingLeft = 0;
		positionWingRight = 0;
		positionWingTail = 0;
		takeoffTilt = 0;
		takeoff = false;
		mass *= 10; 
		potencyTurbine = 0;
		positionWing = 0;
		gravity = -0.01f;
		speed = new Vector3();
		rotation = new Vector3();
		aceleration = new Vector3();
		wind = new Vector3();
		friction = new Vector3(0, 0, -0.0001f);
	}
	
	void Update () {
		potencyTurbineLeft *= positionWingLeft;
		potencyTurbineRight *= positionWingRight;
		potencyTurbine = potencyTurbineLeft + potencyTurbineRight;
		positionWing = positionWingLeft - positionWingRight;

		if (speed.z > 1.5f) {
			takeoff = true;
		}
	}

	public Vector3 Speed() {
		aceleration = new Vector3(0, 0, potencyTurbine / mass);
		speed += aceleration;
		float z = functionBank.Constrain(speed.z, 0, 2);
		speed = new Vector3(0, 0, z);
		return speed;
	}

	public Vector3 Rotation() {
		float x = rotation.x;
		float y = rotation.y;
		float z = rotation.z;

		if (takeoff) {
			x = takeoffTilt;
			y += positionWing * 0.25f;
			z = -functionBank.Map(positionWing, -2, 2, -30, 30);
		} else {
			y += (positionWingTail * 0.25f);
		}

		rotation = new Vector3(x, y, z);
		return rotation;
	}

	public Vector3 Phenomena() {
		float x = 0;
		float y = 0;
		float z = 0;
		Vector3 phenomena = new Vector3(x, y, z);

		if (takeoff) {
			y = gravity;
			phenomena = new Vector3(x, y, z);
			phenomena += wind;
			speed += friction;
		}
		
		return phenomena;
	}
}
