﻿using UnityEngine;
using UnityEngine.EventSystems;

public class btnActionScript : MonoBehaviour , IPointerDownHandler{

	public Player_control playerCont;
	public bool left;
	public bool especial;

	public void OnPointerDown(PointerEventData data){

		if(!especial){
			if(left){
				playerCont.BtnLeft();
				
			}else{
				playerCont.BtnRigth();
			}
		}else{
			playerCont.btnEspecial();
		}



	}
}
