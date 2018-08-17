using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCrowdMember : MonoBehaviour {

	/// <summary>
	/// Source for crowd member body parts.
	/// </summary>
	public string bodyFile;
	/// <summary>
	/// Source for crowd heads.
	/// </summary>
	public string headFile;
	/// <summary>
	/// Source for moods
	/// </summary>
	public string moodFile;

	protected Sprite[] bodySprites;
	protected Sprite[] headSprites;
	protected Sprite[] moodSprites;
	protected Sprite[] handObjectSprites;

	protected Transform head, body, armLeft, armRight, hand, mood;



	public Skill.Alignment baseAlignment;
	public Skill.Drive baseDrive;

	/// <summary>
	/// Decide intial mood by venue, location, fame, ...?
	/// HATE = 0
	/// Bored = 10
	/// INDIFFERENT = 30
	/// INTERESTED = 60
	/// LOVE = 120
	/// OBSESSED = 200
	/// </summary>
	public enum Mood { HATE, BORED, INDIFFERENT, INTERESTED, LOVE, OBSESSED}
	public Mood currentMood = Mood.HATE;
	protected int moodCounter;


}
