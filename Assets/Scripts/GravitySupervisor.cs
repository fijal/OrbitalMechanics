using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySupervisor : MonoBehaviour {
    float gravConst = 0.0000003f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (GameObject one in GameObject.FindGameObjectsWithTag("gravity"))
            foreach (GameObject two in GameObject.FindGameObjectsWithTag("gravity")) {
                if (Object.ReferenceEquals(one, two))
                    continue;
                var distance = (one.transform.position - two.transform.position).magnitude;
                var mass = one.GetComponent<Gravity>().mass;
                var forceValue = gravConst * mass / distance / distance;
                var force = (one.transform.position - two.transform.position).normalized * forceValue;
                two.GetComponent<Gravity>().speed += force;
            }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("gravity"))
            go.GetComponent<Gravity>().UpdatePosition();
        
    }
}
