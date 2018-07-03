using UnityEngine;
using System.Collections;
//using Soomla.Store;

public class prisaoScript : MonoBehaviour {


	public string name_goomo;


	void OnTriggerEnter2D(Collider2D other) {
		
		if(other.tag == "Player"){
			//StoreInventory.GiveItem(name_goomo,1);
			GetComponent<Animator>().SetBool("free", true);
		}
	}
}
