using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

	public Vector3 windDirection;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Airplane") {
			collision.gameObject.GetComponent<AirplanePhysical>().runningWind = windDirection;
		}
	}
}
