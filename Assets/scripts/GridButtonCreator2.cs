using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class GridButtonCreator2 : MonoBehaviour
{
    public ButtonLoader buttonLoader;
    public GameObject buttonPrefab;
    public int QuantX = 5;
    public int QuantY = 5;
    public Vector2 buttonSize = new Vector2(100, 100);
    public Vector2 buttonSpacing = new Vector2(10, 10);

    public string imagesFolderPath = "images";

    private Button[,] buttonsGrid;
    private Dictionary<int, string> idToNameMap = new Dictionary<int, string>();

    void Start()
    {
        QuantY = buttonLoader.CountNameOccurrences();
        LoadIDToNameMap();
        CreateGrid(QuantY);
    }

    void LoadIDToNameMap()
    {
        // Caminho para o arquivo JSON (substitua pelo caminho real do seu arquivo)
        string filePath = Application.dataPath + "/palworld_index.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            // Desserializa o JSON em uma lista de objetos
            List<PalWorldIndexData> dataList = JsonUtility.FromJson<PalWorldIndexDataList>("{\"list\":" + jsonData + "}").list;

            // Itera sobre cada objeto e adiciona ao mapa ID-nome
            foreach (PalWorldIndexData data in dataList)
            {
                idToNameMap[data.id] = data.name;
            }
        }
        else
        {
            Debug.LogError("Arquivo JSON não encontrado: " + filePath);
        }
    }

    public void CreateGrid(int occurrences)
    {
        QuantY = ((occurrences)/2) + 1;
        Debug.Log("quantidade de Y: " + QuantY);
        buttonsGrid = new Button[QuantX, QuantY];

        for (int j = 0; j < QuantY; j++)
        {
            for (int i = 0; i < QuantX; i++)
            {
                Vector3 position = new Vector3(i * (buttonSize.x + buttonSpacing.x), -j * (buttonSize.y + buttonSpacing.y), 0);
                GameObject buttonObject = Instantiate(buttonPrefab, transform);
                buttonObject.transform.localPosition = position;
                buttonObject.transform.localScale = Vector3.one;

                Button buttonComponent = buttonObject.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    int buttonID = j * QuantX + i + 1; // Calcula o ID do botão
                    buttonComponent.onClick.AddListener(() => ButtonClicked(buttonID)); // Adiciona um listener de clique para o botão

                    buttonsGrid[i, j] = buttonComponent;

                    // Atribui o nome do botão com base no mapa de ID-nome
                    string name;
                    if (idToNameMap.TryGetValue(buttonID, out name))
                    {
                        buttonObject.name = name;
                    }
                }
            }
        }
        //LoadButtonImages();
    }

    string LoadNameFromJSON(int buttonID)
    {
        // Caminho relativo ao arquivo JSON dentro da pasta "scripts"
        string filePath = Path.Combine(Application.dataPath, "scripts", "combinacao.json");

        if (!File.Exists(filePath))
        {
            Debug.LogError("Arquivo JSON não encontrado: " + filePath);
            return "Nome do Botão";
        }

        string jsonData = File.ReadAllText(filePath);
        ComboData comboData = JsonUtility.FromJson<ComboData>(jsonData);

        // Retorna o nome do botão com base no ID
        return comboData.combinacoes.FirstOrDefault(combo => combo.posicao_filho.Contains(buttonID.ToString()))?.filho ?? "Nome do Botão";
    }

    void ButtonClicked(int buttonID)
    {
        Debug.Log("Button " + buttonID + " clicked!");
    }
}