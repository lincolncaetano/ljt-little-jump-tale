using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver2Script : MonoBehaviour {

	public Text txtScoreTotal;
    public Text txtGemaTotal;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		txtScoreTotal.text = PlayerPrefs.GetInt("TotalScore").ToString();
        txtGemaTotal.text = PlayerPrefs.GetInt("TotalGema").ToString();
	}
}
