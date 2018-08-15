using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoController : MonoBehaviour {

	Vector2 startPos;
	

	void Start() {
		startPos = GetComponent<Transform>().position;
	}

	
	void Update() {
		if (Input.GetKeyDown("1")) {
			GetComponent<Animator>().SetTrigger("GatoPunch");
			GetComponent<Transform>().position = new Vector2(0, 0);
			StartCoroutine(returnToStart());
		}
	}


	IEnumerator returnToStart() {
		yield return new WaitForSeconds(2);
		GetComponent<Transform>().position = startPos;
	}
}
