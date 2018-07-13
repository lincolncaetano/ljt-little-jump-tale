using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaScript : MonoBehaviour {

	private float jumpForce = 40f;
	 void OnTriggerEnter2D(Collider2D col)
    {
		if(col.tag == "Player"){
			col.GetComponent<Player_control>().timeIma = 5f;
			Destroy(this.gameObject);
		}

    }
}
