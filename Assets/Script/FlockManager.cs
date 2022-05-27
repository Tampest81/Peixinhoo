using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject fishPrefab2;

    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);

    public float minSpeed;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));

            if (i%2 == 1)
            {
                allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            }
            else if(i%2 == 0)
            {
                allFish[i] = (GameObject)Instantiate(fishPrefab2, pos, Quaternion.identity);
            }
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
