using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

    private GameController gameController;

    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fechar() {
        gameController.currentState = GameController.GameStates.Menu; 
    }
}
