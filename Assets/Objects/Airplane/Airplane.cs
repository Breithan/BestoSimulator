using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {

	FunctionBank functionBank = null;
	AirplaneController airplaneController = null;

	void Start () {
		airplaneController = GetComponent<AirplaneController>();
		functionBank = gameObject.AddComponent<FunctionBank>();
	}
	
	void Update () {
		transform.position += airplaneController.Phenomena();
		transform.rotation = Quaternion.Euler(airplaneController.Rotation());
		transform.Translate(airplaneController.Speed());
	}
}
