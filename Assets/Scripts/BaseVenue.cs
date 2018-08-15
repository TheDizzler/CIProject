using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVenue : MonoBehaviour {

	public string venueName;
	public Skill.Alignment baseAlignment;
	public Skill.Drive baseDrive;
	public int maxOccupancy;
	protected int currentOccupancy;
}
