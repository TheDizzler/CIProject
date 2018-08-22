using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdolStateMachine : BaseIdol {

	/// <summary>
	/// WAITING == waitng for turn, input, etc
	/// ACTION == performing assigned action
	/// FAINTED == Stamina/Spirit at 0
	/// </summary>
	public enum IdolState { WAITING, ACTION, FAINTED }

	public IdolState currentState;

	protected float armLeftLoweredAngle = -30;
	protected float armLeftRaisedAngle = 40;
	protected float armRightLoweredAngle = 215;
	protected float armRightRaisedAngle = 140;


	public void setHUD(Transform hud) {

		idolHUD = hud;
		idolHUD.Find("NameHolder").GetComponent<Text>().text = idolName;
		staminaText = idolHUD.Find("StaminaPanel").Find("StaminaText").GetComponent<Text>();
		staminaMeter = idolHUD.Find("StaminaPanel").Find("StaminaMeter").GetComponent<Image>();
		spiritText = idolHUD.Find("SpiritPanel").Find("SpiritText").GetComponent<Text>();
		spiritMeter = idolHUD.Find("SpiritPanel").Find("SpiritMeter").GetComponent<Image>();
		idolHUD.Find("IdolProfile").GetComponent<Image>().sprite = profileImage;

	}


	void Start() {
		currentState = IdolState.WAITING;

		float stamPercent = (float)currentStamina / maxStamina;
		staminaMeter.transform.localScale = new Vector3(Mathf.Clamp(stamPercent, 0, 1), 1, 1);
		float spiritPercent = (float)currentSpirit / maxSpirit;
		spiritMeter.transform.localScale = new Vector3(Mathf.Clamp(spiritPercent, 0, 1), 1, 1);


		staminaText.text = currentStamina + " / " + maxStamina;
		spiritText.text = currentSpirit + " / " + maxSpirit;

		head = transform.Find("Head");
		body = transform.Find("Body");
		armLeft = transform.Find("Arm left");
		hand = armLeft.Find("Hand");
		armRight = transform.Find("Arm right");


		headSprites = Resources.LoadAll<Sprite>(headFile);
		bodySprites = Resources.LoadAll<Sprite>(bodyFile);
		head.GetComponent<SpriteRenderer>().sprite = headSprites[2];
		body.GetComponent<SpriteRenderer>().sprite = bodySprites[1];
		hand.GetComponent<SpriteRenderer>().sprite = micSprite;

		skills = new Skill[5];

		Skill skill = new Skill(this, Skill.ActionType.SING, baseDrive, baseAlignment, profileImage);
		skills[0] = skill;

		skill = new Skill(this, Skill.ActionType.DANCE, baseDrive, baseAlignment, profileImage);
		skills[1] = skill;

		skill = new Skill(this, Skill.ActionType.CROWD_BOOST, baseDrive, baseAlignment, profileImage);
		skills[2] = skill;

		skill = new Skill(this, Skill.ActionType.DANCE, Skill.Drive.KNOWLEDGE, baseAlignment, profileImage);
		skills[3] = skill;

		skill = new Skill(this, Skill.ActionType.SING, Skill.Drive.FAME, baseAlignment, profileImage);
		skills[4] = skill;
	}



	protected Skill performing = null;
	

	void Update() {

		switch (currentState) {

			case IdolState.WAITING:

				break;

			case IdolState.ACTION:
				if (perform()) {
					currentState = IdolState.WAITING;

				}

				break;

			case IdolState.FAINTED:

				break;
		}
	}

	public void readyPerformance(Skill perform) {
		performing = perform;
		currentState = IdolState.ACTION;
	}


	/// <summary>
	/// Returns true when completed
	/// </summary>
	/// <returns></returns>
	public bool perform() {

		switch (performing.actionType) {
			case Skill.ActionType.DANCE:
				return dance();
			case Skill.ActionType.CROWD_BOOST:
				return dance();
			case Skill.ActionType.SING:
				return dance();
			default:
				return true;

		}
	}


	float actionTime = 0;
	float timeToPerformAction = .25f;
	bool reverse = false;
	private int cycleCount = 0;
	private int maxCycles = 3;
	private bool dance() {

		if (actionTime <= 0) {
			reverse = false;
			++cycleCount;
		} else if (actionTime >= timeToPerformAction) {
			reverse = true;
		}

		if (reverse)
			actionTime -= Time.deltaTime;
		else
			actionTime += Time.deltaTime;

		armLeft.rotation = Quaternion.Euler(0f, 0f,
			Mathf.Lerp(armLeftLoweredAngle, armLeftRaisedAngle,
				actionTime / timeToPerformAction));
		armRight.rotation = Quaternion.Euler(0f, 0f,
			Mathf.Lerp(armRightLoweredAngle, armRightRaisedAngle,
				actionTime / timeToPerformAction));

		if (cycleCount > maxCycles) {
			cycleCount = 0;
			return true;
		}
		return false;
	}


	public float getWidth() {
		return body.GetComponent<Renderer>().bounds.size.x;
	}
}
