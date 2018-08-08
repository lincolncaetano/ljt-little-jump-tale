using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectChar : MonoBehaviour {

	public const string prefixComprado = "comprado";
	
	private CharsScript chScript;
	private Text labelValor;

	public CharsScript.CharSelect tipo;

	private GameController gc;

	private GameObject botaoValor;
	public GameObject[] listaChars;
	public CharsScript charsManager;
	private bool comprado;

	public void Start(){
		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.SetInt("TotalScore", 5000);
		//PlayerPrefs.SetInt("TotalGema", 1000);

		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		chScript = GameObject.FindGameObjectWithTag(CharsScript.tag).GetComponent<CharsScript>();
		labelValor = GetComponentInChildren<Text>();

		charsManager = GameObject.FindGameObjectWithTag(CharsScript.tag).GetComponent<CharsScript>();
	}

	public void Update(){
		if(transform.Find("BtnValor") != null){
			botaoValor = transform.Find("BtnValor").gameObject;
		}

		if(GameController.GameStates.Shop == gc.currentState){

			if(labelValor != null){
				labelValor.text = chScript.retornoValorChar(tipo.ToString()).ToString();
			}
			CharsScript.TipoMoeda moeda = chScript.retornoTipoMoeda(tipo.ToString());
			if(moeda.Equals(CharsScript.TipoMoeda.Gemma)){
				if(botaoValor!= null && botaoValor.transform.Find("Gema") != null){
					botaoValor.transform.Find("Gema").gameObject.SetActive(true);
				}
			}else if(moeda.Equals(CharsScript.TipoMoeda.Estrela)){
				if(botaoValor!= null && botaoValor.transform.Find("Estrela") != null){
					botaoValor.transform.Find("Estrela").gameObject.SetActive(true);
				}
			}


			verificaEquiped();
			verificaComprado();
		}
	}

	public void verificaEquiped(){
		if(botaoValor != null){
			string equipado = PlayerPrefs.GetString("CharSelecionado");

			if(tipo.ToString().Equals(equipado)){
				this.GetComponent<Image>().color = new Color(0.2901961F, 0.8588236F, 1F, 1);
			}
		}
	}

	public void verificaComprado(){
		if(botaoValor != null){
			if(PlayerPrefs.GetInt(prefixComprado+tipo.ToString()) == 1){
				botaoValor.SetActive(false);
				comprado = true;
			}else{
				this.GetComponent<Image>().color = Color.gray;
				Image imgChar = transform.Find("ImgChar").gameObject.GetComponent<Image>();
				if(imgChar != null){
					imgChar.color = Color.gray;
				}
				for (int i = 0; i < transform.Find("ImgChar").childCount; i++)
				{
					 transform.Find("ImgChar").GetChild(i).gameObject.GetComponent<Image>().color = Color.gray;
				}
			}
		}
	}

	public void comprarChar(){

		if(PlayerPrefs.GetInt(prefixComprado+tipo.ToString()) < 1){
			CharsScript.TipoMoeda moeda = chScript.retornoTipoMoeda(tipo.ToString());
			int scoreTotal = PlayerPrefs.GetInt("TotalScore");
			int gemaTotal = PlayerPrefs.GetInt("TotalGema");
			int valor = chScript.retornoValorChar(tipo.ToString());
			bool comprou = false;

			if(moeda.Equals(CharsScript.TipoMoeda.Estrela) && scoreTotal>= valor){
				comprou = true;
				PlayerPrefs.SetInt("TotalScore", scoreTotal- valor);
			}else if(moeda.Equals(CharsScript.TipoMoeda.Gemma) && gemaTotal>= valor){
				comprou = true;
				PlayerPrefs.SetInt("TotalGema", gemaTotal- valor);
			}

			if(comprou){
				PlayerPrefs.SetInt(prefixComprado+tipo.ToString(), 1);
				this.GetComponent<Image>().color = Color.white;
				Image imgChar = transform.Find("ImgChar").gameObject.GetComponent<Image>();
				if(imgChar != null){
					imgChar.color = Color.white;
				}
				for (int i = 0; i < transform.Find("ImgChar").childCount; i++)
				{
					 transform.Find("ImgChar").GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
				}
			}
		}
	}

	public void EquipaChar(){
		if(comprado){
			string selectAnt = PlayerPrefs.GetString("CharSelecionado");
			PlayerPrefs.SetString("CharSelecionado", tipo.ToString());
			foreach (var item in listaChars)
			{
				item.GetComponent<Image>().color = Color.white;
			}
			gc.resetAfterEquip();
		}
	}

}
