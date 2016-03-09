using UnityEngine;
using System.Collections;

[AddComponentMenu("Physics/FauxBody")]
public class body : MonoBehaviour {

	public attract attractor;
	private Transform obj_trans;
	private Rigidbody obj_rb;

	void Start () 
	{
		// Initaliserar komponenterna  
		obj_trans = this.transform;
		obj_rb = GetComponent<Rigidbody> ();

		// Hindrar att gravitationen påverkar objektets rotation
		obj_rb.constraints = RigidbodyConstraints.FreezeRotation;
		obj_rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Kallar på gravitationen
		attractor.Attract (obj_trans, obj_rb);
	}
}
