using UnityEngine;
using UnityEngine.UI;

public class ClickedButtonUI : MonoBehaviour
{
    public Text buttonText;
    public Image buttonImage;

    public void SetButton(string buttonName, Sprite buttonSprite)
    {
        buttonText.text = buttonName;
        buttonImage.sprite = buttonSprite;
    }
}
