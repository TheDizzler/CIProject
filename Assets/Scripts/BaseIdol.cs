using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseIdol : MonoBehaviour {

	public string idolName;

	public int maxStamina;
	public int currentStamina;

	public int maxSpirit;
	public int currentSpirit;

	public Skill[] skills;
	public Skill.Alignment baseAlignment;
	public Skill.Drive baseDrive;

	protected Transform head, body, armLeft, armRight, hand;

	/* Set these for each individual idol. */
	public Sprite profileImage;
	public string bodyFile;
	public string headFile;


	protected Sprite[] bodySprites;
	protected Sprite[] headSprites;

	/// <summary>
	/// Object being held in hand.
	/// </summary>
	public Sprite micSprite;

	/*
	 HUD related stuff
	*/
	protected Transform idolHUD;
	protected Text staminaText;
	protected Image staminaMeter;

	protected Text spiritText;
	protected Image spiritMeter;

}
