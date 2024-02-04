using UnityEngine;
using UnityEngine.UI;

public class YourButtonScript2 : MonoBehaviour
{
    public int buttonID1; // ID do primeiro botão associado
    public int buttonID2; // ID do segundo botão associado

    // Função para configurar os IDs dos botões associados
    public void AssignIDs(int id1, int id2)
    {
        buttonID1 = id1;
        buttonID2 = id2;
    }
}
