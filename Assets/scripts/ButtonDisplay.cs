using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonDisplay : MonoBehaviour
{
    public Image buttonImage;
    public Text buttonText;

    void Start()
    {
        // Recuperando os dados do botão salvos usando PlayerPrefs
        string buttonName = PlayerPrefs.GetString("ButtonName");
        string imageName = PlayerPrefs.GetString("ButtonImage");
        Sprite buttonImageSprite = Resources.Load<Sprite>("images" + imageName); // Carregando a imagem com base no nome

        // Configurando o botão com os dados recuperados
        buttonText.text = buttonName;
        buttonImage.sprite = buttonImageSprite;
    }
}


