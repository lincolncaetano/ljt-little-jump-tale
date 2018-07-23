﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformSript : MonoBehaviour {

	public float velocidade = 1.5f;         // The relative speed at which the camera will catch up.
	private Transform player;           // Reference to the player's transform.
	private Vector3 newPos;
	private GameController gc;
	private float timerUp = 0;

	void Start(){
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	void Update(){
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
	void FixedUpdate ()
	{
		if(player != null){	
			if(gc.currentState == GameController.GameStates.InGame){
				if(timerUp > 3){
					Vector3 newPos;
					Debug.Log(player.position.y - transform.position.y);
					if(player.position.y - transform.position.y > 10){
						newPos = new Vector3(transform.position.x, player.position.y + 5f , transform.position.z);
					}else{
						newPos = new Vector3(transform.position.x, transform.position.y + velocidade , transform.position.z);
					}
					transform.position = Vector3.Lerp(transform.position, newPos , Time.deltaTime);
				}else{
					timerUp+= Time.deltaTime;
				}
			}
		}
	}
	 void OnTriggerEnter2D(Collider2D col)
    {	
		if(col.gameObject.tag.Equals("Player")){
			gc.currentState = GameController.GameStates.GameOver;
		}else if(col.GetComponent<ScripPlataforma>()!= null && !col.GetComponent<ScripPlataforma>().inicio){
			Destroy(col.gameObject);
		}

		if(col.gameObject.tag.Equals("Item")){
			Destroy(col.gameObject);
		}
    }

	public void Reset(){
		Vector3 posicaoInicial = new Vector3(0, -4.94f, 0);
		transform.position = posicaoInicial;
		timerUp = 0f;
	}

}
