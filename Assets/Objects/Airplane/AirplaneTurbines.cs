using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AirplaneTurbines : MonoBehaviour {

	public bool locationTurbine; // Verdadero para derecha, falso para izquierda.
	public Slider potencyUI;
	AirplanePhysics airplanePhysics = null;

	void Start () {
		GameObject airplane = GameObject.FindGameObjectWithTag("Airplane");
		airplanePhysics = airplane.GetComponent<AirplanePhysics>();
	}
	
	void Update () {
		if (locationTurbine) {
			airplanePhysics.potencyTurbineRight = Potency();
		} else {
			airplanePhysics.potencyTurbineLeft = Potency();
		}
	}

	float Potency() {
		float potency = 0;
		potency = potencyUI.value;
		return potency;
	}
}
