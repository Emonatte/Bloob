using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	#region Variabler

	private Vector3 standardPosition;
	private Vector3 clampedPosition;
	public Transform playerTransform;

	public bool IsColliding{ get; set; }
	public Vector3 globalPosition{ get; set; }

	private BoxCollider boxKoll; 
	#endregion

	void Start () 
	{
		boxKoll = GetComponent<BoxCollider> ();
		standardPosition = transform.localPosition;
		clampedPosition = transform.localPosition / 2;
		IsColliding = false;
		globalPosition = transform.position;
	}
		
	void Update ()
	{
		
	}
}
