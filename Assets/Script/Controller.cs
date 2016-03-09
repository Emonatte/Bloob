using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	#region Variabler

	public GameObject camera;
	public float move_speed = 15;
	public float jumping_force = 20000f;

	private bool isJumping; 

	private Vector3 strafe_direction; 
	private Vector3 move_direction;
	private Vector3 camera_standard_local_position;
	private Vector3 camera_last_position;

	private Rigidbody object_rigidbody;

	#endregion

	//Initialisera objektet och dess komponenter
	void Start () 
	{
		isJumping = false;

		object_rigidbody = GetComponent<Rigidbody> ();

		camera_standard_local_position = camera.transform.localPosition;
		camera_last_position = camera.transform.position;

	}
	//Updatera objektet
	void Update () 
	{
		//Testa rörelser i verticalt och horizontellt led
		CheckMovement ();
		
		//Om kameran har rört på sig sen förra framen, uppdatera dess position om collision
		//position om collision uppstått
		if (camera_last_position != camera.transform.position) {
			CheckCameraCollision ();
		}

		//Uppdatera kamerans förra position
		camera_last_position = camera.transform.position;
	}
	void FixedUpdate()
	{
		DoMovement ();
	}
	//Testa rörelse i varje led
	void CheckMovement ()
	{
		Vector3 horizontal_movement = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0).normalized;
		Vector3 vertical_movement = new Vector3 (0, 0, Input.GetAxisRaw ("Vertical")).normalized;

		strafe_direction = horizontal_movement;
		move_direction = vertical_movement;
	}
	//Utför rörelse efter att varje led har testats
	void DoMovement ()
	{
		Vector3 vertical_movement = transform.TransformDirection(move_direction) * move_speed * Time.deltaTime;
		Vector3 horizontal_movement = transform.TransformDirection(strafe_direction) * (move_speed/2) * Time.deltaTime;

		object_rigidbody.MovePosition (object_rigidbody.position + vertical_movement + horizontal_movement);
	}
	//Testa om spelaren har hoppat eller om ens möjligheten till att hoppa
	void Jumping ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !isJumping) 
		{
			isJumping = true;

			Vector3 jump = new Vector3 (0, jumping_force * Time.deltaTime, 0);
			object_rigidbody.AddRelativeForce (jump);
		}
	}
	//Upprepad kollisionstest
	void OnCollisionStay (Collision coll)
	{
		Jumping ();

		if (coll.gameObject.name == "Planet" && isJumping) 
		{
			Vector3 hit = coll.contacts [0].normal;
			if (hit.y > 0) {
				Debug.Log ("On planet ground");
				isJumping = false;
			} else
				Debug.Log ("On planet wall");
		}
	}
	//Kamerans kollisionstest
	void CheckCameraCollision(){

		RaycastHit hit;

		if (Physics.Raycast (transform.position, camera.transform.position - transform.position, out hit, maxDistance: 10f)) {
			
			camera.transform.localPosition = Vector3.LerpUnclamped (camera.transform.localPosition, transform.position.normalized, 3f * Time.deltaTime);
		} else {
			camera.transform.localPosition = Vector3.LerpUnclamped (camera.transform.localPosition, camera_standard_local_position, 3f * Time.deltaTime);
		}
	}
}
