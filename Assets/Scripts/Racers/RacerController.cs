using UnityEngine;
using System.Collections;

public class RacerController : MonoBehaviour {
	
	/** Impulse to apply to racer to get him running. */
	public float RunImpulse = 18;
	
	/** Time between jumps. */
	public float JumpInterval = 2;
	
	/** Impulse to apply to racer to get him jumping. */
	public float JumpImpulse = 1500;
	
	/** Jump probability. */
	public float JumpProbability = 0.1f;
	
	private Transform t;
	
	private float nextJumpTime = 0;

	
	
	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	void Update () {
		UpdateInput();
	}
	
	void UpdateInput()
	{
		// Get racer's axes.
		Vector3 up = t.position.normalized;
		Vector3 right = Vector3.Cross(up, Vector3.forward);
		
		// Racers always run as fast as they can.
		float f = RunImpulse;
		rigidbody.AddForce(right * f);
		
		// Add random jump.
		float pr = JumpProbability * Time.deltaTime;
		if (Random.value < pr && Time.time >= nextJumpTime)
		{
			rigidbody.AddForce(up * JumpImpulse);
			nextJumpTime = Time.time + JumpInterval;
		}
		
	}
}
