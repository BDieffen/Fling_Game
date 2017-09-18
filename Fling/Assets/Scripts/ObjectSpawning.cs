using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour {

    public GameObject obj_Box;
    public GameObject power_EnlargePaddle;

    public GameObject spawnZone;


	// Use this for initialization
	void Start () {
        SpawnObj(obj_Box);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnObj(GameObject objToSpawn)
    {
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = spawnZone.transform.TransformPoint(rndPosWithin * .5f);
        GameObject obj = Instantiate(objToSpawn, rndPosWithin, objToSpawn.transform.rotation);
        if (objToSpawn.gameObject.tag == "Obstacle")
        {
            obj.transform.SetParent(GameObject.Find("Obstacles").transform);
        } else if(objToSpawn.gameObject.tag == "PowerUp")
        {
            obj.transform.SetParent(GameObject.Find("PowerUps").transform);
        }
    }
}
