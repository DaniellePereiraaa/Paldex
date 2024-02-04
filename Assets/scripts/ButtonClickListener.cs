using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonClickListener : MonoBehaviour
{
    public void OnButtonClick()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        // Supondo que você tenha uma referência para o botão clicado
        Button clickedButton = GetComponent<Button>();

        // Supondo que você tenha uma referência para o nome e a imagem do botão
        string buttonName = "Nome do Botão";
        Sprite buttonImage = Resources.Load<Sprite>("images"); // Substitua pelo caminho correto da imagem

        // Salvando os dados do botão usando PlayerPrefs
        PlayerPrefs.SetString("ButtonName", buttonName);
        PlayerPrefs.SetString("ButtonImage", buttonImage.name); // Salvando apenas o nome da imagem
        
        Debug.Log("Botão '" + buttonName);
    }
}
