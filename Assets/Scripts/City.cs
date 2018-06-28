using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City {

	private string name;
	private Dictionary<string, string> data;
	private string current_info;

	public City(string name, string description, string art, string cuisine){
		this.name = name;
		data = new Dictionary<string, string> ();
		data["description"] = description;
		data["art"] = art;
		data["cuisine"] = cuisine;

		current_info = data ["description"];
	}

	public string getName(){
		return this.name;
	}

	public string getDescription(){
		return data["description"];
	}

	public string getArtData(){
		return data["art"];
	}

	public string getCuisineData(){
		return data["cuisine"];
	}

	public string getCurrentInfo(){
		if (this.current_info == null) {
			this.current_info = data ["description"];
		}
		return this.current_info;
	}

	public void setCurrentInfo(string key){
		if (key == "null") {
			this.current_info = this.data ["description"];
		} else {
			this.current_info = this.data [key];
		}
	}
}
