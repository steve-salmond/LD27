using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/** Impulse to apply to player to get him running. */
	public float RunImpulse = 18;
	
	/** Time between jumps. */
	public float JumpInterval = 2;
	
	/** Impulse to apply to player to get him jumping. */
	public float JumpImpulse = 18;
	
	/** Maximum angular speed for the player. (degrees per second) */
	public float RunSpeedMax = 18;
	
	/** Threshold for player to be considered to be 'on the ground'. */
	public float GroundedDistance = 1.7f;
	
	public bool Grounded
		{ get; private set; }
	
	private Transform t;
	
	private float nextJumpTime = 0;

	
	
	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	void FixedUpdate() 
	{
		Ray ray = new Ray(t.position, -t.position);
		Grounded = Physics.Raycast(ray, GroundedDistance);
	}
	
	void Update () {
		UpdateInput();
	}
	
	void UpdateInput()
	{
		// Get player's axes.
		Vector3 up = t.position.normalized;
		Vector3 right = Vector3.Cross(up, Vector3.forward);
		
		// Control player's movement over the world.
		float f = Input.GetAxis("Horizontal") * RunImpulse;
		rigidbody.AddForce(right * f);
		
		// Add player jump.
		if (Input.GetButtonDown("Jump") && Time.time >= nextJumpTime)
		{
			rigidbody.AddForce(up * JumpImpulse);
			nextJumpTime = Time.time + JumpInterval;
		}
		
	}
}
