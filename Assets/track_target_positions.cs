using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class track_target_positions : MonoBehaviour {

	private TrackableBehaviour RocksTrackableBehaviour;
	private TrackableBehaviour CuisineCardTrackableBehaviour;
	private TrackableBehaviour ArtCardTrackableBehaviour;
	private GameObject countryPlate;
	private Text text;

	// Use this for initialization
	void Start () {
		RocksTrackableBehaviour = GameObject.Find("ImageTarget_Veridium").GetComponent<TrackableBehaviour>();
		CuisineCardTrackableBehaviour = GameObject.Find("ImageTarget_Drone").GetComponent<TrackableBehaviour>();
		ArtCardTrackableBehaviour = GameObject.Find("ImageTarget_Oxygen").GetComponent<TrackableBehaviour>();
		countryPlate = GameObject.Find ("CountryPlate");
		text = GameObject.Find ("UIText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (RocksTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED &&
		    CuisineCardTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
			Vector3 ImageToCamera = RocksTrackableBehaviour.transform.position - Camera.main.transform.position;
			Vector3 CuisineCardToCamera = CuisineCardTrackableBehaviour.transform.position - Camera.main.transform.position;
			float distance = Vector3.Distance (ImageToCamera, CuisineCardToCamera);
			text.text = "Distance cuisine: " + distance;

			if (distance < 39) {
				countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("cuisine");
			}
		
		} else if (RocksTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED &&
				ArtCardTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
			Vector3 ImageToCamera = RocksTrackableBehaviour.transform.position - Camera.main.transform.position;
			Vector3 ArtCardToCamera = ArtCardTrackableBehaviour.transform.position - Camera.main.transform.position;
			float distance = Vector3.Distance (ImageToCamera, ArtCardToCamera);
			text.text = "Distance art: " + distance;

			if (distance < 39) {
				countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("art");
			}

		} else {
			countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("description");
		}
		
	}
}
