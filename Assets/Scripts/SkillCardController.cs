using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillCardController : MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler {

	public static int numSelected = 0;

	public GameObject manager;
	private BattleController battleMan;

	private Skill skill;

	private Text skillName;
	private Image idolProfile;
	private Text alignText;
	private Text driveText;
	private Image cardBG;
	private Color cardColor;
	public Color cardSelectColor;
	public bool selected = false;


	void Start () {
		battleMan = manager.GetComponent<BattleController>();

		skillName = transform.Find("SkillNameText").GetComponent<Text>();
		idolProfile = transform.Find("IdolProfileImage").GetComponent<Image>();
		alignText = transform.Find("AlignmentTypeText").GetComponent<Text>();
		driveText = transform.Find("DriveTypeText").GetComponent<Text>();
		cardBG = gameObject.GetComponent<Image>();
		cardColor = cardBG.color;
	}
	

	public void setSkill(Skill skll) {
		deselect();
		skill = skll;
		skillName.text = skill.actionType.ToString();
		alignText.text = skill.alignment.ToString();
		driveText.text = skill.drive.ToString();
		idolProfile.sprite = skill.idolProfile;
	}


	public void OnPointerDown(PointerEventData eventData) {
		if (selected)
			return;
		selected = true;
		cardBG.color = cardSelectColor;
		battleMan.cardSelected(skill);
	}

	public void deselect() {
		cardBG.color = cardColor;
		selected = false;
	}
}
