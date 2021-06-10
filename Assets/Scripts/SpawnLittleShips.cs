using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets {
    public class SpawnLittleShips : SpawnShips
    {
        public GameObject Ship1, Ship2, Ship3, Ship4;
        private List<GameObject> LitteShipsToSpawn;
        
        public override void BeforeStart()
        {
           // var taggedObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "Enemy").Distinct().ToList();
           // taggedObjects.Remove(taggedObjects.Find(x => x.name == "CargoShip"));
            LitteShipsToSpawn = new List<GameObject>(){Ship1, Ship2, Ship3, Ship4};
            LitteShipsToSpawn.RemoveAll(x => x == null);
        }

        protected override GameObject SpawnObject(Vector3 pos, Quaternion rotation)
        {
            var number = LitteShipsToSpawn.Count;
            if (number == 0)
            {
                return null;
            }
            var index = Random.Range(0, number);
            var toSpawn = LitteShipsToSpawn[index];
            return Instantiate(toSpawn, pos, rotation);
        }
    }
}