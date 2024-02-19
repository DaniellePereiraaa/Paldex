using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;


public class ButtonLoader : MonoBehaviour
{
    public Button buttonInScene;
    private int buttonID;
    // GridButtonCreator2 getIDFromName;
    private Dictionary<string, int> nameToIDMap;
    
    string namePal;
    int QuantX = 2;
    Button[,] buttonsGrid; // Matriz de botões

    
    void Start()
    {
        // Debug.Log("Conteúdo do dicionário idToNameMap em GridButtonCreator2:");
        // foreach (var kvp in nameToIDMap)
        // {
        //     Debug.Log($"ID: {kvp.Key}, Nome: {kvp.Value}");
        // }
        // Verifica se o PlayerPrefs contém o ID do botão
        if (PlayerPrefs.HasKey("idbutton"))
        {
            buttonID = PlayerPrefs.GetInt("idbutton");

            // Carrega o nome do botão com base no ID do arquivo JSON
            namePal = LoadNameFromJSON(buttonID);

            // Salva o nome do botão nos dados do jogador
            PlayerPrefs.SetString("ButtonName", namePal);

            // Carrega a imagem do botão com base no ID
            Sprite image = LoadImageByID(buttonID);
            // Exibe a imagem no botão
            Image buttonImage = buttonInScene.GetComponent<Image>();

            if (buttonImage != null && image != null)
            {
                buttonImage.sprite = image;
            }
            else
            {
                Debug.LogWarning("O componente de imagem do botão ou a imagem não foram encontrados.");
            }

            // Imprime os dados salvos no console
            Debug.Log("Nome do botão salvo: " + namePal);
            Debug.Log("Imagem do botão salva nos dados do jogador.");
            
            // List<string> parentNames = GetParentNames(buttonID);
            // foreach (string parentName in parentNames)
            // {    
            //         Debug.Log("IName PARENTS: " + parentName);

            //     string[] individualNames = parentName.Split(new string[] { " + " }, StringSplitOptions.None);
            //     foreach (string name in individualNames)
            //     { 
                   
            //         Debug.Log("Individual Name: " + name);
            //         int idParent = GetIDFromName(name);
                    
            //         Debug.Log("PARENT NAME: " + name + "PARENT ID:" + idParent);
            //     }
            // }

            // AssignIDsToButtons(parentNames); // Atribui IDs aos botões na grade

            // Conta as ocorrências do nome na posição de filho
            int quantPais = CountNameOccurrences();
        }
        else
        {
            Debug.LogWarning("PlayerPrefs não contém o ID do botão.");
        }
    }
    public int getButtonId()
    {
        return buttonID;
    }


    // Função para obter a matriz de botões
    public Button[,] GetButtonsGrid()
    {
        return buttonsGrid;
    }


    //  public int GetIDFromName(string parentName)
    //     {
    //         foreach (var kvp in nameToIDMap)
    //         {
    //             if (kvp.Key == parentName)
    //             {
    //                 return kvp.Value;
    //             }
    //         }
    //         return -1; // Retorna -1 se o nome não for encontrado
    //     }

   public void AssignIDsToButtons(List<string> parentNames)
    {
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

    // Função para carregar o nome do botão com base no ID do arquivo JSON
    string LoadNameFromJSON(int buttonID)
    {
        string filePath = Application.dataPath + "/palworld_index.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            PalWorldIndexDataList dataList = JsonUtility.FromJson<PalWorldIndexDataList>("{\"list\":" + jsonData + "}");

            foreach (PalWorldIndexData data in dataList.list)
            {
                if (data.id == buttonID)
                {
                    return data.name;
                }
            }

            Debug.LogWarning("Nome não encontrado para o ID do botão: " + buttonID);
        }
        else
        {
            Debug.LogError("Arquivo JSON não encontrado: " + filePath);
        }

        return "Nome do Botão";
    }

    // Função para carregar a imagem do botão com base no ID
    Sprite LoadImageByID(int buttonID)
    {
        string imagePath = Application.dataPath + "/images/" + buttonID + ".png";

        if (File.Exists(imagePath))
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.LogError("Imagem não encontrada para o ID do botão: " + buttonID);
            return null;
        }
    }

    // Método para contar quantas vezes o nome referente a um ID aparece na posição de filho
    public int CountNameOccurrences()
    {
        string namePal = LoadNameFromJSON(buttonID);
        string filePath = Application.dataPath + "/combinacao.json";
        Debug.Log("Olha on nome: " + namePal);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            // Analisa o JSON para contar as ocorrências do nome na posição de filho
            int occurrences = 0;
            string searchString = "\"filho\": \"" + namePal + "\"";
            occurrences = (jsonData.Length - jsonData.Replace(searchString, "").Length) / searchString.Length;

            Debug.Log("Ocorrências do nome para o ID " + buttonID + ": " + occurrences);
            return occurrences;
        }
        else
        {
            Debug.LogError("Arquivo combinacoes.json não encontrado em: " + filePath);
            return 0;
        }
    }
   
    public List<string> GetParentNames(int buttonID)
    {
        namePal = LoadNameFromJSON(buttonID);
        string filePath = Application.dataPath + "/combinacao.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ComboData comboData = JsonUtility.FromJson<ComboData>("{\"combinacoes\":" + jsonData + "}");

            HashSet<string> uniqueParentNames = new HashSet<string>();

            foreach (Combo combo in comboData.combinacoes)
            {
                if (combo.filho == namePal)
                {
                    for (int i = 0; i < combo.posicao_filho.Length - 1; i += 2)
                    {
                        string parentNames = combo.posicao_filho[i] + " + " + combo.posicao_filho[i + 1];
                        if (!uniqueParentNames.Contains(parentNames))
                        {
                            uniqueParentNames.Add(parentNames);
                        }
                    }
                }
            }

            return RemoveDuplicatePairs(uniqueParentNames.ToList());
        }
        else
        {
            Debug.LogError("Arquivo combinacoes.json não encontrado em: " + filePath);
            return new List<string>();
        }
    }

    List<string> RemoveDuplicatePairs(List<string> parentNames)
    {
        HashSet<string> uniquePairs = new HashSet<string>();

        List<string> result = new List<string>();

        foreach (string pair in parentNames)
        {
            string[] names = pair.Split(new string[] { " + " }, System.StringSplitOptions.None);
            if (uniquePairs.Contains(pair) || uniquePairs.Contains(names[1] + " + " + names[0]))
            {
                // Caso o par já exista na lista, ignore
                continue;
            }

            uniquePairs.Add(pair);
            result.Add(pair);
        }

        return result;
    }

    void ButtonClicked(int buttonID)
    {
        Debug.Log("Button " + buttonID + " clicked!");
    }
}
