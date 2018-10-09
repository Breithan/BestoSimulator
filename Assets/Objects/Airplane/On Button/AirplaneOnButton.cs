using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneOnButton : MonoBehaviour {

	public GameObject airplane;
	public bool on { get; set; }
	Vector3 position;

	void Start () {
		on = false;
		position = new Vector3(0, 0.75f, 0);
	}
	
	void Update () {
		if (on) {
			position = new Vector3(0, 0.35f, 0);
		} else {
			position = new Vector3(0, 0.75f, 0);
		}

		transform.localPosition = position;
	}

	void OnMouseOver() {
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
			on = !on;

			if (on) {
				airplane.AddComponent<AirplanePotencyDetect>();
				airplane.AddComponent<AirplaneInclinationDetect>();
			} else {
				Destroy(airplane.GetComponent<AirplanePotencyDetect>());
				Destroy(airplane.GetComponent<AirplaneInclinationDetect>());
			}
		}
    }
}
