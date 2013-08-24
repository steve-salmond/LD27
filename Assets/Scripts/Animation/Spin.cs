using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	/** Spin speed, degrees per second. */
	public Vector3 Speed;

	void Update () {
		transform.Rotate(Speed * Time.deltaTime);
	}
}
