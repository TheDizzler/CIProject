using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

	private enum BattleState { STARTING, WAITING, PLAYER_SELECTING_ACTIONS, PERFORMING_ACTIONS }
	BattleState currentState;

	private const string backgroundLayer = "Background";

	private List<IdolStateMachine> idols;
	//private List<GameObject> positionHolders;

	/// <summary>
	/// Randomly selected skill holders.
	/// </summary>
	private SkillCardController[] skillCards = new SkillCardController[5];
	/// <summary>
	/// All active idol's skills.
	/// </summary>
	private Skill[] allSkills;
	/// <summary>
	/// Currently selected skills for current round.
	/// </summary>
	private List<Skill> selectedSkills = new List<Skill>();

	private GameObject idolHUDPanel, skillHUDPanel, position1, position2, position3, enemyPosition1;
	private Vector3 originalHUDPos, hudOffscreenPos;

	void Start() {
		//positionHolders = new List<GameObject>(GameObject.FindGameObjectsWithTag("PositionPlaceholder"));

		idolHUDPanel = GameObject.Find("IdolHUDs");
		skillHUDPanel = GameObject.Find("SkillHUD");
		originalHUDPos = skillHUDPanel.transform.localPosition;
		RectTransform rt = skillHUDPanel.transform.GetComponent<RectTransform>();
		hudOffscreenPos = originalHUDPos;
		//hudOffscreenPos.x += rt.sizeDelta.x * rt.localScale.x;
		hudOffscreenPos.x = rt.rect.width;
		print(hudOffscreenPos.x + "");

		skillCards[0] = GameObject.Find("SkillCard (0)").GetComponent<SkillCardController>();
		skillCards[1] = GameObject.Find("SkillCard (1)").GetComponent<SkillCardController>();
		skillCards[2] = GameObject.Find("SkillCard (2)").GetComponent<SkillCardController>();
		skillCards[3] = GameObject.Find("SkillCard (3)").GetComponent<SkillCardController>();
		skillCards[4] = GameObject.Find("SkillCard (4)").GetComponent<SkillCardController>();



		position1 = GameObject.Find("position1");
		position1.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = backgroundLayer;
		position2 = GameObject.Find("position2");
		position2.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = backgroundLayer;
		position3 = GameObject.Find("position3");
		position3.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = backgroundLayer;

		enemyPosition1 = GameObject.Find("enemyPosition1");
		enemyPosition1.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = backgroundLayer;


		idols = new List<IdolStateMachine>();

		GameObject idol = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Idol Achan.prefab");
		GameObject clone = GameObject.Instantiate(idol, position1.transform.position, idol.transform.rotation);
		IdolStateMachine ism = clone.GetComponent<IdolStateMachine>();
		ism.setHUD(GameObject.Find("IdolHUD (1)").transform);

		idols.Add(ism);


		idol = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Idol Bchan.prefab");
		clone = GameObject.Instantiate(idol, position2.transform.position, idol.transform.rotation);
		ism = clone.GetComponent<IdolStateMachine>();
		ism.setHUD(GameObject.Find("IdolHUD (2)").transform);

		idols.Add(ism);


		idol = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/Idol Cchan.prefab");
		clone = GameObject.Instantiate(idol, position3.transform.position, idol.transform.rotation);
		ism = clone.GetComponent<IdolStateMachine>();
		ism.setHUD(GameObject.Find("IdolHUD (3)").transform);

		idols.Add(ism);

		currentState = BattleState.STARTING;


	}


	float hudTimeTraveling, timeForUIOffscreen = .5f;
	int nextActor = 0;
	IdolStateMachine currentActor = null;

	void Update() {

		switch (currentState) {
			case BattleState.STARTING:
				if (allSkills == null) {
					allSkills = new Skill[15];
					allSkills[0] = idols[0].skills[0];
					allSkills[1] = idols[0].skills[1];
					allSkills[2] = idols[0].skills[2];
					allSkills[3] = idols[0].skills[3];
					allSkills[4] = idols[0].skills[4];
					allSkills[5] = idols[1].skills[0];
					allSkills[6] = idols[1].skills[1];
					allSkills[7] = idols[1].skills[2];
					allSkills[8] = idols[1].skills[3];
					allSkills[9] = idols[1].skills[4];
					allSkills[10] = idols[2].skills[0];
					allSkills[11] = idols[2].skills[1];
					allSkills[12] = idols[2].skills[2];
					allSkills[13] = idols[2].skills[3];
					allSkills[14] = idols[2].skills[4];
				}
				currentState = BattleState.WAITING;
				break;
			case BattleState.WAITING:
				int a, b, c, d, e;

				a = Random.Range(0, 14);
				b = Random.Range(0, 14);
				while (a == b) {
					b = Random.Range(0, 14);
				}
				c = Random.Range(0, 14);
				while (c == a || c == b) {
					c = Random.Range(0, 14);
				}
				d = Random.Range(0, 14);
				while (d == a || d == b || d == c) {
					d = Random.Range(0, 14);
				}
				e = Random.Range(0, 14);
				while (e == a || e == b || e == c || e == d) {
					e = Random.Range(0, 14);
				}

				skillCards[0].setSkill(allSkills[a]);
				skillCards[1].setSkill(allSkills[b]);
				skillCards[2].setSkill(allSkills[c]);
				skillCards[3].setSkill(allSkills[d]);
				skillCards[4].setSkill(allSkills[e]);

				selectedSkills.Clear();

				currentState = BattleState.PLAYER_SELECTING_ACTIONS;
				//skillHUDPanel.transform.localPosition = originalHUDPos;
				break;
			case BattleState.PLAYER_SELECTING_ACTIONS:
				//hudTimeTraveling = 0;
				if (hudTimeTraveling > 0) {
					hudTimeTraveling -= Time.deltaTime;
					float t = hudTimeTraveling / timeForUIOffscreen;
					Vector3 newPos = Vector3.Lerp(originalHUDPos, hudOffscreenPos, t);
					skillHUDPanel.transform.localPosition = newPos;
				}
				break;
			case BattleState.PERFORMING_ACTIONS:
				if (hudTimeTraveling < timeForUIOffscreen) {
					hudTimeTraveling += Time.deltaTime;
					float t = hudTimeTraveling / timeForUIOffscreen;
					Vector3 newPos = Vector3.Lerp(originalHUDPos, hudOffscreenPos, t);
					skillHUDPanel.transform.localPosition = newPos;
				}
					
				if (currentActor.currentState != IdolStateMachine.IdolState.ACTION) {
					if (nextActor < selectedSkills.Count) {
						currentActor = selectedSkills[nextActor++].readyPerformance();
					} else {
						// Idols finished their actions
						currentState = BattleState.WAITING;
					}

				}

				break;

		}
	}

	public void cardSelected(Skill skll) {

		if (selectedSkills.Count >= 3) {
			// too many skills added
		} else {
			selectedSkills.Add(skll);
			if (selectedSkills.Count >= 3) {
				currentState = BattleState.PERFORMING_ACTIONS;
				//foreach (Skill skill in selectedSkills) {
				//	skill.readyPerformance();
				//}
				nextActor = 0;
				currentActor = selectedSkills[nextActor++].readyPerformance();
			}
		}
	}


}
