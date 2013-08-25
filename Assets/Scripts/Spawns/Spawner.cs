using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public GameObject SpawnedObject;
	public float Radius = 55;	
	public float SpawnRateInitial = 0.1f;
	public float SpawnRateFinal = 1f;
	public float SpawnVarianceFraction = 0.2f;
	public float SpawnRampTime = 180f;
	

	private float startTime = 0;
	
		
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		Spawn();
	}

	
	/** Spawn an object. */
	private void Spawn()
	{
		if (PlayerController.Instance.Racing)
		{
			GameObject go = Instantiate(SpawnedObject) as GameObject;
			Vector3 p = Random.onUnitSphere; p.z = 0;
			go.transform.position += p.normalized * Radius;	
		}
		
		float elapsed = Time.time - startTime;
		float t = Mathf.Clamp(elapsed / SpawnRampTime, 0, 1);
		float rate = Mathf.Lerp(SpawnRateInitial, SpawnRateFinal, t);
		float duration = (1 / rate);
		float variance = Random.Range(-0.5f, 0.5f) * SpawnVarianceFraction;
		float next =  duration * (1 + variance);
		Invoke("Spawn", next);
	}
	
	
}
