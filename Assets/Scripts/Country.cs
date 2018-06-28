using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country {

	public string name;
	public List<City> cities;
	public City selected_city;

	public Country(string name){
		this.name = name;
		cities = new List<City> ();
	}

	public void addCity(City city){
		this.cities.Add (city);
		this.selected_city = city;
	}

	public void setSelectedCity(string city_name){
		bool found = false;
		foreach (City c in this.cities) {
			if (c.getName() == city_name) {
				selected_city = c;
				found = true;
			}
		}
		if (!found) {
			selected_city = cities[0];
		}
	}

	public void selectNextCity(){
		int idx = -1;
		for (int i=0; i < cities.Count; i++) {
			if (cities[i].getName() == selected_city.getName()) {
				idx = i;
			}
		}
		if (idx >= cities.Count-1) {
			selected_city = cities [0];
		} else if (idx == -1) {
			selected_city = cities [0];
		} else {
			selected_city = cities [idx+1];
		}
	}

	public void selectPrevCity(){
		int idx = -1;
		for (int i=0; i < cities.Count; i++) {
			if (cities[i].getName() == selected_city.getName()) {
				idx = i;
			}
		}
		if (idx >= cities.Count-1) {
			selected_city = cities [cities.Count-1];
		} else if (idx == -1) {
			selected_city = cities [cities.Count-1];
		} else {
			selected_city = cities [idx-1];
		}
	}

}
