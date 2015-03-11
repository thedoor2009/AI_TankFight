using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine_Manager : MonoBehaviour {

	public List<GameObject> Mine_List;
	public Transform Mine_Prefab;
	public float Init_Range;
	
	private int Mine_Num;

	void Start () {
		Init_Range = 7.0f;
		Init();
	}

	void Update () {
		Mine_List.RemoveAll(NullMine);
	}

	public void Init(){
		//Delete previous mines
		GameObject []Mine_Array = GameObject.FindGameObjectsWithTag("Mine");
		foreach( GameObject mine in Mine_Array){
			Destroy(mine);
		}

		//Create new mines
		Mine_Num = Random.Range(50,70);
		Vector3 Mine_Init_Position;
		for( int i = 0; i < Mine_Num; i ++ ){
			Mine_Init_Position = new Vector3( Random.Range( -Init_Range, Init_Range), 0.4f, Random.Range( -Init_Range, Init_Range ));
			Transform Mine = (Transform)Instantiate( Mine_Prefab, Mine_Init_Position, Quaternion.identity); 
			Mine.parent = transform;
		}

		//Mine list find new mines
		Mine_List.Clear();
		GameObject []New_Mine_Array = GameObject.FindGameObjectsWithTag("Mine");
		Mine_List.AddRange(New_Mine_Array);
	}
	
	public void Respawn(){
		//Create a new mine
		Vector3 Mine_Respawn_Position = new Vector3( Random.Range( -Init_Range, Init_Range), 0.4f, Random.Range( -Init_Range, Init_Range ));
		Transform Mine = (Transform)Instantiate( Mine_Prefab, Mine_Respawn_Position, Quaternion.identity);
		Mine.parent = transform;

		//Mine list find new mines
		Mine_List.Clear();
		GameObject []Mine_Array = GameObject.FindGameObjectsWithTag("Mine");
		Mine_List.AddRange(Mine_Array);
	}

	private bool NullMine(GameObject mine)
	{
		return mine == null;
	}
}
