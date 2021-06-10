using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public float MovementSpeed = 2.0f;
    // Update is called once per frame
    void Update () {
	    var direction = transform.forward;
	    var movment = direction * Time.deltaTime * MovementSpeed;
	    transform.SetPositionAndRotation(transform.position + movment, transform.rotation);
    }
}
