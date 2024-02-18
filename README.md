# Paldex

![Unity Version](https://img.shields.io/badge/Unity-20XX.XX-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

Este projeto Unity demonstra como criar uma interface de usuário (UI) interativa para explorar combinações de botões em uma grade. O projeto inclui a leitura de dados de um arquivo JSON, a representação desses dados em botões clicáveis e a exibição de combinações correspondentes em uma grade secundária.

## Funcionalidades Principais

- Leitura de dados a partir de um arquivo JSON.
- Exibição do botão clicado na parte superior da tela.
- Exibição dos botões correspondentes em uma grade na parte inferior da tela.
- Interface de usuário (UI) interativa e responsiva.

## Capturas de Tela
<img src="Assets/screenshots/pal1.PNG" width="200">              <img src="Assets/screenshots/pal2.PNG" width="200">            <img src="Assets/screenshots/pal3.PNG" width="200"> 

## Classes presentes no projeto
- **AlphanumericComparer**: Classe que implementa um comparador alfanumérico para ordenar strings alfanuméricas.  
- **AuxiliarScrollRectLimiter**: Classe que fornece funcionalidades auxiliares para limitar o deslocamento de um ScrollRect.  
- **ButtonClickListener**: Classe responsável por lidar com os eventos de clique em botões.  
- **ButtonData**: Classe de dados que armazena informações sobre um botão, como seu ID, nome e imagem.  
- **ButtonDisplay**: Classe que controla a exibição de um botão na interface do usuário.  
- **ButtonLoader**: Classe principal responsável por carregar e configurar botões dinamicamente na cena.  
- **ButtonReturn**: Classe que define o comportamento de retorno de um botão.  
- **ButtonSearchFilter**: Classe que implementa um filtro de pesquisa para botões.  
- **ClickedButtonUI**: Classe que define a interface do usuário de um botão clicado.  
- **Combo**: Classe que representa uma combinação de itens ou elementos.  
- **ComboData**: Classe de dados que armazena informações sobre combinações de elementos.  
- **GridButtonCreator**: Classe responsável por criar e organizar botões em uma grade na interface do usuário.  
- **GridButtonCreator2**: Classe alternativa ou versão modificada de GridButtonCreator.  
- **PalWorldIndexData**: Classe de dados que armazena informações sobre índices de elementos em um mundo fictício.  
- **PalWorldIndexDataList**: Classe que encapsula uma lista de dados de índice de elementos em um mundo fictício.  
- **ScrollRectLimiter**: Classe que limita o deslocamento de um ScrollRect em uma interface de usuário.  
- **SimpleJSON**: Biblioteca ou classe que oferece funcionalidades simples para manipulação de dados JSON.  
- **SimpleScrollView**: Classe que implementa uma visualização de rolagem simples.  
- **YourButtonScript**: Classe responsável por controlar o comportamento e a interação de um botão específico.  
- **YourButtonScript2**: Uma versão alternativa ou modificada de YourButtonScript.  

## Requisitos do Sistema

- Unity 2021.3.6/1 ou superior.
- Plataforma de desenvolvimento compatível (Windows, macOS, Linux).

## Instalação

1. Clone o repositório para o seu ambiente de desenvolvimento local:

```
git clone https://github.com/Luiz-Dalay/Paldex.git
```

2. Abra o projeto Unity através do Unity Hub.

3. Navegue até a cena principal `MainScene` localizada no diretório `Assets/Scenes`.

4. Execute o projeto Unity no editor ou crie um build para a plataforma desejada.

## Uso

1. Execute o projeto Unity no editor ou inicie o build criado.

2. Clique em um botão na grade principal para visualizar as combinações correspondentes na grade inferior.

3. Observe como o botão clicado é exibido na parte superior da tela.

4. Experimente diferentes combinações de botões para explorar as funcionalidades do projeto.

## Estrutura do Projeto

- **Assets**: Contém todos os assets do projeto, incluindo scripts, prefabs, cenas e outros recursos.
  - **Scripts**: Scripts C# utilizados para controlar a lógica do projeto.
  - **Prefabs**: Prefabs reutilizáveis para botões e outros elementos da interface de usuário (UI).
  - **Scenes**: Cenas do Unity que compõem o fluxo principal do projeto.
- **Documentation**: Documentação adicional, incluindo este arquivo README.md.

## Contribuição

Contribuições são bem-vindas! Se você tiver alguma sugestão, correção de bugs ou desejar implementar novas funcionalidades, sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Licença

Este projeto está licenciado sob a licença [MIT](https://opensource.org/licenses/MIT) - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
