using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    // Vari�veis
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);

    [Header("Configura��es do Cardume")]
    [Range(0f,5f)]
    public float minSpeed;
    [Range(0f, 5f)]
    public float maxSpeed;
    [Range(1f, 10f)]
    public float neighbourDistance;
    [Range(0f, 5f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Instancia o array
        allFish = new GameObject[numFish];
        // For para a cria��o dos peixes
        for (int i = 0; i < numFish; i++)
        {
            // aleatoriza a posi��o de cria��o e armazena na vari�vel pos, os seus limites � baseado no vector3 swinLimits
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            // Instancia o peixe
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            // Faz a referencia do myManager nos peixes instanciados
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
