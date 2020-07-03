using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Atributos
    public GameObject mainMenuPanel;
    public GameObject creationMenuPanel;
    public GameObject playMenuPanel;
    public GameObject pauseMenuPanel;

    public InputField worldWidth;
    public InputField worldHeight;
    public InputField worldLength;
    public InputField worldMaxHeight;
    public InputField worldSeed;
    public InputField worldScale;

    public InputField roomMinWidth;
    public InputField roomCeilingHeight;
    public InputField roomMinLength;
    public InputField roomGenerationLayer;
    public InputField roomHallWidth;
    public InputField roomIterations;

    public GameObject playerPrefab;

    private bool canPause = false;
    private bool paused = false;

    private void Update()
    {
        //Detecta si se esta presionando el input para pausar
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                {
                    //Desactiva el menu y permite la rotacion
                    pauseMenuPanel.SetActive(false);
                    FindObjectOfType<CameraRotation>().canRotate = true;

                    //Desactiva el mouse
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    //Activa el menu y desactiva la rotacion
                    pauseMenuPanel.SetActive(true);
                    FindObjectOfType<CameraRotation>().canRotate = false;

                    //Activa el mouse
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                //Cambia el estado
                paused = !paused;
            }
        }

        //Evita que los valores excedan cierto limite
        worldWidth.text = Clamp(worldWidth, 1, 99);
        worldHeight.text = Clamp(worldHeight, 1, 99);
        worldLength.text = Clamp(worldLength, 1, 99);
        worldMaxHeight.text = Clamp(worldMaxHeight, 1, Parse(worldHeight));
        worldSeed.text = Clamp(worldSeed, -99999, 99999);
        worldScale.text = Clamp(worldScale, 1, 99999);
        roomMinWidth.text = Clamp(roomMinWidth, 1, Parse(worldWidth));
        roomCeilingHeight.text = Clamp(roomCeilingHeight, 1, Parse(worldHeight));
        roomMinLength.text = Clamp(roomMinLength, 1, Parse(worldLength));
        roomGenerationLayer.text = Clamp(roomGenerationLayer, 1, Parse(worldHeight));
        roomHallWidth.text = Clamp(roomHallWidth, 1, Parse(roomMinWidth));
        roomHallWidth.text = Clamp(roomHallWidth, 1, Parse(roomMinLength));
        roomIterations.text = Clamp(roomIterations, 1, 99999);
    }

    //Metodo para convertir un input a numero
    private int Parse(InputField input)
    {
        int value = 0;
        int.TryParse(input.text, out value);
        return value;
    }

    //Metodo para mantener un valor en un rango en formato string
    private string Clamp(InputField input, int from, int to)
    {
        int value = Parse(input);
        value = Mathf.Clamp(value, from, to);
        return value.ToString();
    }

    //Metodo para ir al menu de creacion
    public void GoToCreationMenu()
    {
        mainMenuPanel.SetActive(false);
        creationMenuPanel.SetActive(true);
    }

    //Metodo para ir al menu principal
    public void GoToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        creationMenuPanel.SetActive(false);
    }

    //Salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }

    //Metodo para generar el terreno y estructuras
    public void GenerateTerrain()
    {
        creationMenuPanel.SetActive(false);
        playMenuPanel.SetActive(true);

        //Asigna todos los datos a los controladorse correspondientes
        DynamicGridController grid = FindObjectOfType<DynamicGridController>();
        TerrainGenerator terrain = FindObjectOfType<TerrainGenerator>();
        RoomGenerator room = FindObjectOfType<RoomGenerator>();

        grid.width = Parse(worldWidth);
        grid.height = Parse(worldHeight);
        grid.length = Parse(worldLength);
        terrain.maxHeight = Parse(worldMaxHeight);
        terrain.seed = Parse(worldSeed);
        terrain.scale = Parse(worldScale);
        room.minWidth = Parse(roomMinWidth);
        room.roomHeight = Parse(roomCeilingHeight);
        room.minLength = Parse(roomMinLength);
        room.roomLayer = Parse(roomGenerationLayer);
        room.hallWidth = Parse(roomHallWidth);
        room.maxIterations = Parse(roomIterations);

        //Destruye la malla anterior y la regenera
        FindObjectOfType<DynamicGridController>().CreateGrid();

        //Crea el terreno
        FindObjectOfType<TerrainGenerator>().StartTerrainGeneration();

        //Crea las estructuras
        FindObjectOfType<RoomGenerator>().StartRoomGeneration();

        //Posiciona la camara principal donde se pueda observar la generacion
        Vector3 position = new Vector3(grid.transform.position.x + grid.width / 2 * grid.unitSize + grid.unitSize/2f, grid.transform.position.y + terrain.maxHeight * grid.unitSize * 2, grid.transform.position.z - 10);
        Camera.main.transform.position = position;
        Camera.main.transform.LookAt(grid.Grid.GetCellWorldPosition(grid.width / 2, 0, grid.length / 2));
    }

    //Metodo para regresar a la configuracion del terreno
    public void GoBackToCreationMenu()
    {
        //Destruye la geometria
        FindObjectOfType<DynamicGridController>().DestroyGrid();

        //Activa los paneles
        creationMenuPanel.SetActive(true);
        playMenuPanel.SetActive(false);
    }

    //Metodo para explorar la generacion
    public void ExploreMap()
    {
        //Obtiene el grid
        DynamicGridController grid = FindObjectOfType<DynamicGridController>();

        //Obtiene la posicion donde debe de ser creado el jugador
        Vector3 position = grid.Grid.GetCellWorldPosition(grid.width / 2, grid.height - 1, grid.length / 2);
        Instantiate(playerPrefab, position, Quaternion.identity);

        //Esconde el menu
        playMenuPanel.SetActive(false);

        //Indica que puede pausar
        canPause = true;
    }

    //Metodo para salir de la exploracion
    public void GoBackToPlayMenu()
    {
        //Destruye al jugador
        Destroy(FindObjectOfType<Player>().gameObject);

        //Activa el menu de juego
        paused = false;
        canPause = false;
        pauseMenuPanel.SetActive(false);
        playMenuPanel.SetActive(true);
    }

    //Metodo para salir completamente de la generacion
    public void ExitToMainMenu()
    {
        //Destruye la generacion
        FindObjectOfType<DynamicGridController>().DestroyGrid();

        //Destruye al jugador
        Destroy(FindObjectOfType<Player>().gameObject);

        //Desactiva el menu de pausa y activa el menu principal
        paused = false;
        canPause = false;
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
