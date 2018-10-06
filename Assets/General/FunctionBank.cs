using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBank : MonoBehaviour {

	public float Constrain(float valor, float start, float stop) {
		if (valor > stop) {
			valor = stop;
		}

		if (valor < start) {
			valor = start;
		}

		return valor;
	}

	public float Map(float valor, float start1, float stop1, float start2, float stop2) {
		float delta = 0;
		delta = stop1 - start1;
		valor -= delta;
		valor /= delta; // --- Valor va de 0 a 1.
        delta = stop2 - start2;
		valor *= delta;
        valor += delta; 
		return valor;
	}
}
