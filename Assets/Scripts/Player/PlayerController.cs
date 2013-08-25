using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/** Impulse to apply to player to get him running. */
	public float RunImpulse = 18;
	
	/** Time between jumps. */
	public float JumpInterval = 2;
	
	/** Impulse to apply to player to get him jumping. */
	public float JumpImpulse = 18;
	
	/** Jump sound. */
	public AudioClip JumpSound;
	
	/** The player's animated model. */
	public GameObject Model;
	
	/** Is player racing? */
	public bool Racing
		{ get; private set; }

	/** Has player finished racing? */
	public bool Finished
		{ get; private set; }
	
	/** Player's current lap. */
	public int Lap
		{ get { return (int) Mathf.Max(0, Mathf.Min(10, Mathf.Ceil(10 - LapProgress))); } }

	/** Player's current placing. */
	public int Place
		{ get { return GetPlace(); } }
	
	/** Distance travelled by player so far (degrees). */
	public float Progress
		{ get; private set; }
	
	/** Fractional laps travelled by player so far. */
	public float LapProgress
		{ get { return Progress / 360; } }
	
	
	private Transform t;
	
	private float nextJumpTime = 0;

	private float moveThreshold = 1f;
	
	private RacerController[] Racers;
	
	private Vector3 last;
	
	private float startTime = 0;
	
	/** Player controller instance. */
	public static PlayerController Instance;
	
	void Awake() {
		Instance = this;
		
		// Locate other competitors.
		Racers = FindObjectsOfType(typeof(RacerController)) as RacerController[];
	}
		
	// Use this for initialization
	void Start () {
		t = transform;
		Progress = 0;
		last = t.position;
		startTime = Time.time;
	}
	
	void Update () {
		
		// Schedule level reload on race completion.
		if (!Finished && Lap <= 0)
			Invoke("Reload", 10);

		// Are we racing?
		Finished = (Lap <= 0);
		float t = Time.time - startTime;
		Racing = (t >= 10) && !Finished;
		
		// Go fullscreen on enter.
		if (Input.GetKeyDown(KeyCode.Return))
			Screen.fullScreen = !Screen.fullScreen;
		
		// Can the player race?
		if (Racing)
			UpdateInput();
		
		// Update player's animation.
		UpdateAnimation();
		
		// Update progress.
		if (!Finished)
			UpdateProgress();
	}
	
	/** Reload the game. */
	void Reload()
		{ Application.LoadLevel(Application.loadedLevel); }
		
	
	void UpdateInput()
	{
		// Get player's axes.
		Vector3 up = t.position.normalized;
		Vector3 right = Vector3.Cross(up, Vector3.forward);
		
		// Control player's movement over the world.
		float f = Input.GetAxis("Horizontal") * RunImpulse;
		rigidbody.AddForce(right * f);
		
		// Add player jump.
		if (Input.GetButtonDown("Jump"))
			Jump();
		
	}
	
	void Jump()
	{
		if (Time.time < nextJumpTime)
			return;
	
		// Apply jump impulse.
		Vector3 up = t.position.normalized;
		rigidbody.AddForce(up * JumpImpulse);
		nextJumpTime = Time.time + JumpInterval;

		audio.PlayOneShot(JumpSound);
	}
	
	void UpdateAnimation()
	{
		bool moving = rigidbody.velocity.magnitude > moveThreshold;
		Animation animation = Model.animation;
		
		if (moving && !animation.IsPlaying("Run"))
			animation.Play("Run", AnimationPlayMode.Mix);
		else if (!moving && !animation.IsPlaying("Idle"))
			animation.Play("Idle", AnimationPlayMode.Mix);
	} 
	
	int GetPlace()
	{
		int place = 1;
		
		foreach (RacerController racer in Racers)
			if (racer.Progress > Progress)
				place++;
		
		return place;
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
		
		// Debug.Log("Progress: " + Progress + ", delta: " + delta + ", v: " + v);
	}
	
}
