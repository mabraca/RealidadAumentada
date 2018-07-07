using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class PositionTracker : MonoBehaviour {

	private TrackableBehaviour ImageTrackableBehaviour;
	private TrackableBehaviour CuisineCardTrackableBehaviour;
	private TrackableBehaviour ArtCardTrackableBehaviour;
	private GameObject countryPlate;
	private Text text;
	private GameObject currentModel_go;
	private GameObject artModel_go;
	private GameObject cuisineModel_go;
	private GameObject descriptionModel_go;

	// Use this for initialization
	void Start () {
		ImageTrackableBehaviour = this.GetComponent<TrackableBehaviour>();
		CuisineCardTrackableBehaviour = GameObject.Find("ImageTarget_Drone").GetComponent<TrackableBehaviour>();
		ArtCardTrackableBehaviour = GameObject.Find("ImageTarget_Oxygen").GetComponent<TrackableBehaviour>();
		countryPlate = this.transform.GetChild (0).gameObject; //GameObject.Find ("CountryPlate");

		artModel_go = this.transform.GetChild (0).GetChild (1).GetChild (0).gameObject;
		artModel_go.SetActive (false);
		cuisineModel_go = this.transform.GetChild (0).GetChild (1).GetChild (1).gameObject;
		cuisineModel_go.SetActive (false);
		descriptionModel_go = this.transform.GetChild (0).GetChild (1).GetChild (2).gameObject;
		descriptionModel_go.SetActive (true);
		currentModel_go = descriptionModel_go;
		text = GameObject.Find ("UIText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ImageTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED &&
			CuisineCardTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
			Vector3 ImageToCamera = ImageTrackableBehaviour.transform.position - Camera.main.transform.position;
			Vector3 CuisineCardToCamera = CuisineCardTrackableBehaviour.transform.position - Camera.main.transform.position;
			float distance = Vector3.Distance (ImageToCamera, CuisineCardToCamera);
			text.text = "Distance cuisine: " + distance;

			// Cuisine has approached
			if (distance < 39) {
				countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("cuisine");
				currentModel_go.SetActive (false);
				cuisineModel_go.SetActive (true);
				currentModel_go = cuisineModel_go;
			}

		} else if (ImageTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED &&
			ArtCardTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
			Vector3 ImageToCamera = ImageTrackableBehaviour.transform.position - Camera.main.transform.position;
			Vector3 ArtCardToCamera = ArtCardTrackableBehaviour.transform.position - Camera.main.transform.position;
			float distance = Vector3.Distance (ImageToCamera, ArtCardToCamera);
			text.text = "Distance art: " + distance;

			// Art has approached
			if (distance < 39) {
				countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("art");
				currentModel_go.SetActive (false);
				artModel_go.SetActive (true);
				currentModel_go = artModel_go;
			}

		} else {
			countryPlate.GetComponent<CountryListener> ().country.selected_city.setCurrentInfo ("description");
			currentModel_go.SetActive (false);
			descriptionModel_go.SetActive (true);
			currentModel_go = descriptionModel_go;
		}
	}
}
