using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TranslatorController : MonoBehaviour
{
	// PLEASE READ !!!
	// Before editing, please rename this file!
	// Else if i update this asset, all you changes will be overwritten!
	// PLEASE READ !!!

	// GameObject name, used for 'NeedsToContain'
	public string GameObjectTranslate;

	//Text GameObject's name needs to include "[+]" to translate.
	// Please edit the NeedsToContain here, if needed!
	public string NeedsToContain = "[+]";

	// Prefix for logging
	public string TranslatorPrefix = "[AutoTranslator]";

	public string SelectedLanguage = "Brasileiro";
	//public string DefaultLanguage = "Brasileiro";

	public Text translateText;

	// All the languages, you can add or remove any of these!
	// Remember to edit these IN EDITOR!
	public string Language_EN = "English";
	public string Language_FI = "Suomi";
	public string Language_DE = "Deutsch";
	public string Language_SE = "Svenska";
	public string Language_RU = "русский";
	public string Language_PT_BR = "Brasileiro";

	// Start function for automatic translation

	public void Start()
	{

		// No we check if the GameObject needs to be translated.
		GameObjectTranslate = this.gameObject.name;

		// Checks if GameObject name includes 'NeedsToContain'

		if (GameObjectTranslate.Contains (NeedsToContain))
		{
			translateText = GetComponentInChildren<Text> ();
		}
		else
		{
			// If no 'NeedsToContain' is found, print a log.
			Debug.Log (TranslatorPrefix + " " + GameObjectTranslate + " doesn't contain " + NeedsToContain);
			return;
		}
	}


	public void Update()
	{
		if(translateText == null){
			translateText = GetComponentInChildren<Text> ();
		}
		
		// If no language is selected, select the 'DefaultLanguage'
		//if(SelectedLanguage == "")
		SelectedLanguage = PlayerPrefs.GetString("SelectLanguage");

		// Translate Text to Languages
		if (SelectedLanguage == "English")
		{
			translateText.text = Language_EN;
			return;
		}

		if (SelectedLanguage == "Suomi")
		{
			translateText.text = Language_FI;
			return;
		}

		if (SelectedLanguage == "Deutsch")
		{
			translateText.text = Language_DE;
			return;
		}

		if (SelectedLanguage == "Svenska")
		{
			translateText.text = Language_SE;
			return;
		}

		if (SelectedLanguage == "русский")
		{
			translateText.text = Language_RU;
			return;
		}

		if (SelectedLanguage == "Brasileiro")
		{
			translateText.text = Language_PT_BR;
			return;
		}

		// If 'SelectedLanguage' is not found, call error.
		Debug.LogError ("Language not found!");
	}
}