using UnityEngine;

public class HudScript : MonoBehaviour {

	
	public GameObject panelMenu, panelGame, panelPause, panelGameOver, panelShop, btnLogout, panelWatting, panelOferta;
	public GameController controller;


	void Update () {

		if(controller.currentState == GameController.GameStates.Menu){
			panelMenu.SetActive(true);
			panelGame.SetActive(false);
			panelPause.SetActive(false);
			panelGameOver.SetActive(false);
			panelShop.SetActive(false);
			panelWatting.SetActive(false);
			panelOferta.SetActive(false);

		}else if(controller.currentState == GameController.GameStates.Watting){
			panelMenu.SetActive(false);
			panelGame.SetActive(true);
			panelPause.SetActive(false);
			panelGameOver.SetActive(false);
			panelShop.SetActive(false);
			panelWatting.SetActive(true);
			panelOferta.SetActive(false);
			
		}else if(controller.currentState == GameController.GameStates.InGame){
			panelMenu.SetActive(false);
			panelGame.SetActive(true);
			panelPause.SetActive(false);
			panelGameOver.SetActive(false);
			panelShop.SetActive(false);
			panelWatting.SetActive(false);
			panelOferta.SetActive(false);
			
		}else if(controller.currentState == GameController.GameStates.InPause){
			panelMenu.SetActive(false);
			panelGame.SetActive(false);
			panelPause.SetActive(true);
			panelGameOver.SetActive(false);
			panelShop.SetActive(false);
			panelWatting.SetActive(false);
			panelOferta.SetActive(false);


		}else if(controller.currentState == GameController.GameStates.GameOver){
			panelMenu.SetActive(false);
			panelGame.SetActive(false);
			panelPause.SetActive(false);
			panelGameOver.SetActive(true);
			panelShop.SetActive(false);
			panelWatting.SetActive(false);
			panelOferta.SetActive(false);

		}else if(controller.currentState == GameController.GameStates.Shop){
			panelMenu.SetActive(false);
			panelGame.SetActive(false);
			panelPause.SetActive(false);
			panelGameOver.SetActive(false);
			panelShop.SetActive(true);
			panelWatting.SetActive(false);
			panelOferta.SetActive(false);

		}else if(controller.currentState == GameController.GameStates.Offertas){
			panelOferta.SetActive(true);
			panelMenu.SetActive(false);
			panelGame.SetActive(false);
			panelPause.SetActive(false);
			panelGameOver.SetActive(false);
			panelShop.SetActive(false);
			panelWatting.SetActive(false);
		}
	
	}
}
