using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Variáveis
    public FlockManager myManager;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Aleatoriza o valor da velocidade entre o minimo e o maximo recebido através do myManager
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // faz a movimentação no eixo Z
        transform.Translate(0,0,Time.deltaTime * speed);
    }
}
