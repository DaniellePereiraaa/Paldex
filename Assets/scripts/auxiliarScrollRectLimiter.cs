using UnityEngine;

public class auxiliarScrollRectLimiter : MonoBehaviour
{
    private Transform objTransform;
    public ButtonLoader buttonLoader;
    public  int QuantY;
    float distancia = 0;

    void Start()
    {
        QuantY = buttonLoader.CountNameOccurrences();
        objTransform = transform; // Atribui o componente Transform do objeto atual
        calculaLimite();
    }

    void Update()
    {
        // Obtém a posição Y do objeto
        float yPosition = transform.position.y;

        
        
        // Limita a posição Y conforme necessário
        LimitarMinPosicaoY();
        LimitarMaxPosicaoY();
    }

    void calculaLimite(){
        QuantY=((QuantY)/2) + 1;
        Debug.Log("quantidade de Y no scroll: " + QuantY);
        distancia = ((180*QuantY)-70);

    }
    void LimitarMinPosicaoY()
    {
        // Verifica se a posição Y ultrapassou o limite
        if (objTransform.position.y < 794)
        {
            Vector3 newPosition = objTransform.position;
            newPosition.y = 794;
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
