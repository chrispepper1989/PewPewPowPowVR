using System.Collections.Generic;
using System.Linq;
using CompleteProject;
using UnityEngine;

namespace Assets {
    public class SpawnShips : MonoBehaviour
    {
        private PlayerHealth playerHealth;
       // public GameObject ToSpawn;
        public GameObject ToSpawn;
        public GameObject SpawnNear;
        private GameObject spawnUnder;
        public float MinDistanceToSpawnNear = 300;
        public float MaxDistanceToSpawnNear = 1000;
        public float MaxYDistance = 10;
        public float MaxToSpawn = 4;
        public float SpawnTime = 2.0f;

        private Transform playerTarget;

        public virtual void BeforeStart()
        { }
        public virtual void OnSpawn(GameObject obj)
        { }

        private float? objectWidth = null;
        void Awake()
        {
            // Set up the references.
            playerTarget = GameObject.FindGameObjectWithTag("GunTarget").transform;
            spawnUnder = null;//GameObject.FindGameObjectWithTag("EnemyParent");

            // Set up the references.
            playerHealth = playerTarget.GetComponent<PlayerHealth>();

            
        }

        // Use this for initialization
        void Start()
        {
            BeforeStart();
            
            InvokeRepeating("Spawn", 1, SpawnTime);
        }

        Vector3 FindNewPos()
        {
            Collider[] neighbours;
            Vector3 newPos;

          ////  do
           // {
                // draw a new position (TODO favor player position)
                var xDirection = Random.Range(-0.4f, 0.4f);
                var zDirection = 1.0f;// - xDirection;
                
                var direction =  new Vector3(xDirection, 0, zDirection);

                var range = Random.Range(MinDistanceToSpawnNear, MaxDistanceToSpawnNear);
                if (SpawnNear == null)
                {
                    SpawnNear = this.gameObject;
                }
                
                var initialPosition = SpawnNear.transform.position;
                newPos = initialPosition + (direction * range);
                newPos.y += Random.Range(-MaxYDistance, MaxYDistance);
                float width = 10;
                if (objectWidth != null)
                {
                    width = objectWidth.Value;
                    // get neighbours inside minDistance:
      
                }
              //  neighbours = Physics.OverlapSphere(newPos, width * 2);
                // if there's any neighbour inside range, repeat the loop:
            //} while (neighbours.Length > 0);
            return newPos; // otherwise return the new position
        }

        protected virtual GameObject SpawnObject(Vector3 pos, Quaternion rotation)
        {
            return Instantiate(ToSpawn, pos, rotation);
        }
        void Spawn()
        {
            // If the player has no health left...
            if (playerHealth != null && playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject newObject = SpawnObject(FindNewPos(), new Quaternion());
            /*if (objectWidth == null)
            {
                objectWidth = newObject.GetComponent<BoxCollider>().bounds.size.x;
            }*/
          /*  if (spawnUnder == null)
            {
                newObject = Instantiate(ToSpawn, FindNewPos(), new Quaternion());
            }
            else
            {
                newObject = Instantiate(ToSpawn, FindNewPos(), new Quaternion(), spawnUnder.transform);
            }*/
            
            OnSpawn(newObject);
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}