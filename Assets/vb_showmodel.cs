using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_showmodel : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject vb_go;
	public Animator model_anim;

	// Use this for initialization
	void Start () {
		vb_go = GameObject.Find ("VirtualButton_AnimateModel");
		vb_go.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
		model_anim = GameObject.Find ("Chicken").GetComponent<Animator> ();
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb){
		model_anim.enabled = true;
		Debug.Log("It flies!");
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb){
		model_anim.enabled = false;
		Debug.Log("Oh come on ):");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
