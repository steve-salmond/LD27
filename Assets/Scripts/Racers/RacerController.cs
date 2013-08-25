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
	
	/** Jump sound. */
	public AudioClip JumpSound;
	
	/** The racer's animated model. */
	public GameObject Model;
	
	/** Distance travelled by racer so far (fractional laps). */
	public double Progress
		{ get; private set; }
	
	private Transform t;
	
	private float nextJumpTime = 0;
	
	private Vector3 checkpoint;
	
	private float nextCheckpointTime = 0;
	private float checkpointInterval = 0.5f;
	private float progressThreshold = 0.5f;
	
	private Vector3 last;
	
	
	// Use this for initialization
	void Start () {
		t = transform;
		Progress = 0;
		last = t.position;
	}
	
	void FixedUpdate () {
		
		// Get racer's axes.
		Vector3 up = t.position.normalized;
		Vector3 right = Vector3.Cross(up, Vector3.forward);
		
		// Racers always run as fast as they can.
		float f = RunImpulse;
		float s = Time.deltaTime * 60;
		rigidbody.AddForce(right * f * s);
		
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
		
		// Update overall race progress.
		if (!PlayerController.Instance.Finished)
			UpdateProgress();
				
	}
	
	void Jump()
	{
		if (Time.time < nextJumpTime)
			return;
	
		// Apply jump impulse.
		Vector3 up = t.position.normalized;
		float s = Time.deltaTime * 60;
		rigidbody.AddForce(up * JumpImpulse * s);
		nextJumpTime = Time.time + JumpInterval;
		
		audio.PlayOneShot(JumpSound);
	}
	
	void UpdateProgress()
	{
		// Measure angle travelled since last frame.
		float delta = Vector3.Angle(last, t.position);
		
		// Check if we're travelling forwards.
		Vector3 v = Model.transform.InverseTransformDirection(rigidbody.velocity);
		bool forwards = v.x <= 0;
		
		// Update overall angle travelled.
		float scale = forwards ? 1 : -1;
		Progress += (delta * scale); 
		
		// Update last known position.
		last = t.position;
	}
	
}
