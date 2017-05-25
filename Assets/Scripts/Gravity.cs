using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Vector3 speed, rotationSpeed;
    public float mass;
    public GameObject flatOrbitAround = null; // if non-null, the speed is calculated for flat orbit and the value is disregarded
    public GameObject stationaryParent = null;
    
	// Use this for initialization
	void Start () {
        if (flatOrbitAround != null)
        {
            var distance = flatOrbitAround.transform.position - transform.position;
            Vector3 direction = Vector3.Cross(distance, Vector3.up);
            direction.Normalize();
            speed = direction * Mathf.Sqrt(GravitySupervisor.gravConst * flatOrbitAround.GetComponent<Gravity>().mass / distance.magnitude);
        }
    }
	
	// Update is called once per frame
	public void UpdatePosition () {
        if (stationaryParent != null)
        {
            var parent = stationaryParent.GetComponent<Gravity>();
            var distance = stationaryParent.transform.position - transform.position;
            var d = Quaternion.Euler(parent.rotationSpeed) * distance - distance;
            transform.position = transform.position + parent.speed - d;
            transform.rotation *= Quaternion.Euler(parent.rotationSpeed);
        } else
        {
            transform.rotation *= Quaternion.Euler(rotationSpeed);
            transform.position = transform.position + speed;
        }
	}
}
