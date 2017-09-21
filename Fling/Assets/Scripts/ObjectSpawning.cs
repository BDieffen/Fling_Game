using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour {

    public GameObject obj_Box;
    //GameObject[] obstacles = new GameObject[1];

    public GameObject power_EnlargePaddle;
    public GameObject power_ExtraLife;
    GameObject[] powers = new GameObject[2];

    public GameObject spawnZone;


	// Use this for initialization
	void Start () {
        //SpawnObj(obj_Box);
        //obstacles[0] = obj_Box;
        powers[0] = power_ExtraLife;
        powers[1] = power_EnlargePaddle;
    }

    public void SpawnObj(GameObject objToSpawn)
    {
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = spawnZone.transform.TransformPoint(rndPosWithin * .5f);
        GameObject obj = Instantiate(objToSpawn, rndPosWithin, objToSpawn.transform.rotation);
        //If the object to be spawned is an obstacle, this puts it as a child to the "Obstacles" game object
        if (objToSpawn.gameObject.tag == "Obstacle")
        {
            obj.transform.SetParent(GameObject.Find("Obstacles").transform);
        }
        //If the object to be spawned is a power, this puts it as a child to the "PowerUp" game object
        else if (objToSpawn.gameObject.tag == "PowerUp")
        {
            obj.transform.SetParent(GameObject.Find("PowerUps").transform);
        }
    }
}
