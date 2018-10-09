using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePropeller : MonoBehaviour {

	float angularSpeed;
	bool move;

	void Start () {
		angularSpeed = 0;
	}
	
	void Update () {
		if (move) {
			angularSpeed = Mathf.MoveTowards(angularSpeed, 100, 0.5f);
		} else {
			angularSpeed = Mathf.MoveTowards(angularSpeed, 0, 0.25f);
		}

		transform.Rotate(new Vector3(0, 0, angularSpeed));
	}

	public void SetMove(bool on) {
		move = on;
	}
}
