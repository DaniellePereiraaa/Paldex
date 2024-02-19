using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;


public class GridButtonCreator : MonoBehaviour
{
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
        LoadIDToNameMap();
        CreateGrid();
    }

    // Carrega o mapeamento de IDs para nomes a partir do arquivo JSON
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

    // Cria a grade de botões
    public void CreateGrid()
    {
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
        LoadButtonImages();
    }

    // Método para obter a matriz de botões
    public Button[,] GetButtonsGrid()
    {
        return buttonsGrid;
    }

    // Carrega e atribui imagens aos botões
    void LoadButtonImages()
    {
        string imagesPath = Path.Combine(Application.dataPath, imagesFolderPath);
        if (!Directory.Exists(imagesPath))
        {
            Debug.LogError("Pasta de imagens não encontrada: " + imagesPath);
            return;
        }

        // Lista todos os arquivos de imagem na pasta
        string[] imagePaths = Directory.GetFiles(imagesPath, "*.png");

        // Ordena os caminhos das imagens alfanumericamente
        System.Array.Sort(imagePaths, new AlphanumericComparer());

        // Itera sobre cada imagem
        for (int i = 0; i < imagePaths.Length; i++)
        {
            // Obtém o ID do botão correspondente à imagem
            int buttonID = i + 1;

            // Obtém o botão correspondente ao ID
            GameObject buttonObject = buttonsGrid[(buttonID - 1) % QuantX, (buttonID - 1) / QuantX].gameObject;

            // Obtém ou adiciona um componente Image ao botão
            Image imageComponent = buttonObject.GetComponent<Image>();
            if (imageComponent == null)
            {
                imageComponent = buttonObject.AddComponent<Image>();
            }

            // Carrega a textura da imagem
            Texture2D texture = LoadTextureFromFile(imagePaths[i]);

            // Cria um sprite com a textura da imagem
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Atribui o sprite ao componente Image do botão
            imageComponent.sprite = sprite;
        }
    }

    // Carrega uma textura de arquivo
    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Carrega a imagem dos bytes

        return texture;
    }

    // Função para lidar com o clique em um botão
    public void ButtonClicked(int buttonID)
    {
        string name;
        if (idToNameMap.TryGetValue(buttonID, out name))
        {
            Debug.Log("Button " + buttonID + " clicked! Name: " + name);
        }
        else
        {
            Debug.LogWarning("No name found for Button ID: " + buttonID);
        }

        int idbutton = buttonID;
        // Salvando os dados do botão usando PlayerPrefs
        PlayerPrefs.SetInt("idbutton", buttonID);

        SceneManager.LoadScene("pal_dados");

        
    }
}

[System.Serializable]
public class PalWorldIndexData
{
    public int id;
    public string name;
}

[System.Serializable]
public class PalWorldIndexDataList
{
    public List<PalWorldIndexData> list;
}

// Classe para comparar strings alfanumericamente
public class AlphanumericComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        // Implementação de comparação alfanumérica
        // Esta implementação compara os números em uma string como inteiros e os caracteres restantes como strings
        // Isso garante que "10" venha depois de "9" e antes de "11"
        // Você pode usar uma implementação diferente, dependendo dos seus requisitos

        // Converte as strings para arrays de caracteres
        char[] charsX = x.ToCharArray();
        char[] charsY = y.ToCharArray();

        int indexX = 0;
        int indexY = 0;

        while (indexX < charsX.Length && indexY < charsY.Length)
        {
            // Verifica se os caracteres atuais são dígitos numéricos
            bool isDigitX = char.IsDigit(charsX[indexX]);
            bool isDigitY = char.IsDigit(charsY[indexY]);

            if (isDigitX && isDigitY)
            {
                // Se ambos são dígitos numéricos, compara-os como números inteiros
                string numStringX = "";
                while (indexX < charsX.Length && char.IsDigit(charsX[indexX]))
                {
                    numStringX += charsX[indexX++];
                }
                string numStringY = "";
                while (indexY < charsY.Length && char.IsDigit(charsY[indexY]))
                {
                    numStringY += charsY[indexY++];
                }

                int numX = int.Parse(numStringX);
                int numY = int.Parse(numStringY);

                if (numX != numY)
                {
                    return numX.CompareTo(numY);
                }
            }
            else
            {
                // Se os caracteres atuais não são dígitos numéricos, compara-os como caracteres normais
                if (charsX[indexX] != charsY[indexY])
                {
                    return charsX[indexX].CompareTo(charsY[indexY]);
                }
                indexX++;
                indexY++;
            }
        }

        // Se uma string for um prefixo da outra, a string mais curta vem primeiro
        return charsX.Length - charsY.Length;
    }  
}
