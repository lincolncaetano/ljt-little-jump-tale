using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsScript : MonoBehaviour{
	
	public static string tag = "CharManager";
	public GameObject[] chars;
	public Player[] listaChars = new Player[]{advGirl,advBoy,cat,dog,cavaleiro,dino,ninjaBoy,ninjaGirl,jack,noel,robo,cuteGirl,zBoy,zGirl,ibisen};
	public enum CharSelect{AdvGil,AdvBoy,Cat,Dog,Cavaleiro,Dino,NinjaBoy,NinjaGirl,Jack,Noel,Robo,CuteGirl,ZBoy,ZGirl,Ibisen};
	public enum TipoMoeda{Estrela, Gemma, Dinheiro};

	public static Player advGirl = new Player(CharSelect.AdvGil.ToString(), TipoMoeda.Estrela, 200);
	public static Player advBoy = new Player(CharSelect.AdvBoy.ToString(), TipoMoeda.Estrela, 500);
	public static Player cat = new Player(CharSelect.Cat.ToString(), TipoMoeda.Estrela, 1000);
	public static Player dog = new Player(CharSelect.Dog.ToString(), TipoMoeda.Gemma, 50);
	public static Player cavaleiro = new Player(CharSelect.Cavaleiro.ToString(), TipoMoeda.Estrela, 2000);
	public static Player dino = new Player(CharSelect.Dino.ToString(), TipoMoeda.Estrela, 3000);
	public static Player ninjaBoy = new Player(CharSelect.NinjaBoy.ToString(), TipoMoeda.Estrela, 3500);
	public static Player ninjaGirl = new Player(CharSelect.NinjaGirl.ToString(), TipoMoeda.Estrela, 4000);
	public static Player jack = new Player(CharSelect.Jack.ToString(), TipoMoeda.Gemma, 75);
	public static Player noel = new Player(CharSelect.Noel.ToString(), TipoMoeda.Estrela, 1700);
	public static Player robo = new Player(CharSelect.Robo.ToString(), TipoMoeda.Estrela, 1500);
	public static Player cuteGirl = new Player(CharSelect.CuteGirl.ToString(), TipoMoeda.Gemma, 125);
	public static Player zBoy = new Player(CharSelect.ZBoy.ToString(), TipoMoeda.Gemma, 350);
	public static Player zGirl = new Player(CharSelect.ZGirl.ToString(), TipoMoeda.Gemma, 350);
	public static Player ibisen = new Player(CharSelect.Ibisen.ToString(), TipoMoeda.Gemma, 20);


	public GameObject retornoChar(string id){
		GameObject retorno = new GameObject();
		int aux = 0;
		foreach(Player ch in listaChars){
			if(ch.id.ToString().Equals(id)){
				return chars[aux];
			}
			aux++;
		}

		return retorno;
	}

	public int retornoValorChar(string id){
		int aux = 0;
		foreach(Player ch in listaChars){
			if(ch.id.ToString().Equals(id)){
				return ch.valor;
			}
			aux++;
		}

		return 0;
	}

	public TipoMoeda retornoTipoMoeda(string id){
		int aux = 0;
		foreach(Player ch in listaChars){
			if(ch.id.ToString().Equals(id)){
				return ch.tipoMoeda;
			}
			aux++;
		}

		return 0;
	}

}

public class Player{

	public Player(string id,CharsScript.TipoMoeda tipoMoeda, int valor){
		this.id = id;
		this.tipoMoeda = tipoMoeda;
		this.valor = valor;
	}
	public string id;
	public CharsScript.TipoMoeda tipoMoeda;
	public int valor;
}
