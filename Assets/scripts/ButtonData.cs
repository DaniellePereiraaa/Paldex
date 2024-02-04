using UnityEngine;

[System.Serializable]
public class ButtonData
{
    public string buttonName;
    public Sprite buttonImage; // Imagem do botão

    // Construtor para inicializar os dados do botão
    public ButtonData(string name, Sprite image)
    {
        buttonName = name;
        buttonImage = image;
    }
}
