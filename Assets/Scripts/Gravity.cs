using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Vector3 speed, rotationSpeed;
    public float mass;
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	public void UpdatePosition () {
        transform.rotation *= Quaternion.Euler(rotationSpeed);
        transform.position = transform.position + speed;
	}
}
