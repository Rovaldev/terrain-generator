using System.Diagnostics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //Atributos
    public GameObject cubePrefab;
    public DynamicGridController gridController;
    public RoomGenerator roomGenerator;
    public int maxHeight;
    public float scale = 10f;
    public int seed=200;

    //Metodo para comenzar la generacion del terreno
    public void StartTerrainGeneration()
    {
        //Variabe para contar el tiempo de ejecucion
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //Obtiene el tamaño de la malla
        int width = gridController.Grid.Width;
        int height = gridController.Grid.Height;
        int length = gridController.Grid.Length;

        //Recorre todas las posiciones para calcular su valor en perlin noise
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                //Genera una coordenada para esa posicion
                float noise = Mathf.PerlinNoise((float)x / width * scale + seed, (float)z / length * scale + seed);

                //En base a la densidad del grid calcula la posicion correspondiente en y
                int yGridPosition = (int)(noise * maxHeight);

                /*GENERAR LA MALLA MANUALMENTE PARA CADA CHUNK*/
                //Crea los bloques de las capas superiores
                GameObject cellCube = Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(x, yGridPosition, z), Quaternion.identity);
                gridController.Grid.SetDataFromGridCoordinates(x, yGridPosition, z, cellCube);

                //Crea los bloques de las capas inferiores
                for (int k = 0; k < yGridPosition; k++)
                {
                    cellCube = Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(x, k, z), Quaternion.identity);
                    gridController.Grid.SetDataFromGridCoordinates(x, k, z, cellCube);
                }
            }
        }

        //Deja de contar el tiempo y lo imprime
        stopwatch.Stop();
        UnityEngine.Debug.Log("Tiempo de ejecución: " + stopwatch.ElapsedMilliseconds);
    }
}
