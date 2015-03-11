using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tank_Weight_Genetic : MonoBehaviour {

	public GameObject father;
	public GameObject mother;

	private GameObject []tanks_array;
	private GameObject player;
	private float []tanks_probability;
	private int selection_type;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Tank_Me");
		tanks_array = GameObject.FindGameObjectsWithTag("Tank");
		tanks_probability = new float[tanks_array.Length];

		selection_type = 0;
	}

	void Update () {
	
	}
	void OnGUI(){
		if (GUI.Button(new Rect(Screen.width/7.0f * 5.0f, Screen.height/2.0f, 300, 20), "Match the Highest Score")){
			selection_type = 0;
		}
		if (GUI.Button(new Rect(Screen.width/7.0f * 5.0f, Screen.height/2.0f + 50.0f, 300, 20), "Match the Player's Score")){
			selection_type = 1;
		}
	}

	public void WeightSelection(){
		if( selection_type == 0 ){
			WeightSelectionHighest();
		}
		else{
			WeightSelectionPlayer();
		}
	}
	void WeightSelectionPlayer(){
		father = null;
		mother = null;
		
		float sum = 0;
		float probability_sum = 0;
		
		for( int i = 0; i < tanks_array.Length; i++ ){
			float score = Mathf.Abs( tanks_array[i].transform.GetComponent<Tank_Player_Controller>().score - 
			                      player.transform.GetComponent<Tank_Player_Controller>().score );
			if( score < 0.1f ) score += 0.1f;
			sum += 1.0f / score;
		}
		
		for( int i = 0; i < tanks_array.Length; i ++ ){

			float score = Mathf.Abs( tanks_array[i].transform.GetComponent<Tank_Player_Controller>().score - 
			                        player.transform.GetComponent<Tank_Player_Controller>().score );
			if( score < 0.1f ) score += 0.1f;
			tanks_probability[i] = probability_sum + 1.0f / score /sum;
			probability_sum = tanks_probability[i];
		}
		
		while( father == null || mother == null ){
			float number = (float)Random.Range(1,10) * 0.1f;
			for( int i = 0; i < tanks_probability.Length - 1; i ++ ){
				if( number > tanks_probability[i] && number <= tanks_probability[i+1]){
					if( father == null ) father = tanks_array[i+1];
					else mother = tanks_array[i+1];
				}
			}
		}
		Crossover();
	}

	void WeightSelectionHighest(){
		father = null;
		mother = null;
		
		float sum = 0;
		float probability_sum = 0;
		
		for( int i = 0; i < tanks_array.Length; i++ ){

			sum += tanks_array[i].transform.GetComponent<Tank_Player_Controller>().score;
		}
		
		for( int i = 0; i < tanks_array.Length; i ++ ){

			float score = tanks_array[i].transform.GetComponent<Tank_Player_Controller>().score;
			tanks_probability[i] = probability_sum + score/sum;
			probability_sum = tanks_probability[i];
		}
		
		while( father == null || mother == null ){
			float number = (float)Random.Range(1,10) * 0.1f;
			for( int i = 0; i < tanks_probability.Length - 1; i ++ ){
				if( number > tanks_probability[i] && number <= tanks_probability[i+1]){
					if( father == null ) father = tanks_array[i+1];
					else mother = tanks_array[i+1];
				}
			}
		}
		Crossover();
	}

	public void Crossover(){
		for( int i = 0; i < tanks_array.Length; i ++ ){
			//Debug.Log("Corssover");
			float []child = new float[4];
			int cross_type = (int)Mathf.Round(Random.Range(0,3));
			if( cross_type == 0){
				child = father.GetComponent<Tank_Player_Controller>().tank_chromosome;
			}
			else if( cross_type == 1){
				child = mother.GetComponent<Tank_Player_Controller>().tank_chromosome;
			}
			else if( cross_type == 2){
				if( Random.Range(0,1) < 0.5f ){
					child[0] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[0];
					child[1] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[1];
					child[2] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[2];
					child[3] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[3];
				}
				else{
					child[0] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[0];
					child[1] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[1];
					child[2] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[2];
					child[3] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[3];
				}
			}
			else{
				child[0] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[0];
				child[1] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[1];
				child[2] = father.GetComponent<Tank_Player_Controller>().tank_chromosome[2];
				child[3] = mother.GetComponent<Tank_Player_Controller>().tank_chromosome[3];
			}

			if( Random.Range(0,100) < 5 ){
				int index = (int)Mathf.Round(Random.Range(0,3));
				child[index] = Random.Range(1,10) * 0.1f;
			}

			Tank_Player_Controller tank_controller = tanks_array[i].transform.GetComponent<Tank_Player_Controller>();
			tank_controller.tank_chromosome = child;
		}
	}
}
