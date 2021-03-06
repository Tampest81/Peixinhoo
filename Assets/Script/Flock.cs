using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Vari?veis
    public FlockManager myManager;
    float speed;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        // Aleatoriza o valor da velocidade entre o minimo e o maximo recebido atrav?s do myManager
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Recebe os limites do objeto que est? com o script MyManager e aumenta o limite da area onde os peixes nadam
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        // Gera o hit do raycast
        RaycastHit hit = new RaycastHit();
        // Dire??o apontando para o pilar, para poder realizar o reflect
        Vector3 direction = myManager.transform.position - transform.position;
        // Se o peixe n?o entrar no bounds
        if (!b.Contains(transform.position))
        {
            turning = true;
            // A dire??o ser? apontada at? o objeto que est? com o myManager atribuido
            direction = myManager.transform.position - transform.position;
        }
        // Gera um raycast e aponta para frente, se detectar algo, ir? gerar o resultado e atribuir? na dire??o
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }
        // Se o turning est? ativo
        if (turning)
        {
            // Faz a rota??o at? a dire??o gerada anteriormente
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            myManager.rotationSpeed * Time.deltaTime);
        }
        // Caso contr?rio,
        else
        {
            // Gera um valor randomico, e se for menor que 10, ele aleatoriza a velocidade
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed,
                myManager.maxSpeed);
            // Gera um valor randomico, e se for menor que 20, ele chama o m?todo ApplyRules
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        // faz a movimenta??o no eixo Z
        transform.Translate(0, 0, Time.deltaTime * speed);
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
                    // Define o ponto central com a posi??o do peixe atual do foreach (go)
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
            // O ponto central ser? determinado a partir do ponto central dividido pelo tamanho do grupo
            vcentre = vcentre / groupSize;
            // A velocidade do peixe ser? resultado entre a velocidade do grupo dividido pelo tamanho do grupo
            speed = gSpeed / groupSize;
            // Gera a dire??o para a rota??o ser realizada
            Vector3 direction = (vcentre + vavoid) - transform.position;
            // Faz a rota??o
            if(direction!= Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(direction),
                                     myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
