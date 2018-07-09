using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaScript : MonoBehaviour {

	public PlayerJump player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerJump>();	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(player.grounded);
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        player.grounded = true;
    }

	void OnTriggerStay2D(Collider2D col)
    {
        player.grounded = true;
    }

	void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
    }
}
