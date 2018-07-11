using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CountryListener : MonoBehaviour {

	public Country country;
	public GameObject imgTarget;

	private TextMesh text_mesh;
	private MeshRenderer text_mesh_renderer;
	int n_chars, n_lines;

	bool btnPrevState;
	bool btnNextState;

	public Text text;

	// Use this for initialization
	void Start () {
		string[] go_name = this.transform.parent.gameObject.name.Split ('_');
		//string path = "Assets/CountryInfo/" + go_name [go_name.Length - 1] + ".txt";
		string path = "CountryInfo/" + go_name [go_name.Length - 1];// + ".txt";
		TextAsset file = Resources.Load(path) as TextAsset;
		if (file == null) {
			Debug.Log ("File not found!");
		}
		//StreamReader reader = new StreamReader (path);
		StreamReader reader = new StreamReader (new MemoryStream(file.bytes));
		text = GameObject.Find ("UIText").GetComponent<Text> ();

		// Read Country name
		country = new Country (reader.ReadLine());

		// Read city names
		string line;
		List<string> city_names = new List<string>();
		while (true) {
			line = reader.ReadLine ();
			if (line == "=end") {
				break;
			}else if(line != ""){
				city_names.Add(line);
			}
		}

		reader.Close ();

		// Read each city
		//path = "Assets/CountryInfo/" + go_name [go_name.Length - 1] + "Cities/";
		path = "CountryInfo/" + go_name [go_name.Length - 1] + "Cities/";
		string description = "";
		string art = "";
		string cuisine = "";
		foreach (string city in city_names) {
			//TextAsset cityFile = Resources.Load (path + city + ".txt") as TextAsset;
			TextAsset cityFile = Resources.Load (path + city) as TextAsset;
			//reader = new StreamReader(path + city + ".txt");
			reader = new StreamReader(new MemoryStream(cityFile.bytes));
			while (true) {
				line = reader.ReadLine ();
				if (line == "=description") {
					description = "";
					while (true) {
						line = reader.ReadLine ();
						if (line == "=end") {
							break;
						} else {
							description += line + "\n";
						}
					}
				} else if (line == "=art") {
					art = "";
					while (true) {
						line = reader.ReadLine ();
						if (line == "=end") {
							break;
						} else {
							art += line + "\n";
						}
					}
				} else if (line == "=cuisine") {
					cuisine = "";
					while (true) {
						line = reader.ReadLine ();
						if (line == "=end") {
							break;
						} else {
							cuisine += line + "\n";
						}
					}
				} else {
					break;
				}
			}
			country.addCity (new City (city, description, art, cuisine));
			reader.Close ();
		}

		// Some stuff that i dont remember
		//imgTarget = GameObject.Find ("ImageTarget_Veridium");
		imgTarget = this.transform.parent.gameObject;

		//text_mesh_renderer = GameObject.Find("text").GetComponent<MeshRenderer>();
		text_mesh_renderer = this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer> ();
		//text_mesh = GameObject.Find("text").GetComponent<TextMesh>();
		text_mesh = this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>();
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
		/*
		Debug.Log("ACHU");
		Debug.Log (char_width);
		Debug.Log (text_container_bounds.size.x);
		Debug.Log (text_container.transform.lossyScale.x);
		Debug.Log (text_container.transform.localScale.x);
		Debug.Log (text_container_bounds.size.x * text_container.transform.lossyScale.x);
		*/
		Debug.Log ("SOMETHING");
		Debug.Log ((text_container_bounds.size.x * text_container.transform.lossyScale.x)/(char_width * text_mesh.characterSize * 0.1f));
		return 65;
		return (int)((text_container_bounds.size.x * text_container.transform.lossyScale.x)/(char_width * text_mesh.characterSize * 0.1f));
		//return (int)((text_container_bounds.size.x * text_container.transform.lossyScale.x)/(char_width * text_mesh.characterSize * text_mesh.transform.lossyScale.x * 0.1f));
	}

	private int get_n_lines(TextMesh text_mesh){
		GameObject text_container = this.transform.parent.gameObject;
		Bounds text_container_bounds = text_container.GetComponent<MeshFilter>().mesh.bounds;
		int char_height = text_mesh.font.lineHeight;

		return (int)((text_container_bounds.size.z * text_container.transform.lossyScale.z)/(char_height * text_mesh.characterSize * text_mesh.transform.lossyScale.y * 0.1f));
	}
}
