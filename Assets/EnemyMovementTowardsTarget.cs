using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTowardsTarget : MonoBehaviour
{

    public GameObject Target;
	// Use this for initialization
	void Start () {
	    if (Target == null)
	    {
	        Target = GameObject.FindWithTag("Player");
	    }
	}

    public float RotationSpeed = 2.0f;

    public float MovementSpeed = 10.0f; 
	// Update is called once per frame
	void Update () {
        //calculate the rotation needed 
        var neededRotation = Quaternion.LookRotation(Target.transform.position - transform.position);


        //use spherical interpollation over time 
        var interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * RotationSpeed);

	    var direction = transform.forward;
	    var movment = direction * Time.deltaTime * MovementSpeed;
        transform.SetPositionAndRotation(transform.position + movment, interpolatedRotation);
	    
    }
}
