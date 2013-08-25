using UnityEngine;
using System.Collections;

public class FlagController : MonoBehaviour {
	
	
	
	public Vector3 MinWind = new Vector3(-10, 0, 0);
	
	public Vector3 MaxWind = new Vector3(-10, 0, 0);
	
	private InteractiveCloth cloth;
	
	void Start()
	{
		cloth = GetComponent<InteractiveCloth>();
	}
	
	void FixedUpdate()
	{
		float x = Random.Range(MinWind.x, MaxWind.x);
		float y = Random.Range(MinWind.y, MaxWind.y);
		float z = Random.Range(MinWind.z, MaxWind.z);
		cloth.externalAcceleration = transform.TransformDirection(x, y, z);
	}
}
