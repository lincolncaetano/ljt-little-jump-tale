using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	public GameObject btnMusica;

	public Sprite btnMusicaON;
	public Sprite btnMusicaOFF;
	public GameObject btnSom;
	public Sprite btnSomON;
	public Sprite btnSomOFF;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(PlayerPrefs.GetInt("musica") == 1){
			btnMusica.GetComponent<Image>().sprite = btnMusicaON;
		}else{
			btnMusica.GetComponent<Image>().sprite = btnMusicaOFF;
		}

		if(PlayerPrefs.GetInt("som") == 1){
			btnSom.GetComponent<Image>().sprite = btnSomON;
		}else{
			btnSom.GetComponent<Image>().sprite = btnSomOFF;
		}

	}

	public void btnMusicaActive(){
		bool mudou = false;
		if(PlayerPrefs.GetInt("musica") == 1){
			PlayerPrefs.SetInt("musica", 0);
			mudou = true;
		}
		if(!mudou && PlayerPrefs.GetInt("musica") == 0){
			PlayerPrefs.SetInt("musica", 1);
		}
	}

	public void btnSomActive(){
		bool mudou = false;
		if(PlayerPrefs.GetInt("som") == 1){
			PlayerPrefs.SetInt("som", 0);
			mudou = true;
		}
		if(!mudou && PlayerPrefs.GetInt("som")== 0){
			PlayerPrefs.SetInt("som", 1);
			return;
		}
	}
}
