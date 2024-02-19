using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

public class GridButtonCreator2 : MonoBehaviour
{
    public ButtonLoader buttonLoader;
    public GameObject buttonPrefab;
    public int QuantX = 5;
    public int QuantY = 5;
    public Vector2 buttonSize = new Vector2(100, 100);
    public Vector2 buttonSpacing = new Vector2(10, 10);
    public GridButtonCreator images;

    public string imagesFolderPath = "images";
    List<string> parentNames ;

    private Button[,] buttonsGrid;
    private Dictionary<int, string> idToNameMap = new Dictionary<int, string>();
    private Dictionary<string, int> parentsNameID = new Dictionary<string, int>();

    void Start()
    {
        QuantY = buttonLoader.CountNameOccurrences();
        LoadIDToNameMap();
        List<string> parentNames = buttonLoader.GetParentNames(buttonLoader.getButtonId());
        foreach (string parentName in parentNames)
            {   
                string[] individualNames = parentName.Split(new string[] { " + " }, StringSplitOptions.None);
                foreach (string name in individualNames)
                { 
                    foreach (var kvp in idToNameMap)
                    {
                        if (kvp.Value == name)
                        parentsNameID[kvp.Value] = kvp.Key;
                        Debug.Log($"ID: {kvp.Value}, Nome: {kvp.Key}");
                    }
                    // int idParent = buttonLoader.GetIDFromName(name);
                    
        //             // Debug.Log("PARENT NAME: " + name + "PARENT ID:" + idParent);
                }
            }

       // Exiba o conteúdo do dicionário idToNameMap no console
        // Debug.Log("Conteúdo do dicionário idToNameMap em GridButtonCreator2:");
        // foreach (var kvp in idToNameMap)
        // {
        //     Debug.Log($"ID: {kvp.Key}, Nome: {kvp.Value}");
        // }
        
        // Inicializa o mapa de ID para nome
            // AssignIDsToButtons(parentNames); 
        // Cria a grade de botões e atribui os pares de pais aos botões adjacentes
        CreateGrid(QuantY);
        //AssignIDsToButtons(parentNames);
        // LoadIDToNameMap();
    }

    public void LoadIDToNameMap(){
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

     public void CreateGrid(int occurrences){
        QuantY = ((occurrences) / 2) + 1;
        Debug.Log("quantidade de Y: " + QuantY);
        Debug.Log("quantidade de x: " + QuantX);

        buttonsGrid = new Button[QuantX, QuantY];
         foreach (var kvp in parentsNameID)
            {
                // if (kvp.Value == name)
                // parentsNameID[kvp.Value] = kvp.Key;
                Debug.Log($"ID: {kvp.Key}, Nome: {kvp.Value}");
            }

        List<string> parentNames = buttonLoader.GetParentNames(buttonLoader.getButtonId());

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
    void AssignIDsToButtons(List<string> parentNames){
        buttonsGrid = new Button[parentNames.Count / QuantX, QuantX]; // Inicializa a matriz de botões

        // Carregar dados do arquivo palworld_index.json
        string filePath = Application.dataPath + "/palworld_index.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PalWorldIndexDataList dataList = JsonUtility.FromJson<PalWorldIndexDataList>("{\"list\":" + jsonData + "}");

            // Mapear nomes para IDs
            Dictionary<string, int> nameToIDMap = new Dictionary<string, int>();
            foreach (PalWorldIndexData data in dataList.list)
            {
                nameToIDMap[data.name] = data.id;
            }

            // Iterar sobre a lista de nomes de pais
            for (int i = 0; i < parentNames.Count; i++)
            {
                string[] names = parentNames[i].Split(new string[] { " + " }, System.StringSplitOptions.None);
                int id1, id2;

                // Obter IDs correspondentes aos nomes
                if (nameToIDMap.TryGetValue(names[0], out id1) && nameToIDMap.TryGetValue(names[1], out id2))
                {
                    // Associar IDs aos botões na grid
                    buttonsGrid[i / QuantX, i % QuantX].GetComponent<YourButtonScript2>().AssignIDs(id1, id2);
                }
                else
                {
                    Debug.LogError("ID não encontrado para um dos nomes na lista de pares de pais: " + parentNames[i]);
                }
            }
        }
        else
        {
            Debug.LogError("Arquivo palworld_index.json não encontrado em: " + filePath);
        }
    }
    public void LoadButtonImages2(){
        // Certifique-se de que idToNameMap esteja preenchido antes de chamar esta função
        // LoadIDToNameMap();

        foreach (var kvp in parentsNameID)
        {
            int buttonID = kvp.Value;
            string parentName = kvp.Key;
            Debug.Log("BUTTOONN ID: " + buttonID + "PARENT NAMEE: "+ parentName);

            // Obtém o botão correspondente ao ID
            GameObject buttonObject = buttonsGrid[(buttonID - 1) % QuantX, (buttonID - 1) / QuantX].gameObject;

            // Obtém ou adiciona um componente Image ao botão
            Image imageComponent = buttonObject.GetComponent<Image>();
            if (imageComponent == null)
            {
                imageComponent = buttonObject.AddComponent<Image>();
            }

            // Carrega a textura da imagem com base no ID
            Texture2D texture = LoadTextureByID(buttonID);

            // Cria um sprite com a textura da imagem
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Atribui o sprite ao componente Image do botão
            imageComponent.sprite = sprite;
        }
    }
    Texture2D LoadTextureByID(int buttonID){
        string imagePath = Path.Combine(Application.dataPath, imagesFolderPath, $"{buttonID}.png");

        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            return texture;
        }
        else
        {
            Debug.LogError($"Imagem não encontrada para o ID do botão: {buttonID}");
            return null;
        }
    }
    void LoadButtonImages(){
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
    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Carrega a imagem dos bytes

        return texture;
    }

    void ButtonClicked(int buttonID)
    {
        Debug.Log("Button " + buttonID + " clicked!");
    }
}

[System.Serializable]
public class ComboData
{
    public Combo[] combinacoes;
}

[System.Serializable]
public class Combo
{
    public string[] posicao_filho;
    public string filho;
}