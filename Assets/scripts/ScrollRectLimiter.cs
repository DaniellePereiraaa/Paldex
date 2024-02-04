using UnityEngine;

public class ScrollRectLimiter : MonoBehaviour
{
    private Transform objTransform;
    public  int QuantY;
    float distancia = 0;

    void Start()
    {
        objTransform = transform; // Atribui o componente Transform do objeto atual
    }

    void Update()
    {
        // Obtém a posição Y do objeto
        float yPosition = transform.position.y;

        calculaLimite();
        
        // Limita a posição Y conforme necessário
        LimitarMinPosicaoY();
        LimitarMaxPosicaoY();
    }

    void calculaLimite(){
        distancia = ((170*QuantY)-70);

    }
    void LimitarMinPosicaoY()
    {
        // Verifica se a posição Y ultrapassou o limite
        if (objTransform.position.y < 869)
        {
            Vector3 newPosition = objTransform.position;
            newPosition.y = 869;
            objTransform.position = newPosition;
        }
    }
    void LimitarMaxPosicaoY()
    {
        // Verifica se a posição Y ultrapassou o limite
        if (objTransform.position.y > distancia)
        {
            // Limita a posição Y para 3000
            Vector3 newPosition = objTransform.position;
            newPosition.y = distancia;
            objTransform.position = newPosition;
        }
    }
}
