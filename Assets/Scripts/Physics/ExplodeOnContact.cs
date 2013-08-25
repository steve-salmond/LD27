using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {
	
	public float ExplosionArmDelay = 1;
	public float ExplosionForce = 1000;
	public float ExplosionRadius = 10;
	public float DieDelay = 1;
	public GameObject DieEffect;
	
	private float startTime = 0;
	private bool exploded = false;
	
	void Start()
	{
		startTime = 0;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (Time.time < (startTime + ExplosionArmDelay))
			return;
		
		if (exploded)
			return;
		
		Vector3 p = transform.position;
        Collider[] colliders = Physics.OverlapSphere(p, ExplosionRadius);
        foreach (Collider hit in colliders) 
            if (hit && hit.rigidbody)
                hit.rigidbody.AddExplosionForce(ExplosionForce, p, ExplosionRadius);
		
		exploded = true;
		
		if (particleSystem)
			particleSystem.enableEmission = false;
		if (renderer)
			renderer.enabled = false;
		
		MeshRenderer mr = GetComponent<MeshRenderer>();
		if (mr)
			mr.enabled = false;
		
		if (DieEffect)
			Instantiate(DieEffect, transform.position, Quaternion.identity);
		
		// Invoke("Die", DieDelay);
		Die ();
		
	}
	
	void Die()
	{
		Destroy(gameObject);
	}
	
}
