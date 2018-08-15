using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

	public enum ActionType { SING, DANCE, CROWD_BOOST }

	public enum Drive { POWER, LOVE, MONEY, FAME, KNOWLEDGE }
	public enum Alignment { /*DESTRUCTION, CREATION, CHAOS*/ ORDER, BALANCE, CHAOS }

	public IdolStateMachine idol;
	public ActionType actionType;
	public Drive drive;
	public Alignment alignment;
	public Sprite idolProfile;


	public Skill(IdolStateMachine idl, ActionType type, Drive drv, Alignment align, Sprite idolPic) {
		idol = idl;
		actionType = type;
		alignment = align;
		drive = drv;
		idolProfile = idolPic;
	}


	public IdolStateMachine readyPerformance() {
		idol.readyPerformance(this);
		return idol;
	}






}
