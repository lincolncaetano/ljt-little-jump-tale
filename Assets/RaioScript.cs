using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioScript : MonoBehaviour {

	private float jumpForce = 40f;
	 void OnTriggerEnter2D(Collider2D col)
    {
		if(col.tag == "Player"){
			col.GetComponent<Player_control>().timeRaio = 2f;
			Rigidbody2D rigi = col.GetComponent<Rigidbody2D>();
			if(rigi != null){
				Vector2 velocity = rigi.velocity;
				velocity.y = jumpForce;
				rigi.velocity = velocity;
			}
			Destroy(this.gameObject);
		}

    }
}
