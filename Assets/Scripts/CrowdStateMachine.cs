using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdStateMachine : BaseCrowdMember {

	
	void Start () {

		head = transform.Find("Head");
		body = transform.Find("Body");
		armLeft = transform.Find("ArmLeft");
		hand = armLeft.Find("Hand");
		armRight = transform.Find("ArmRight");
		mood = transform.Find("ThoughtBubble").Find("MoodIndicator");

		headSprites = Resources.LoadAll<Sprite>(headFile);
		bodySprites = Resources.LoadAll<Sprite>(bodyFile);
		moodSprites = Resources.LoadAll<Sprite>(moodFile);

		head.GetComponent<SpriteRenderer>().sprite = headSprites[0];
		body.GetComponent<SpriteRenderer>().sprite = bodySprites[1];
		hand.GetComponent<SpriteRenderer>().sprite = objectSprite;
		mood.GetComponent<SpriteRenderer>().sprite = moodSprites[(int)currentMood];

	}
	
	
	void Update () {
		//currentMood += 4;
	}
}
