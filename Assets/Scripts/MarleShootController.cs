using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarleShootController : MonoBehaviour {

	public Transform fireballTrans;
	//public GameObject fireball;
	private bool attacked = false;
	private Vector2 startPos;
	private GameObject target;
	private Vector3 attackPosOffset = new Vector3(-1.5f, 0, 0);
	private float sequenceTime = 0;

	/// <summary>
	/// WAIT == not your turn
	/// RETURN == returning to original position
	/// </summary>
	protected enum AttackSequence { WAIT, MOVE_TO_ATTACK, ATTACK, RETURN };
	AttackSequence attSeq = AttackSequence.WAIT;

	// Use this for initialization
	void Start() {
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown("space")) {
			attSeq = AttackSequence.MOVE_TO_ATTACK;
			// get target position
			target = GameObject.FindGameObjectWithTag("Enemy");
			
			sequenceTime = 0;
			attacked = false;

			//GetComponent<Animator>().SetTrigger("MarleShoot");
			//GetComponent<Transform>().position = new Vector2(2, 0);
			//GetComponent<Rigidbody>().velocity = new Vector2(12, 0);
			//Instantiate(fireball, new Vector2(2.5f, 0), fireball.rotation);
		}

		switch (attSeq) {
			case AttackSequence.MOVE_TO_ATTACK:
				// move to attack position
				sequenceTime += Time.deltaTime;
				//GetComponent<Transform>().position = Vector2.Lerp(startPos,
				//	target.transform.position + attackPosOffset, sequenceTime / moveTime);
				if (sequenceTime >= moveTime) {
					// when movement done, go to ATTACK sequence
					attSeq = AttackSequence.ATTACK;
					sequenceTime = 0;
				}
				break;
			case AttackSequence.ATTACK:
				if (!attacked) {
					attacked = true;
					GetComponent<Animator>().SetTrigger("MarleShoot");
					Transform clone = Instantiate(fireballTrans,
						GetComponent<Transform>().position + new Vector3(.5f, 1),
						fireballTrans.rotation);
					clone.GetComponent<FireballController>().setTarget(target);
					//fireballTrans.gameObject.setTarget(target);
				}
				break;
			case AttackSequence.RETURN:
				sequenceTime += Time.deltaTime;
				//GetComponent<Transform>().position = Vector2.Lerp(
				//	target.transform.position + attackPosOffset, startPos, sequenceTime / moveTime);
				if (sequenceTime >= moveTime) {
					attSeq = AttackSequence.WAIT;
					sequenceTime = 0;
				}
				break;
			case AttackSequence.WAIT:
			// do nothing
			default:
				break;
		}
	}

	float moveTime = .25f;

	void returnToStartPos() {
		//GetComponent<Transform>().position = startPos;
		//StartCoroutine(returnFromAttack());
		attSeq = AttackSequence.RETURN;
		sequenceTime = 0;

	}

	IEnumerator returnFromAttack() {

		yield return new WaitForSeconds(1);
		Vector2 currentPos = GetComponent<Transform>().position;

		GetComponent<Rigidbody>().velocity = new Vector2(2, 0);
		//GetComponent<Transform>().p


	}


}
