using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class TextWrapper{
	// Wrap text by line length and total lines
	public static string WrappText(string input, int lineLength, int n_lines){
		
		string[] words = input.Split(' ');
		string result = "";
		string line = "";
		int count = 1;
		
		foreach(string w in words){
			string temp = line + " " + w;
			
			if (temp.Length > lineLength) {
				result += line + "\n";
				line = w;
				count += 1;
			}else if(temp[temp.Length -1] == '\n'){
				result += temp;
				line = "";
				count += 1;
			}else{
				line = temp;
			}

			if(count >= n_lines-1){
				break;
			}
		}
		result += line;
		
		return result.Substring(1, result.Length-1);
	}
}
