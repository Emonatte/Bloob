using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour {

	public float gravity = -10;

	public void attract(Transform body, Rigidbody rb)
	{
		// Räkna ut var kroppen befinner sig i relation till planeten och normaliserar den.
		Vector3 normalized_body_position = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;
		rb.AddForce (normalized_body_position * gravity);

		// LÄS PÅ OM QUATERNION FÖÖR FAAAN!
		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, normalized_body_position) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);
	}
}
