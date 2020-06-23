using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //Atributos
    public GameObject cubePrefab;
    public CubicGridController gridController;
    public float scale = 10f;
    public int seed=200;

    private void Start()
    {

        //Obtiene el tamaño de la malla
        int width = gridController.Grid.Width;
        int length = gridController.Grid.Length;
        
        //Recorre todas las posiciones para calcular su valor en perlin noise
        for(int i=0; i<width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                //Genera una coordenada para esa posicion
                float noise = Mathf.PerlinNoise((float)i / width * scale + seed, (float)j/length * scale + seed);

                //En base a la densidad del grid calcula la posicion correspondiente en y
                int yGridPosition = (int)(noise * gridController.density);
                
                /*GENERAR LA MALLA MANUALMENTE PARA CADA CHUNK*/
                //Crea los bloques de las capas superiores
                Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(i, yGridPosition, j), Quaternion.identity);

                //Crea los bloques de las capas inferiores
                for(int k=0; k<yGridPosition; k++)
                {
                    Instantiate(cubePrefab, gridController.Grid.GetCellWorldPosition(i, k, j), Quaternion.identity);
                }
            }
        }
    }
}
