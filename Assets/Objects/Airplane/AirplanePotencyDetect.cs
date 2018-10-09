using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePotencyDetect : MonoBehaviour {

	float potency;
	AirplanePhysical airplanePhysical = null;

	void Start () {
		potency = 0;
		airplanePhysical = GetComponent<AirplanePhysical>();
	}
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			potency = Mathf.MoveTowards(potency, 1, Time.deltaTime * 0.5f);
		} else if (Input.GetMouseButton(1)) {
			potency = Mathf.MoveTowards(potency, 0, Time.deltaTime * 0.5f);
		}

		airplanePhysical.potency = potency;
	}
}
