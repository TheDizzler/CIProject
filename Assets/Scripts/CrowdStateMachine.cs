using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdStateMachine : BaseCrowdMember {

	public enum CrowdState { WAITING, ACTION }
	public CrowdState crowdState;

	private Vector3 orgHandPos;
	private float orgRotation;

	void Start() {

		head = transform.Find("Head");
		body = transform.Find("Body");
		armLeft = transform.Find("ArmLeft");
		hand = armLeft.Find("Hand");
		armRight = transform.Find("ArmRight");
		mood = transform.Find("ThoughtBubble").Find("MoodIndicator");

		headSprites = Resources.LoadAll<Sprite>(headFile);
		bodySprites = Resources.LoadAll<Sprite>(bodyFile);
		moodSprites = Resources.LoadAll<Sprite>(moodFile);
		handObjectSprites = Resources.LoadAll<Sprite>("Crowd objects");

		head.GetComponent<SpriteRenderer>().sprite = headSprites[0];
		body.GetComponent<SpriteRenderer>().sprite = bodySprites[1];
		mood.GetComponent<SpriteRenderer>().sprite = moodSprites[(int)currentMood +1];
		int randObj = UnityEngine.Random.Range(0, handObjectSprites.Length/* * 2*/);
		if (randObj < handObjectSprites.Length)
			hand.GetComponent<SpriteRenderer>().sprite = handObjectSprites[randObj];
		orgHandPos = hand.position;
		orgRotation = hand.rotation.z;

		crowdState = CrowdState.WAITING;


		

	}

	IdolStateMachine target = null;
	float flightTime = 0;

	void Update() {

		switch (crowdState) {
			case CrowdState.ACTION:
				flightTime += Time.deltaTime;
				hand.position = Vector3.Lerp(orgHandPos, target.transform.position, flightTime);
				hand.rotation = Quaternion.Euler(0f, 0f, hand.rotation.z + flightTime*1000);
				break;
			case CrowdState.WAITING:
				break;

		}
	}

	internal void startAction() {
		crowdState = CrowdStateMachine.CrowdState.ACTION;
		if (target == null)
			target = BattleController.idols[UnityEngine.Random.Range(0, BattleController.idols.Count - 1)];
	}

	/// <summary>
	/// Crowd skills:
	///		Throw object
	///		Insult
	///		Boo
	///		Ignore
	///		Nothing
	///		Tap Foot
	///		Nod Head
	///		
	///		Cheer
	///		Dance
	///		Crowd Surf
	///		
	/// </summary>

	//private bool throwObject() {

	//	IdolStateMachine target = BattleController.idols[Random.Range(0, BattleController.idols.Count - 1)];
	//}

}
