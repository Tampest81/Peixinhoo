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
        ApplyRules();
        // faz a movimentação no eixo Z
        transform.Translate(0,0,Time.deltaTime * speed);
    }

    public void ApplyRules()
    {
        GameObject[] gos;
        // Popula o array gos com todos os peixes da cena, que foram puxados do myManager
        gos = myManager.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            // Se o gameobject analizado nao for este
            if(go != this.gameObject)
            {
                // Define a distancia entre este peixe e o outro
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                // Se a distancia for maior ou igual
                if(nDistance <= myManager.neighbourDistance)
                {
                    // Define o ponto central com a posição do peixe atual do foreach (go)
                    vcentre += go.transform.position;
                    // Incrementa +1 no contador groupSize
                    groupSize++;
                    // Se a distancia for menor que 1 ele afasta o peixe
                    if(nDistance < 1)
                    {
                        vavoid += (this.transform.position - go.transform.position);
                    }
                    // Recebe o componente do peixe atual do foreach
                    Flock anotherFlock = go.GetComponent<Flock>();
                    // Recebe a velocidade do peixe atual e aplica no gSpeed
                    gSpeed += anotherFlock.speed;

                }
            }
        }
        // Se houver um grupo
        if(groupSize > 0)
        {
            // O ponto central será determinado a partir do ponto central dividido pelo tamanho do grupo
            vcentre = vcentre / groupSize;
            // A velocidade do peixe será resultado entre a velocidade do grupo dividido pelo tamanho do grupo
            speed = gSpeed / groupSize;
            // Gera a direção para a rotação ser realizada
            Vector3 direction = (vcentre + vavoid) - transform.position;
            // Faz a rotação
            if(direction!= Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(direction),
                                     myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
