using System.Diagnostics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //Atributos
    public GameObject cubePrefab;
    public DynamicGridController gridController;
    public RoomGenerator roomGenerator;
    public float scale = 10f;
    public int seed=200;

    private void Start()
    {

        //Variabe para contar el tiempo de ejecucion
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //Obtiene el tamaño de la malla
        int width = gridController.Grid.Width;
        int height = gridController.Grid.Height;
        int length = gridController.Grid.Length;
        
        //Recorre todas las posiciones para calcular su valor en perlin noise
        for(int x=0; x<width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                //Genera una coordenada para esa posicion
                float noise = Mathf.PerlinNoise((float)x / width * scale + seed, (float)z/length * scale + seed);

                //En base a la densidad del grid calcula la posicion correspondiente en y
                int yGridPosition = (int)(noise * gridController.height);
                
                /*GENERAR LA MALLA MANUALMENTE PARA CADA CHUNK*/
                //Crea los bloques de las capas superiores
                GameObject cellCube = Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(x, yGridPosition, z), Quaternion.identity);
                gridController.Grid.SetDataFromGridCoordinates(x, yGridPosition, z, cellCube);

                //Crea los bloques de las capas inferiores
                for(int k=0; k<yGridPosition; k++)
                {
                    cellCube = Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(x, k, z), Quaternion.identity);
                    gridController.Grid.SetDataFromGridCoordinates(x, k, z, cellCube);
                }
            }
        }

        //Genera los cuartos
        roomGenerator.StartRoomGeneration();

        //Deja de contar el tiempo y lo imprime
        stopwatch.Stop();
        UnityEngine.Debug.Log("Tiempo de ejecución: " + stopwatch.ElapsedMilliseconds);
    }

    /*private void Update()
    {
        //GenerateTerrainGeometry();
    }

    //Metodo para generar la geometria
    private void GenerateTerrainGeometry()
    {
        //Recorrer todas las posiciones de la malla

        
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < gridController.width; x++)
        {
            for (int y = 0; y < gridController.height; y++)
            {
                for (int z = 0; z < gridController.length; z++)
                {
                    //Si el bloque no esta vacio
                    if(gridController.Grid.GetDataFromGridCoordinates(x, y, z) == 100)
                    {
                        //cambia el color
                        Gizmos.color = Color.red;

                        //Si su cara superior no tiene ningun blopque adyacente
                        if (gridController.Grid.GetDataFromGridCoordinates(x, y + 1, z) != 100)
                        {
                            //Obtiene la posicion de cada esquina
                            Vector3 bottomLeftCorner = new Vector3(x, y+1, z);
                            Vector3 bottomRightCorner = new Vector3(x + gridController.unitSize, y+1, z);
                            Vector3 topLeftCorner = new Vector3(x, y+1, z + gridController.unitSize);
                            Vector3 topRightCorner = new Vector3(x + gridController.unitSize, y+1, z + gridController.unitSize);


                            Gizmos.DrawSphere(gridController.Grid.Origin + bottomLeftCorner * gridController.unitSize, 0.1f);
                            Gizmos.DrawSphere(gridController.Grid.Origin + bottomRightCorner * gridController.unitSize, 0.1f);
                            Gizmos.DrawSphere(gridController.Grid.Origin + topLeftCorner * gridController.unitSize, 0.1f);
                            Gizmos.DrawSphere(gridController.Grid.Origin + topRightCorner * gridController.unitSize, 0.1f);
                        }

                        //Si su cara +x no tiene ningun bloque adyacente
                        /*if (gridController.Grid.GetDataFromGridCoordinates(x + 1, y, z) != 100)
                        {
                            Gizmos.DrawSphere(gridController.Grid.Origin + new Vector3(x, y, z) * gridController.unitSize, 0.1f);
                            //Gizmos.DrawSphere(gridController.Grid.Origin + new Vector3(x + 1, y, z + gridController.unitSize) * gridController.unitSize, 0.1f);
                        }

                        //Si su cara -x no tiene ningun bloque adyacente
                        if (gridController.Grid.GetDataFromGridCoordinates(x - 1, y, z) != 100)
                        {
                            Gizmos.DrawSphere(gridController.Grid.Origin + new Vector3(x, y, z) * gridController.unitSize, 0.1f);
                            //Gizmos.DrawSphere(gridController.Grid.Origin + new Vector3(x + 1, y, z + gridController.unitSize) * gridController.unitSize, 0.1f);
                        }
                    }
                }
            }
        }
    }*/
}
