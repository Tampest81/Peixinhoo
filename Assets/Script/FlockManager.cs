using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    // Variáveis
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;

    [Header("Configurações do Cardume")]
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
        // For para a criação dos peixes
        for (int i = 0; i < numFish; i++)
        {
            // aleatoriza a posição de criação e armazena na variável pos, os seus limites é baseado no vector3 swinLimits
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            // Instancia o peixe
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            // Faz a referencia do myManager nos peixes instanciados
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Atribui a posição do objeto que recebe esse script na variável goalPos (Vector3)
        goalPos = this.transform.position;
        // com o random.range, ele gera uma chance de 10% de entrar dentro do if
        if (Random.Range(0, 100) < 10)
            // faz uma pequena alteração na posição do ponto de destino, para fazer os peixes não rodarem no mesmo ponto
            goalPos = this.transform.position + new Vector3(
            Random.Range(-swinLimits.x,swinLimits.x),
            Random.Range(-swinLimits.y, swinLimits.y),
            Random.Range(-swinLimits.z, swinLimits.z));
    }
}
