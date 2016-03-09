using UnityEngine;
using System.Collections;

public class attract : MonoBehaviour {

	public float gravity = -10;

	public void Attract(Transform body, Rigidbody rb)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;
		rb.AddForce (gravityUp * gravity);

		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);
	}
}
