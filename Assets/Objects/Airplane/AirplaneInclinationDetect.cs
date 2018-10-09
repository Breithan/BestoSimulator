using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneInclinationDetect : MonoBehaviour {

	public float horInclination { get; set; } // Horizontal
	public float verInclination { get; set; } // Vertical

	AirplanePhysical airplanePhysical = null;

	void Start () {
		horInclination = 0;
		verInclination = 0;
		airplanePhysical = GetComponent<AirplanePhysical>();
	}
	
	void Update () {
		if (Input.GetAxis("Horizontal") > 0) {
			horInclination = Mathf.MoveTowards(horInclination, -1, Time.deltaTime * 0.75f);
		} else if (Input.GetAxis("Horizontal") < 0) {
			horInclination = Mathf.MoveTowards(horInclination, 1, Time.deltaTime * 0.75f);
		} else {
			horInclination = Mathf.MoveTowards(horInclination, 0, Time.deltaTime * 0.75f);
		}

		if (Input.GetAxis("Vertical") > 0) {
			verInclination = Mathf.MoveTowards(verInclination, 1, Time.deltaTime * 0.75f);
		} else if (Input.GetAxis("Vertical") < 0) {
			verInclination = Mathf.MoveTowards(verInclination, -1, Time.deltaTime * 0.75f);
		} else {
			verInclination = Mathf.MoveTowards(verInclination, 0, Time.deltaTime * 0.75f);
		}

		airplanePhysical.horInclination = horInclination;
		airplanePhysical.verInclination = verInclination;
	}
}
 