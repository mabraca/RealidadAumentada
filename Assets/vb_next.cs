using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class vb_next :MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject vb_go;
	public bool btn_pressed;
	public Text text;

	// Use this for initialization
	void Start () {
		//vb_go = GameObject.Find ("VirtualButton_Next");
		vb_go = this.transform.GetChild(2).gameObject;
		vb_go.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
		text = GameObject.Find ("UIText").GetComponent<Text> ();
		btn_pressed = false;
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb){
		text.text = "NEXT";
		btn_pressed = true;
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb){
		text.text = "-";
		btn_pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
