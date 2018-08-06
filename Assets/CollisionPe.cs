using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPe : MonoBehaviour {

	// Use this for initializationprivate GameController gc;
	private GameController gc;
	public PlayerController pc;
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	 void OnCollisionEnter2D(Collision2D col)
    {
		Debug.Log(col.gameObject.tag);
		Debug.Log( col.relativeVelocity.y);
		if(gc.currentState == GameController.GameStates.InGame && col.relativeVelocity.y > 0f){
			if(col.gameObject.tag == "Plataforma"){
				pc.onLanding();
			}
		}
    }
}
