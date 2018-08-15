using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {


	public Transform explosion;

	private GameObject target;

	protected enum EventSequence { MOVE_TO_TARGET, HIT_TARGET }
	private EventSequence eventSeq;
	//private float moveTime = 0;

	private Vector2 endPos;

	void Start() {
		GetComponent<Rigidbody>().velocity = new Vector2(2, 0);
		eventSeq = EventSequence.MOVE_TO_TARGET;
	}

	public void setTarget(GameObject targ) {
		target = targ;

		// aim for center of target
		endPos = target.transform.position + target.GetComponent<SpriteRenderer>().bounds.size/2;
	}

	// Update is called once per frame
	void Update() {

		switch (eventSeq) {
			case EventSequence.MOVE_TO_TARGET:
				//moveTime += Time.deltaTime;
			
				// check if hit target
				if (endPos.x <= transform.position.x) {
					eventSeq = EventSequence.HIT_TARGET;
					Instantiate(explosion, GetComponent<Transform>().position,
						explosion.rotation);
				}
				break;
			case EventSequence.HIT_TARGET:
				Destroy(gameObject);

				break;


		}


	}
}
