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
	
	/** Distance travelled by racer so far (fractional laps). */
	public float Progress
		{ get; private set; }
	
	private Transform t;
	
	private float nextJumpTime = 0;
	
	private Vector3 checkpoint;
	
	private float nextCheckpointTime = 0;
	private float checkpointInterval = 0.5f;
	private float progressThreshold = 0.5f;
	
	
	// Use this for initialization
	void Start () {
		t = transform;
		Progress = 0;
	}
	
	void Update () {
		
		// Get racer's axes.
		Vector3 up = t.position.normalized;
		Vector3 right = Vector3.Cross(up, Vector3.forward);
		
		// Racers always run as fast as they can.
		float f = RunImpulse;
		rigidbody.AddForce(right * f);
		
		// Add random jump.
		float pr = JumpProbability * Time.deltaTime;
		if (Random.value < pr)
			Jump();

		// Update checkpoint progress.
		if (Time.time > nextCheckpointTime)
		{
			if (nextCheckpointTime > 0)
			{
				float d = Vector3.Distance(checkpoint, t.position);
				if (d < progressThreshold)
					Jump();
			}
			
			checkpoint = t.position;
			nextCheckpointTime = Time.time + checkpointInterval;
		}
				
	}
	
	void Jump()
	{
		if (Time.time < nextJumpTime)
			return;
	
		// Apply jump impulse.
		Vector3 up = t.position.normalized;
		rigidbody.AddForce(up * JumpImpulse);
		nextJumpTime = Time.time + JumpInterval;
	}
	
	/*
	void OnCollisionEnter(Collision collision)
	{
		// Jump when racer hits an obstacle.
		if (collision.collider.tag == "Obstacle")
			Jump();
	}
	*/
	
}
