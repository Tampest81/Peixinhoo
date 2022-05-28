using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    // Variáveis
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
        // Instancia o array
        allFish = new GameObject[numFish];
        // For para a criação dos peixes
        for (int i = 0; i < numFish; i++)
        {
            // aleatoriza a posição de criação e armazena na variável pos, os seus limites é baseado no vector3 swinLimits
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));

            // Caso seja par, irá instanciar um peixe e se for impar, irá instanciar outro
            if (i%2 == 1)
            {
                // Instancia o peixe como um gameObject(cast) na posição aleatorizada
                allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            }
            else if(i%2 == 0)
            {
                // Instancia o peixe como um gameObject(cast) na posição aleatorizada
                allFish[i] = (GameObject)Instantiate(fishPrefab2, pos, Quaternion.identity);
            }
            // Faz a referencia do myManager nos peixes instanciados
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
