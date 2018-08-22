using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSongList : MonoBehaviour {

	public GameObject songItemPrefab;
	

	void Start () {

		GameObject newObj;

		for (int i = 0; i < 10; i++) {
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(songItemPrefab, transform);

			// Randomize the color of our image
			newObj.GetComponent<Image>().color = Random.ColorHSV();
		}
	}
	
	void Update () {
		
	}
}
