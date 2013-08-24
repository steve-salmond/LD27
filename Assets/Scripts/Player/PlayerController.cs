using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/** The world (moved when player travels forward/backward). */
	public Transform World;
	
	/** Maximum angular speed for the player w.r.t. the world. (degrees per second) */
	public float RunSpeedMax = 18;
	
	/** Time taken for player to accelerate to max run speed (seconds). */
	public float RunSmoothTime = 1;
	
	
	/** Player's target angular speed (degrees per second) */
	private float targetSpeed = 0;

	/** Player's current angular speed (degrees per second) */
	private float runSpeed = 0;
	
	/** Player's angular acceleration. */
	private float runAcceleration = 0;	


	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Control player's movement over the world.
		targetSpeed = Input.GetAxis("Horizontal") * RunSpeedMax;
		runSpeed = Mathf.SmoothDamp(runSpeed, targetSpeed, ref runAcceleration, RunSmoothTime);
		
		// Update world transform according to run input.
		World.Rotate(0, 0, runSpeed * Time.deltaTime);
			
		
	}
}
