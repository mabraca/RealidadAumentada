using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrapp_text : MonoBehaviour {
	
	private TextMesh text_mesh;
	private MeshRenderer text_mesh_renderer;

	// Use this for initialization
	void Start () {
		text_mesh_renderer = this.GetComponent<MeshRenderer>();
		text_mesh = this.GetComponent<TextMesh>();
		int n_chars = get_n_chars(text_mesh);
		print(string.Format("n_chars: {0}",n_chars));
		int n_lines = get_n_lines(text_mesh);
		print(string.Format("n_lines: {0}", n_lines));
		text_mesh.text = TextWrapper.WrappText(text_mesh.text, n_chars, n_lines);
		text_mesh_renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private int get_n_chars(TextMesh text_mesh){
		CharacterInfo char_info;
		GameObject text_container = this.transform.parent.gameObject;
		Bounds text_container_bounds = text_container.GetComponent<MeshFilter>().mesh.bounds;
		text_mesh.font.GetCharacterInfo('T', out char_info, text_mesh.fontSize, text_mesh.fontStyle);
		int char_width = char_info.advance;
		print(string.Format("char_width: {0}", char_width));
		
		return (int)((text_container_bounds.size.x * text_container.transform.lossyScale.x)/(char_width * text_mesh.characterSize * text_mesh.transform.lossyScale.x * 0.1f));
	}
	
	private int get_n_lines(TextMesh text_mesh){
		CharacterInfo char_info;
		GameObject text_container = this.transform.parent.gameObject;
		Bounds text_container_bounds = text_container.GetComponent<MeshFilter>().mesh.bounds;
		//text_mesh.font.GetCharacterInfo('T', out char_info, text_mesh.fontSize, text_mesh.fontStyle);
		int char_height = text_mesh.font.lineHeight;
		print(string.Format("char_height: {0}", char_height));
		
		return (int)((text_container_bounds.size.z * text_container.transform.lossyScale.z)/(char_height * text_mesh.characterSize * text_mesh.transform.lossyScale.y * 0.1f));
	}
}
