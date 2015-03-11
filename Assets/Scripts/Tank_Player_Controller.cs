using UnityEngine;
using System.Collections;

public class Tank_Player_Controller : MonoBehaviour {

	public string Name;
	public enum TankType{
		Player,
		Computer
	}
	public TankType tank_tpye;

	public float []tank_chromosome;

	public float rotation_speed;
	public float velocity;
	public int score;

	private Mine_Manager mine_manager;
	private GameObject mine_target;

	private Vector3 left_forward ;
	private Vector3 left_back ;
	private Vector3 right_forward ;
	private Vector3 right_back ;

	private float seek_range;
	private bool iswander;
	
	void Start () {
		tank_chromosome = new float[4] ;
		for( int i = 0; i < 4; i++ ){
			tank_chromosome[i] = Random.Range(1, 10 ) * 0.1f;
		}

		mine_manager = GameObject.FindGameObjectWithTag("MineManager").transform.GetComponent<Mine_Manager>();

		rotation_speed = 1.0f;
		velocity = 2.0f;

		left_forward = new Vector3(0.0f, rotation_speed, 0.0f );
		left_back = new Vector3(0.0f, -rotation_speed, 0.0f );
		right_forward = new Vector3(0.0f, -rotation_speed, 0.0f );
		right_back = new Vector3(0.0f, rotation_speed, 0.0f );

		seek_range = 2.5f;
		iswander = true;
		score = 0;
	}

	void Update () {
		if( !Game_Manager.GameOver){
			if( tank_tpye == TankType.Player){
				GetInput();
				BorderCheck();
			}
			else if( tank_tpye == TankType.Computer ){
				TankAI();
				BorderCheck();
			}
		}
	}
	void TankAI(){

		if(iswander){
			Wander();
		}
		else{
			SeekMine( mine_target);
		}
	}

	void Wander(){
		transform.position += -transform.right * velocity * Time.deltaTime;

		Collider[] mine_colliders = Physics.OverlapSphere(transform.position, seek_range * tank_chromosome[0]);
		if( mine_colliders.Length > 0 ){
			foreach( Collider mine in mine_colliders ){
				if( mine.transform.tag == "Mine"){
					mine_target =  mine.gameObject;
					iswander = false;
				}
			}
		}
	}
	
	void SeekMine( GameObject i_target ){
		if( i_target == null ){
			//Debug.Log("Get the mine");
			iswander = true;
			return;
		}

		Vector3 to_mine = i_target.transform.position - transform.position;
		float angle = Vector3.Angle( -transform.right,to_mine);
		float final_angle = angle * Mathf.Sign(Vector3.Cross(-transform.right, to_mine).y);

		if( Mathf.Abs(final_angle) > 10.0f ){
			if( final_angle > 0.0f ){
				transform.eulerAngles += left_forward * tank_chromosome[1];
				transform.eulerAngles += right_back * tank_chromosome[2];
			}
			else{
				transform.eulerAngles += left_back * tank_chromosome[1];
				transform.eulerAngles += right_forward * tank_chromosome[2];
			}
		}
		else{
			transform.position += -transform.right * velocity * tank_chromosome[3] * Time.deltaTime;
			if( i_target == null ){
				Debug.Log("Get the mine");
				iswander = true;
				return;
			}
		}
		//Debug.Log("angle: " + final_angle);
		//Debug.DrawLine( transform.position, transform.position + -transform.right);
		//Debug.DrawLine( i_target.transform.position, transform.position );
	}

	void GetInput(){

		if( Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W)){
			transform.position += -transform.right * velocity * Time.deltaTime;
		}
		else{
			if( Input.GetKey(KeyCode.Q)){
				transform.eulerAngles += left_forward;
			}

			if( Input.GetKey(KeyCode.A)){
				transform.eulerAngles += left_back;
			}

			if( Input.GetKey(KeyCode.W)){
				transform.eulerAngles += right_forward;
			}

			if( Input.GetKey(KeyCode.S)){
				transform.eulerAngles += right_back;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		score++;
		if( other.transform.tag == "Mine"){
			Destroy(other.gameObject);
			mine_manager.Respawn();
		}
	}

	void BorderCheck(){
		if( transform.position.x > 10.0f ){
			transform.position = new Vector3( -10.0f, transform.position.y, transform.position.z);
		}
		if( transform.position.x < -10.0f ){
			transform.position = new Vector3( 10.0f, transform.position.y, transform.position.z);
		}
		if( transform.position.z > 6.0f ){
			transform.position = new Vector3( transform.position.x, transform.position.y, -6.0f);
		}
		if( transform.position.z < -6.0f ){
			transform.position = new Vector3( transform.position.x, transform.position.y, 6.0f);
		}
	}
}
