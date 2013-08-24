using UnityEngine;
using System.Collections;

public class FaceUpwards : MonoBehaviour
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
		
		t.LookAt(t.position - new Vector3(0, 0, 1), t.position);
	}
	
}
