using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePhysical : MonoBehaviour {

	FunctionBank functionBank = null;

	public Vector3 speed;
	public Vector3 rotation;
	float gravity;
	public float mass;
	
	public float potency { get; set; }
	public float horInclination { get; set; } // Horizontal, va de 0 a 1
	public float verInclination { get; set; } // Vertical, va de 0 a 1
	public float speedMax { get; set; }
	public Vector3 runningWind { get; set; }

	public bool motorOn { get; set; } 

	void Start () {
		speed = new Vector3();
		rotation = new Vector3();
		gravity = 0.001f;
		potency = 0;
		horInclination = 0;
		verInclination = 0;
		speedMax = (mass / 2) / mass;
		runningWind = new Vector3();
		functionBank = gameObject.AddComponent<FunctionBank>();
	}
	
	void Update () {
		// Constrain en el eje y
		float y = functionBank.Constrain(transform.position.y, 0, 50);
		transform.position = new Vector3(transform.position.x, y, transform.position.z);

		if (!motorOn) {
			potency = Mathf.MoveTowards(potency, 0, Time.deltaTime * 0.5f);
			horInclination = Mathf.MoveTowards(horInclination, 0, Time.deltaTime * 0.75f);
			verInclination = Mathf.MoveTowards(verInclination, 0, Time.deltaTime * 0.75f);
		}
	}

	public Vector3 SpeedMotorOff() {
		float x = speed.x;
		float y = speed.y;
		float z = speed.z;

		float acceleration = 1 / mass; // Un escalar para calibrar
		z -= acceleration / 16; // Un escalar para calibrar
		z = functionBank.Constrain(z, 0, speedMax);

		speed = new Vector3(x, y, z);
		return speed;
	}

	// Avion en tierra
	public Vector3 SpeedTakeOn() {
		float x = speed.x;
		float y = speed.y;
		float z = speed.z;

		float acceleration = potency / mass;
		z += acceleration;
		z = functionBank.Constrain(z, 0, speedMax);

		speed = new Vector3(x, y, z);
		return speed;
	}

	public Vector3 SpeedTakeOff() {
		float x = speed.x;
		float y = speed.y;
		float z = speed.z;

		float acceleration = potency / mass;
		z += acceleration;
		z = functionBank.Constrain(z, 0, speedMax);
		
		speed = new Vector3(x, y, z);
		return speed;
	}

	public Vector3 RotationMotorOff() {
		float x = rotation.x;
		float y = rotation.y;
		float z = rotation.z;

		rotation = new Vector3(x, y, z);
		rotation = RotationTakeOff();
		return rotation;
	}

	// Avion en tierra
	public Vector3 RotationTakeOn() {
		float x = rotation.x;
		float y = rotation.y;
		float z = rotation.z;
		
		y -= horInclination * 64 / mass; // Un escalar para calibrar
		
		if (speed.z > speedMax * 0.75f) {
			x = functionBank.Map(verInclination, -1, 1, -20, 20);
		}

		rotation = new Vector3(x, y, z);
		return rotation;
	}

	public Vector3 RotationTakeOff() {
		float x = rotation.x;
		float y = rotation.y;
		float z = rotation.z;

		x = functionBank.Map(verInclination, -1, 1, -20, 20);
		y -= horInclination * 64 / mass;  // Un escalar para calibrar
		z = functionBank.Map(horInclination, -1, 1, -45, 45);

		rotation = new Vector3(x, y, z);
		return rotation;
	}

	public Vector3 PhenomenaMotorOff() {
		Vector3 phenomena = new Vector3();
		float x = 0;
		float y = 0;
		float z = 0;

		SpeedMotorOff();
		if (transform.position.y > 2) {
			y = -gravity * mass / 2 * functionBank.Map(potency, 0, 1, 1, 0); // Un escalar para calibrar
		}

		phenomena = new Vector3(x, y, z);
		phenomena += runningWind;
		return phenomena;
	}

	// Avion en tierra
	public Vector3 PhenomenaTakeOn() {
		Vector3 phenomena = new Vector3();
		float x = 0;
		float y = 0;
		float z = 0;

		SpeedMotorOff();

		phenomena = new Vector3(x, y, z);
		phenomena += runningWind;
		return phenomena;
	}

	public Vector3 PhenomenaTakeOff() {
		Vector3 phenomena = new Vector3();
		float x = 0;
		float y = 0;
		float z = 0;

		SpeedMotorOff();
		y = -gravity * mass / 2 * functionBank.Map(potency, 0, 1, 1, 0); // Un escalar para calibrar

		phenomena = new Vector3(x, y, z);
		phenomena += runningWind;
		return phenomena;
	}

}
