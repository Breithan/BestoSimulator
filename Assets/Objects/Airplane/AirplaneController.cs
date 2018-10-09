using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour {

	// --- Componentes
	AirplanePhysical airplanePhysical = null;
	AirplaneOnButton airplaneOnButton = null;
	AirplanePropeller airplanePropeller = null;
	FunctionBank functionBank = null;

	// --- Fisicas
	Vector3 speed;
	Vector3 rotation;
	Vector3 phenomena;

	// --- Panel de control
	public GameObject onButton;
	public GameObject propeller;
	public GameObject handle;
	public GameObject compass;
	public GameObject speedometer;
	public GameObject altimeter;

	// --- Protocolo de despegue
	bool motorOn;
	bool takeoff;

	void Start () {
		// --- Fisicas
		speed = new Vector3();
		rotation = new Vector3();
		phenomena = new Vector3();
		// --- Componentes
		airplanePhysical = GetComponent<AirplanePhysical>();
		airplaneOnButton = onButton.GetComponent<AirplaneOnButton>();
		airplanePropeller = propeller.GetComponent<AirplanePropeller>();
		functionBank = gameObject.AddComponent<FunctionBank>();
		// --- Protocolo de despegue
		motorOn = false;
		takeoff = false;
	}
	
	void Update () {
		// --- Panel de control
		float handleRotation = functionBank.Map(airplanePhysical.potency, 0, 1, -15, 60);
		handle.transform.localRotation = Quaternion.Euler(new Vector3(handleRotation, 0, 0));
		compass.transform.localRotation = Quaternion.Euler(new Vector3(0, -rotation.y, 0));
		float speedometerRotation = functionBank.Map(speed.z, 0, airplanePhysical.speedMax, -90, 90);
		speedometer.transform.localRotation = Quaternion.Euler(new Vector3(0, speedometerRotation, 0));
		float altimeterPosition = functionBank.Map(transform.position.y, 0, 50, -0.25f, 0.25f);
		altimeter.transform.localPosition = new Vector3(0, 0.5f, altimeterPosition); 

		// --- Protocolo de despegue
		motorOn = airplaneOnButton.on;
		airplanePhysical.motorOn = motorOn;

		if (speed.z > airplanePhysical.speedMax * 0.75f) {
			if (transform.position.y > 2) {
				takeoff = true;
			} else {
				takeoff = false;
			}
		}

		// --- Objetos del avion...
		airplanePropeller.SetMove(motorOn);
		
	}

	public Vector3 Speed() {
		if (!motorOn) {
			speed = airplanePhysical.SpeedMotorOff();
			return speed;
		}

		if (!takeoff) {
			speed = airplanePhysical.SpeedTakeOn();
			return speed;
		} else {
			speed = airplanePhysical.SpeedTakeOff();
			return speed;
		}
	}

	public Vector3 Rotation() {
		if (!motorOn) {
			rotation = airplanePhysical.RotationMotorOff();
			return rotation;
		}

		if (!takeoff) {
			rotation = airplanePhysical.RotationTakeOn();
			return rotation;
		} else {
			rotation = airplanePhysical.RotationTakeOff();
			return rotation;
		}
	}

	public Vector3 Phenomena() {
		if (!motorOn) {
			phenomena = airplanePhysical.PhenomenaMotorOff();
			return phenomena;
		}

		if (!takeoff) {
			phenomena = airplanePhysical.PhenomenaTakeOn();
			return phenomena;
		} else {
			phenomena = airplanePhysical.PhenomenaTakeOff();
			return phenomena;
		}
	}
}