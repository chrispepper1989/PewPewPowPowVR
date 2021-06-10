using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    ParticleSystem gunParticles;
    public float Health = 10;
    public Renderer mesh;
    void Awake()
    {
        //gunParticles = GetComponent<ParticleSystem>();
        if(mesh == null)
        mesh = GetComponent<Renderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject Explosion;
    public void TakeDamage(float damage, Vector3 pointOfContact, Color hueColor1, Color hueColor2)
    {
        Health -= damage;
        if (Health <= 0)
        {
            var explosion = Instantiate(Explosion, transform.position,new Quaternion());
            var particleSystem = explosion.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule settings = particleSystem.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(hueColor1,hueColor2);
            particleSystem.Stop();
            particleSystem.Play();
            // settings.
            //gunParticles.colorOverLifetime.color.color = hueColor1;
          /*  if(mesh != null)
            mesh.enabled = false;*/

            Destroy(gameObject);
            Destroy(explosion, 5.0f);
        }
    }

    public void Kill()
    {
        
    }
}
