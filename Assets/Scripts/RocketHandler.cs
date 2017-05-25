using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaroqueUI;

public class RocketHandler : MonoBehaviour {
    float deltaVee = 0.0001f;
    GameObject rocket, jet;

    // Use this for initialization
    void Start () {
        rocket = GameObject.Find("Rocket");
        jet = GameObject.Find("Rocket/Jet");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var r = BaroqueUI.BaroqueUI.GetControllers()[0];
        var em = jet.GetComponent<ParticleSystem>().emission;
        var g = rocket.GetComponent<Gravity>();
        if (r.triggerPressed)
        {
            if (g.stationaryParent != null)
            {
                g.stationaryParent = null;
                // add escape velocity
                g.speed = r.transform.forward * 3 * deltaVee;
            }
            em.enabled = true;
            g.speed += r.transform.forward * deltaVee;
        }
        else
        {
            em.enabled = false;
        }
        if (g.stationaryParent == null)
            rocket.transform.rotation = r.transform.rotation;
    }
}
