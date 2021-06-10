using UnityEngine;

namespace Assets {
    public class SpawnCargoShips : SpawnShips
    {

        public override void OnSpawn(GameObject obj)
        {
            if (obj == null)
            {
                return;
                
            }
            var spawner = obj.GetComponent<SpawnLittleShips>();
            spawner.SpawnNear = obj;
        }
        /* public PlayerHealth playerHealth;
    public GameObject MainPlanet;
    public GameObject CargoShipPrefab;
    public float spawnTime = 3f;

    
    public float size = 200;
    public float minDistance = 20;

    // Use this for initialization
    void Start () {
	    InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    Vector3 FindNewPos()
    {
        Collider[] neighbours;
        Vector3 newPos;
        do
        {
            // draw a new position
            newPos = new Vector3(Random.Range(-size, size), Random.Range(-size, size), Random.Range(-size, size));
            // get neighbours inside minDistance:
            neighbours = Physics.OverlapSphere(newPos, minDistance);
            // if there's any neighbour inside range, repeat the loop:
        } while (neighbours.Length > 0);
        return newPos; // otherwise return the new position
    }
    void Spawn()
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(CargoShipPrefab, FindNewPos(), new Quaternion());
    }

    // Update is called once per frame
    void Update () {
		
	}*/
    }
}
