using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour {

	List<CrowdStateMachine> audience;

	Vector3 firstPosition;
	BaseVenue venue;

	void Start () {
		venue = GameObject.Find("Venue").GetComponent<BaseVenue>();
		audience = new List<CrowdStateMachine>();

		// generate audience: determine audience size by venue, popularity, ... ?
		GameObject startObj = GameObject.Find("CrowdStartPosition");
		startObj.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
		firstPosition = startObj.transform.position;
		position = firstPosition;
		int crowdNum = Random.Range(1, venue.maxOccupancy);
		for (int i = 0; i < crowdNum; ++i) {
			GameObject audMember = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(
				"Assets/Prefabs/CrowdMember.prefab");
			GameObject clone = GameObject.Instantiate(audMember, position.transform.position, idol.transform.rotation);
			IdolStateMachine ism = clone.GetComponent<IdolStateMachine>();
		}
	}

	void Update () {
		
	}
}
