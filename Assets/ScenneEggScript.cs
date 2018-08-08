using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenneEggScript : MonoBehaviour {

	private GameObject player;
	private float levalWidth;
	private GameController gc; 
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		levalWidth = Camera.main.ScreenToWorldPoint(new Vector3(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().pixelWidth, 0, 5)).x;
		SpawnPlataforma();
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}


	public void StartEgg(){
		player.transform.position = GameObject.FindGameObjectWithTag("PlayerStartEgg").transform.position;
		gc.cenaEgg = true;
	}

	
	public void SpawnPlataforma(){

		int aux = 0;
		for (int i = 0; i < transform.childCount; i++) 
         {
             if(transform.GetChild(i).gameObject.tag == "Plataforma")
             {
				 aux++;
				 Transform child = transform.GetChild(i).gameObject.transform;
                 if(aux%2==0){
					Vector3 vec = new Vector3(levalWidth - 0.5f, child.position.y, child.position.z);
					transform.GetChild(i).gameObject.transform.position = vec;
				 }else{
					Vector3 vec = new Vector3(-levalWidth + 0.5f, child.position.y, child.position.z);
					transform.GetChild(i).gameObject.transform.position = vec;
				 }
             }
         }
		

	}
}
