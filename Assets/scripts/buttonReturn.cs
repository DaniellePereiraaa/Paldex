using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonReturn : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene("TelaInicial");   
    }
}
