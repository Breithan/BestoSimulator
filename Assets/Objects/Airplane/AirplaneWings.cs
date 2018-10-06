using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneWings : MonoBehaviour {

	public string locationWing; // Verdadero para derecha, falso para izquierda.
	AirplanePhysics airplanePhysics = null;
	FunctionBank functionBank = null;

	public float positionWing;
	public float takeoffTilt;

	void Start () {
		GameObject airplane = GameObject.FindGameObjectWithTag("Airplane");
		airplanePhysics = airplane.GetComponent<AirplanePhysics>();
		functionBank = gameObject.AddComponent<FunctionBank>();
		takeoffTilt = 0;

		if (locationWing == "tail") {
			positionWing = 0;
		} else {
			positionWing = 1;
		}
	}
	
	void Update () {
		if (Takeoff() != 0 && locationWing != "tail") {
			airplanePhysics.takeoffTilt = Takeoff();
		}

		if (locationWing == "left") {
			airplanePhysics.positionWingLeft = Position();
		} else if (locationWing == "right") {
			airplanePhysics.positionWingRight = Position();
		} else {
			airplanePhysics.positionWingTail = Position();
		}
	}

	float  Takeoff() {
		if (Input.GetAxis("Vertical") > 0) {
			takeoffTilt = Mathf.MoveTowards(takeoffTilt, -10, Time.deltaTime);
		} else if (Input.GetAxis("Vertical") < 0) {
			takeoffTilt = Mathf.MoveTowards(takeoffTilt, 10, Time.deltaTime);
		} else {
			takeoffTilt = Mathf.MoveTowards(takeoffTilt, 0, Time.deltaTime);
		}

		return takeoffTilt;

	}

	float Position() {
		if (airplanePhysics.takeoff && airplanePhysics.gameObject.transform.position.y > 7) {
			if (locationWing == "left") {
				if (Input.GetAxis("Horizontal") < 0) {
					positionWing = Mathf.MoveTowards(positionWing, 0, Time.deltaTime * 0.5f);
				} else if (Input.GetAxis("Horizontal") > 0) {
					positionWing = Mathf.MoveTowards(positionWing, 2, Time.deltaTime * 0.5f);
				} else {
					positionWing = Mathf.MoveTowards(positionWing, 1, Time.deltaTime * 0.5f);
				}
			} else if (locationWing == "right") {
				if (Input.GetAxis("Horizontal") > 0) {
					positionWing = Mathf.MoveTowards(positionWing, 0, Time.deltaTime * 0.5f);
				} else if (Input.GetAxis("Horizontal") < 0) {
					positionWing = Mathf.MoveTowards(positionWing, 2, Time.deltaTime * 0.5f);
				} else {
					positionWing = Mathf.MoveTowards(positionWing, 1, Time.deltaTime * 0.5f);
				}
			} 
		} else {
			if (locationWing == "tail") {
				if (Input.GetAxis("Horizontal") < 0) {
					positionWing = Mathf.MoveTowards(positionWing, -1, Time.deltaTime * 0.5f);
				} else if (Input.GetAxis("Horizontal") > 0) {
					positionWing = Mathf.MoveTowards(positionWing, 1, Time.deltaTime * 0.5f);
				} else {
					positionWing = Mathf.MoveTowards(positionWing, 0, Time.deltaTime * 0.5f);
				}
			}
		}

		return positionWing;
	}
}
