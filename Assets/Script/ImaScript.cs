using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player"){
			if(col.GetComponent<PlayerController>() != null){
				col.GetComponent<PlayerController>().timeIma = 5f;
			}else{
				col.GetComponent<Player_control>().timeIma = 5f;
			}
			Destroy(this.gameObject);
		}

	}
}
