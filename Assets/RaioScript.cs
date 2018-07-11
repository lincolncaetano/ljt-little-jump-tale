using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioScript : MonoBehaviour {

	private float jumpForce = 40f;
	 void OnTriggerEnter2D(Collider2D col)
    {
		if(col.tag == "Player"){
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
