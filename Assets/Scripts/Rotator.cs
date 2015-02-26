using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public Vector3 rotation;
	public float speed;
	
	void Update () {
		this.transform.Rotate (rotation * speed);
	}
}
