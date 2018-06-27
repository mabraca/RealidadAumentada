using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class track_target_positions : MonoBehaviour {

	private TrackableBehaviour ImageTrackableBehaviour;
	private TrackableBehaviour CardTrackableBehaviour;

	// Use this for initialization
	void Start () {
		ImageTrackableBehaviour = GameObject.Find("ImageTarget_Astronaut").GetComponent<TrackableBehaviour>();
		CardTrackableBehaviour = GameObject.Find("ImageTarget_Drone").GetComponent<TrackableBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		if (ImageTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED &&
		   CardTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED) {
			Vector3 ImageToCamera = ImageTrackableBehaviour.transform.position - Camera.main.transform.position;
			Vector3 CardToCamera = CardTrackableBehaviour.transform.position - Camera.main.transform.position;
			float distance = Vector3.Distance (ImageToCamera, CardToCamera);
			Debug.Log ("Distance from target to card: " + distance);
		}
	}
}
