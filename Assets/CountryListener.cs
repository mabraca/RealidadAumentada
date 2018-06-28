using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryListener : MonoBehaviour {

	public Country country;
	public GameObject imgTarget;

	private TextMesh text_mesh;
	private MeshRenderer text_mesh_renderer;
	int n_chars, n_lines;

	bool btnPrevState;
	bool btnNextState;

	// Use this for initialization
	void Start () {
		country = new Country ("Francia");
		country.addCity (new City (
			"Bordeoux",
			"Very GOjira",
			"Very much a lot",
			"Tasty tasty"
		));
		country.addCity (new City (
			"Paris",
			"Very Palomita",
			"Very much much a lot",
			"Tasty croissant"
		));
		country.addCity (new City (
			"Po",
			"Virgin like Virginia",
			"Idk",
			"Cajun maybe?"
		));

		imgTarget = GameObject.Find ("ImageTarget_Veridium");

		text_mesh_renderer = GameObject.Find("text").GetComponent<MeshRenderer>();
		text_mesh = GameObject.Find("text").GetComponent<TextMesh>();
		n_chars = get_n_chars(text_mesh);
		n_lines = get_n_lines(text_mesh);
		text_mesh_renderer.enabled = true;

		btnNextState = false;
		btnPrevState = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool nextPressed = imgTarget.GetComponent<vb_next>().btn_pressed;
		bool prevPressed = imgTarget.GetComponent<vb_prev>().btn_pressed;

		if (nextPressed && !btnNextState) {
			country.selectNextCity ();
		}else if(prevPressed && !btnPrevState){
			country.selectPrevCity ();
		}

		btnNextState = nextPressed;
		btnPrevState = prevPressed;

		// Update stuff
		string s_before_wrap = country.selected_city.getName() +"\n"+ country.selected_city.getCurrentInfo();
		text_mesh.text = TextWrapper.WrappText(s_before_wrap, n_chars, n_lines);
	}

	private int get_n_chars(TextMesh text_mesh){
		CharacterInfo char_info;
		GameObject text_container = this.transform.parent.gameObject;
		Bounds text_container_bounds = text_container.GetComponent<MeshFilter>().mesh.bounds;
		text_mesh.font.GetCharacterInfo('T', out char_info, text_mesh.fontSize, text_mesh.fontStyle);
		int char_width = char_info.advance;

		return (int)((text_container_bounds.size.x * text_container.transform.lossyScale.x)/(char_width * text_mesh.characterSize * text_mesh.transform.lossyScale.x * 0.1f));
	}

	private int get_n_lines(TextMesh text_mesh){
		GameObject text_container = this.transform.parent.gameObject;
		Bounds text_container_bounds = text_container.GetComponent<MeshFilter>().mesh.bounds;
		int char_height = text_mesh.font.lineHeight;

		return (int)((text_container_bounds.size.z * text_container.transform.lossyScale.z)/(char_height * text_mesh.characterSize * text_mesh.transform.lossyScale.y * 0.1f));
	}
}
