using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

	public int totalBankBalance;
	public int currentLevelEarning;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		totalBankBalance = PlayerPrefs.GetInt ("Money");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Money200()
	{
		currentLevelEarning = 200;
		totalBankBalance += currentLevelEarning;
		PlayerPrefs.SetInt ("Money", totalBankBalance);
	}
}
