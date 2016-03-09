using UnityEngine;
using System.Collections;

[AddComponentMenu("Physics/FauxBody")]
public class body : MonoBehaviour {

	public attract attractor;
	private Transform obj_trans;
	private Rigidbody obj_rb;
	void Start () 
	{
		obj_trans = this.transform;
		obj_rb = GetComponent<Rigidbody> ();

		obj_rb.constraints = RigidbodyConstraints.FreezeRotation;
		obj_rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		attractor.Attract (obj_trans, obj_rb);
	}
}
