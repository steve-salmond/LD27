using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour
{
	
	private Transform t;
	
	public void Start()
	{
		t = transform;
	}
	
	public void Update()
	{
		if (!t)
			t = transform;
		
		t.LookAt(Camera.main.transform, t.position);
	}
	
}
