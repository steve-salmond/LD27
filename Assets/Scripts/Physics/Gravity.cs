using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
	
	public float Magnitude = 9.8f;
	
	private Transform t;

	void Start()
	{
		t = transform;
	}
	
	void FixedUpdate()
	{
		float f = rigidbody.mass * Magnitude;
		rigidbody.AddForce((-t.position).normalized * f);
	}
}
