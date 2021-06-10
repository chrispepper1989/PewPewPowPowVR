using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ShootPositionOne;

    public GameObject ShootPositionTwo;
    public GameObject CameraPivot;
    public GameObject ShootDirecction;
    public float AnimationDuration = 2.0f;

    public float GunRange = 10000;

    public float GunDamage = 1;

    public float RocketDamage = 10;
    public float HueStep = 0.05f;
    private float _hue = 0.0f;
    public float TimeBetweenBullets = 0.01f;        // The time between each shot.
    public float EffectsDisplayTime = 10.0f;

    private float hue
    {
        get
        {
            var ret = _hue;
            _hue += HueStep;
            if (_hue > 1.0f) 
            {
                _hue -= 1.0f;
            }
            return ret;
        }
    }
 
    private LineRenderer gunLinePosition1;
    private LineRenderer gunLinePosition2;
    // Use this for initialization
    void Start ()
	{
	    gunLinePosition1 = ShootPositionOne.GetComponent<LineRenderer>();
	    gunLinePosition2 = ShootPositionTwo.GetComponent<LineRenderer>();

	    gunLinePosition1.material = new Material(Shader.Find("Particles/Additive"));
	    gunLinePosition2.material = new Material(Shader.Find("Particles/Additive"));

    }
    private float timer;
    private bool firstGun = true;
    private float speed = 100;
	// Update is called once per frame
	void Update () {
	    timer += Time.deltaTime;

        if (Input.GetMouseButton(0))
	    {
	        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * 2, 0) * Time.deltaTime * speed);
        }

	    const float applyFactor = 0.1f;
	    var rotation = Quaternion.FromToRotation(transform.forward, CameraPivot.transform.forward);
        if (rotation.eulerAngles.y > 45 && rotation.eulerAngles.y < 180)
	    {   
	        float diff = rotation.eulerAngles.y - 45;
	        float apply = diff * applyFactor;
            CameraPivot.transform.Rotate(new Vector3(0,1,0), -apply);
	       // transform.Rotate(new Vector3(0, 1, 0), apply);
	       
        }

	    if (rotation.eulerAngles.y > 180 && rotation.eulerAngles.y < 315)
	    {
	        float diff = 315 - rotation.eulerAngles.y;
	        float apply = diff * applyFactor;

            CameraPivot.transform.Rotate(new Vector3(0, 1, 0), apply);
	       // transform.Rotate(new Vector3(0, 1, 0), -apply);
	    }

	    if (rotation.eulerAngles.x > 30 && rotation.eulerAngles.x < 180)
	    {
	        float diff = rotation.eulerAngles.x - 30;
	        float apply = diff * applyFactor;
	        CameraPivot.transform.Rotate(new Vector3(1, 0, 0), -apply);
	        // transform.Rotate(new Vector3(0, 1, 0), apply);

	    }

	    if (rotation.eulerAngles.x > 180 && rotation.eulerAngles.x < 330)
	    {
	        float diff = 330 - rotation.eulerAngles.x;
	        float apply = diff * applyFactor;

	        CameraPivot.transform.Rotate(new Vector3(1, 0, 0), apply);
	        // transform.Rotate(new Vector3(0, 1, 0), -apply);
	    }
        if (( Input.GetKeyDown(KeyCode.Space)) && timer >= TimeBetweenBullets && Time.timeScale != 0)
	    {
	        Shoot();
	    }
	    
        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= EffectsDisplayTime)
	    {
	        // ... disable the effects.
	        DisableEffects();
	    }
    }

    private void Shoot()
    {
        timer = 0;
        var gun = firstGun ? ShootPositionOne : ShootPositionTwo;
        var lineRenderer = firstGun ? gunLinePosition1 : gunLinePosition2;
        var position = gun.transform.position;
        //var direction = ShootDirecction.transform.position;
        var direction = (ShootDirecction.transform.position - position).normalized;
        firstGun = !firstGun; // switch guns for next cycle;
        RaycastHit shootHit;
        Color startcolor = Color.HSVToRGB(hue, 1.0f, 1.0f);
        Color endcolor = Color.HSVToRGB(hue, 0.5f, 0.5f);
        Vector3 shootTo;
        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(position, direction, out shootHit, GunRange))
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(GunDamage, shootHit.point, startcolor, endcolor);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            shootTo = shootHit.point;
    
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {

            shootTo = position + direction * GunRange;
        }
        
  
        var gunLine = lineRenderer;
        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
       // gunLine.startWidth = 1.5f;
       // gunLine.endWidth = 1.5f;
        gunLine.SetPosition(0, position);
        // Set the second position of the line renderer to the point the raycast hit.
        gunLine.SetPosition(1, shootTo);
        gunLine.startColor = startcolor;
        gunLine.endColor = endcolor;
        
        Debug.DrawLine(position, shootTo, startcolor, AnimationDuration);
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLinePosition1.enabled = false;
        gunLinePosition2.enabled = false;
      
    }
}
