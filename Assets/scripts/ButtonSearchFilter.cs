using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSearchFilter : MonoBehaviour
{
    public TMP_InputField searchInputField;
    public GridButtonCreator buttonCreator;

    private HashSet<Button> hiddenButtons = new HashSet<Button>();
    private List<Button> activeButtons = new List<Button>(); // Lista de botões ativos

    private void Start()
    {
        searchInputField.onValueChanged.AddListener(UpdateButtonVisibility);
        searchInputField.onEndEdit.AddListener(UpdateButtonVisibility);

        // Inicialmente, todos os botões são considerados ativos
        activeButtons.AddRange(buttonCreator.GetComponentsInChildren<Button>());
    }

    private void UpdateButtonVisibility(string searchText)
    {
        ActivateAllButtons();

        string searchTextLower = searchText.ToLower();
        //Debug.Log("UpdateButtonVisibility chamada com searchText: " + searchText);
        Button[] buttons = buttonCreator.GetComponentsInChildren<Button>();

        // Limpa a lista de botões ativos e ocultos
        activeButtons.Clear();
        hiddenButtons.Clear();
        
        // Itera sobre todos os botões para verificar se o texto de pesquisa está presente em seus nomes
        foreach (Button button in buttons)
        {
            bool isVisible = button.gameObject.name.ToLower().Contains(searchTextLower);

            //Debug.Log("Botão '" + button.gameObject.name + "' está visível? " + isVisible);

            // Atualiza a visibilidade do botão com base no resultado da verificação
            button.gameObject.SetActive(isVisible);

            if (isVisible)
            {
                // Adiciona o botão à lista de botões ativos
                activeButtons.Add(button);
            }
            else
            {
                // Adiciona o botão à lista de botões ocultos apenas se não estiver lá
                if (!activeButtons.Contains(button))
                {
                    //Debug.Log("Adicionando botão oculto: " + button.gameObject.name);
                    hiddenButtons.Add(button);
                }
            }
        }

        // Reorganiza os botões dentro da grade
        RearrangeButtons();
    }

    private void ActivateAllButtons()
{
    Button[] buttons = buttonCreator.GetComponentsInChildren<Button>(true); // Adicionando o parâmetro true para incluir objetos desativados

    foreach (Button button in buttons)
    {
        button.gameObject.SetActive(true);
    }
}

    private void RearrangeButtons()
    {
        // Itera sobre todos os botões ativos e os move para a posição correta na grade
        for (int i = 0; i < activeButtons.Count; i++)
        {
            int row = i / buttonCreator.QuantX; // Calcula a linha do botão na grade
            int column = i % buttonCreator.QuantX; // Calcula a coluna do botão na grade

            // Calcula a posição do botão na grade
            Vector3 newPosition = new Vector3(column * (buttonCreator.buttonSize.x + buttonCreator.buttonSpacing.x), -row * (buttonCreator.buttonSize.y + buttonCreator.buttonSpacing.y), 0);

            // Atualiza a posição do botão
            activeButtons[i].gameObject.transform.localPosition = newPosition;
        }
    }

}
