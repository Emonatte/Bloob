using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {
	#region Variabler
	public float moveSpeed = 15;
	public float jumpingForce = 20000f;

	private bool isJumping; 

	private Vector3 strafeDir; 
	private Vector3 moveDir;
	private Vector3 camStdLocPos;
	private Vector3 camLastPos;

	private Rigidbody obj_rb;

	public GameObject camera;
	#endregion

	void Start () 
	{
		isJumping = false;

		obj_rb = GetComponent<Rigidbody> ();
		camStdLocPos = camera.transform.localPosition;
		camLastPos = camera.transform.position;

	}
	void DoMovement ()
	{
		obj_rb.MovePosition (obj_rb.position + (transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime)+(transform.TransformDirection(strafeDir)*(moveSpeed/2) * Time.deltaTime));
	}

	void CheckMovement ()
	{
		moveDir = new Vector3 (0, 0, Input.GetAxisRaw ("Vertical")).normalized;
		strafeDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0).normalized;

	}

	void Jumping ()
	{

		if (Input.GetKeyDown (KeyCode.Space) && !isJumping) 
		{
			isJumping = true;
			obj_rb.AddRelativeForce (new Vector3 (0, jumpingForce * Time.deltaTime, 0));
		}
	}

	void OnCollisionStay (Collision coll)
	{

		Jumping ();

		if (coll.gameObject.name == "Planet" && isJumping) 
		{
			Vector3 hit = coll.contacts [0].normal;
			if (hit.y > 0) {
				Debug.Log ("ground");
				isJumping = false;
			} else
				Debug.Log ("wall");
		}
	}
	// Update is called once per frame
	void Update () 
	{
		CheckMovement ();
		if (camLastPos != camera.transform.position) {
			CheckCameraCollision ();
		}

		camLastPos = camera.transform.position;
	}

	void FixedUpdate()
	{
		DoMovement ();
	}

	void CheckCameraCollision(){

		RaycastHit hit;


		if (Physics.Raycast (transform.position, camera.transform.position - transform.position, out hit, maxDistance: 10f)) {
			
			camera.transform.localPosition = Vector3.LerpUnclamped (camera.transform.localPosition, transform.position.normalized, 3f * Time.deltaTime);
		} else {
			camera.transform.localPosition = Vector3.LerpUnclamped (camera.transform.localPosition, camStdLocPos, 3f * Time.deltaTime);
		}
	}
}
