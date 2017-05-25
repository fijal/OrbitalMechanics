using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaroqueUI;

public class RocketHandler : MonoBehaviour {
    float deltaVee = 0.0001f;
    GameObject rocket, jet;
    Controller controller;

    // Use this for initialization
    void Start () {
        rocket = GameObject.Find("Rocket");
        jet = GameObject.Find("Rocket/Jet");

        controller = Baroque.GetControllers()[0];
        var gt = Controller.GlobalTracker(this);
        gt.onTriggerDown += (ctrl) => { controller = ctrl; };
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var em = jet.GetComponent<ParticleSystem>().emission;
        var g = rocket.GetComponent<Gravity>();
        if (controller.triggerPressed)
        {
            if (g.stationaryParent != null)
            {
                g.stationaryParent = null;
                // add escape velocity
                g.speed = controller.transform.forward * 3 * deltaVee;
            }
            em.enabled = true;
            g.speed += controller.transform.forward * deltaVee;
        }
        else
        {
            em.enabled = false;
        }
        if (g.stationaryParent == null)
            rocket.transform.rotation = controller.transform.rotation;
    }
}
