using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController {

	List<CrowdStateMachine> audience;

	Vector3 firstPosition;
	BaseVenue venue;
	

	public CrowdController() {
		venue = GameObject.Find("Venue").GetComponent<BaseVenue>();
		audience = new List<CrowdStateMachine>();

		// generate audience: determine audience size by venue, popularity, ... ?
		GameObject startObj = GameObject.Find("CrowdStartPosition");
		startObj.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
		firstPosition = startObj.transform.position;
		Vector3 position = firstPosition;
		int crowdNum = Random.Range(5, venue.maxOccupancy);
		int cols = 1;
		for (int i = 0; i < 6; ++i) {
			GameObject audMember = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(
				"Assets/Prefabs/CrowdMember.prefab");
			GameObject clone = GameObject.Instantiate(audMember/*, position, audMember.transform.rotation*/);
			clone.transform.position = position;
			CrowdStateMachine csm = clone.GetComponent<CrowdStateMachine>();
			audience.Add(csm);
			position.y += .5f;
			position.x += .2f;
			position.z += .11f;
			if (position.y > -.66) {
				position = firstPosition;
				position.x = firstPosition.x + (1) * cols++;
			}
		}
	}

	public void update () {
		
	}
}
