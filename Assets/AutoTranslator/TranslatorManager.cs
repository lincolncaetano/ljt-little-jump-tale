using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            PlayerPrefs.SetString("SelectLanguage", "Brasileiro");
        }else{
			PlayerPrefs.SetString("SelectLanguage", "English");
		}
	}
	
	public void SelectLanguage(string Language)
	{
		PlayerPrefs.SetString("SelectLanguage", Language);
		
	}
}
