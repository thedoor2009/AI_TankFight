using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {
	
	public GameObject []Tanks_Array;
	static public bool GameOver;
	public float Timer;
	
	private GameObject player;
	private Tank_Weight_Genetic tank_weight;
	private Mine_Manager mine_manager;
	private GUIStyle largeFont; 
	
	void Start () {
		Tanks_Array = GameObject.FindGameObjectsWithTag("Tank");
		player = GameObject.FindGameObjectWithTag("Tank_Me");
		tank_weight = GameObject.FindGameObjectWithTag("Genetic").transform.GetComponent<Tank_Weight_Genetic>();
		mine_manager = GameObject.FindGameObjectWithTag("MineManager").transform.GetComponent<Mine_Manager>();
		
		GameOver = false;
		Timer = 30.0f;
		
		largeFont = new GUIStyle();
		largeFont.fontSize = 32;
	}
	
	void Update () {
		if( !GameOver ){
			Timer -= Time.deltaTime;
		}
		if( Timer < 0.1f){
			GameOver = true;
			mine_manager.Init();
			tank_weight.WeightSelection();
			Timer = 30.0f;

		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2.0f - 50.0f, Screen.height/5.0f, 100, 20), Timer.ToString(),largeFont);
		GUI.color = Color.black;
		
		float offset = 0.0f;
		GUI.Label(new Rect(Screen.width/7.0f, Screen.height/9.0f + offset, 100, 20), player.transform.GetComponent<Tank_Player_Controller>().Name);
		GUI.Label(new Rect(Screen.width/7.0f + 100.0f, Screen.height/9.0f + offset, 100, 20), player.transform.GetComponent<Tank_Player_Controller>().score.ToString());
		offset += 40.0f;
		
		for( int i = 0; i < Tanks_Array.Length; i ++ ){
			Tank_Player_Controller tank_controller = Tanks_Array[i].transform.GetComponent<Tank_Player_Controller>();
			GUI.Label(new Rect(Screen.width/7.0f, Screen.height/9.0f + offset, 100, 20), tank_controller.Name);
			GUI.Label(new Rect(Screen.width/7.0f + 100.0f, Screen.height/9.0f + offset, 100, 20), tank_controller.score.ToString());
			offset += 40.0f;
		}
		
		
		if( GameOver ){
			GUI.Label(new Rect(Screen.width/2.0f - 50.0f, Screen.height/2.0f, 100, 20), "Game Over",largeFont);
			GUI.Label(new Rect(Screen.width/2.0f - 50.0f, Screen.height/2.0f + 40.0f, 100, 20), "Your score is " + player.transform.GetComponent<Tank_Player_Controller>().score,largeFont);
			if (GUI.Button(new Rect(Screen.width/2.0f - 50.0f, Screen.height/2.0f + 100.0f, 100, 20), "Again")){
				for( int i = 0; i < Tanks_Array.Length; i ++ ){
					Tank_Player_Controller tank_controller = Tanks_Array[i].transform.GetComponent<Tank_Player_Controller>();
					tank_controller.score = 0;
				}
				GameOver = false;
			}
		}
	}
}
