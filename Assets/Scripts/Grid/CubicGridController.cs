using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicGridController : MonoBehaviour
{
    //Atributos
    public int fixedSize;
    public int density;
    public Transform test;

    private GridObject<int> grid;
    private float unitSize;

    private void Start()
    {
        //Origen de la malla
        DataObjects.Point origin = new DataObjects.Point(transform.position.x, transform.position.y, transform.position.z);

        //Inicializa la malla con los parametros dados
        grid = new GridObject<int>(density, density, density, UnitSizeDensityBased(), origin, -1);
    }

    private void Update()
    {
        //Actualiza el tamaño de la malla constantemente
        grid.Length = density;
        grid.Height = density;
        grid.Width = density;

        //Actualiza la unidad de tamaño de la malla constantemente
        grid.UnitSize = UnitSizeDensityBased();

        //Actualiza el origen constantemente
        grid.Origin = new DataObjects.Point(transform.position.x, transform.position.y, transform.position.z);
    }

    //Calcula la cantidad de cuadros en base a la densidad
    private float UnitSizeDensityBased()
    {
        //Calcula cuanto debe equivaler unitSize para que al cambiar la densidad mantenga el mismo tamaño
        return (float)fixedSize / density;
    }

    //Metodo para dibujar la malla
    private void OnDrawGizmos()
    {
        unitSize = UnitSizeDensityBased();

        //Dibuja el plano xy frontal simulando el grid resultante
        for(int x=0; x<density; x++)
        {
            float xScaled = x * unitSize;

            for (int y=0; y<density; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + xScaled, transform.position.y + yScaled, transform.position.z), new Vector3(transform.position.x + xScaled + unitSize, transform.position.y + yScaled, transform.position.z));
                Debug.DrawLine(new Vector3(transform.position.x + xScaled, transform.position.y + yScaled, transform.position.z), new Vector3(transform.position.x + xScaled, transform.position.y + yScaled + unitSize, transform.position.z));
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + unitSize * density, transform.position.z), new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z));
        Debug.DrawLine(new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z), new Vector3(transform.position.x + unitSize * density, transform.position.y, transform.position.z));

        //Dibuja el plano xy trasero simulando el grid resultante
        for (int x = 0; x < density; x++)
        {
            float xScaled = x * unitSize;

            for (int y = 0; y < density; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + xScaled, transform.position.y + yScaled, transform.position.z + unitSize * density), new Vector3(transform.position.x + xScaled + unitSize, transform.position.y + yScaled, transform.position.z + unitSize * density));
                Debug.DrawLine(new Vector3(transform.position.x + xScaled, transform.position.y + yScaled, transform.position.z + unitSize * density), new Vector3(transform.position.x + xScaled, transform.position.y + yScaled + unitSize, transform.position.z + unitSize * density));
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + unitSize * density, transform.position.z + unitSize * density), new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z + unitSize * density));
        Debug.DrawLine(new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z + unitSize * density), new Vector3(transform.position.x + unitSize * density, transform.position.y, transform.position.z + unitSize * density));

        //Dibuja el plano zy delantero simulando el grid resultante
        for (int x = 0; x < density; x++)
        {
            float zScaled = x * unitSize;

            for (int y = 0; y < density; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + yScaled, transform.position.z + zScaled), new Vector3(transform.position.x, transform.position.y + yScaled, transform.position.z + zScaled + unitSize));
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + yScaled, transform.position.z + zScaled), new Vector3(transform.position.x, transform.position.y + yScaled + unitSize, transform.position.z + zScaled));
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + unitSize * density, transform.position.z), new Vector3(transform.position.x, transform.position.y + unitSize * density, transform.position.z + unitSize * density));

        //Dibuja el plano zy trasero simulando el grid resultante
        for (int x = 0; x < density; x++)
        {
            float zScaled = x * unitSize;

            for (int y = 0; y < density; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + unitSize * density, transform.position.y + yScaled, transform.position.z + zScaled), new Vector3(transform.position.x + unitSize * density, transform.position.y + yScaled, transform.position.z + zScaled + unitSize));
                Debug.DrawLine(new Vector3(transform.position.x + unitSize * density, transform.position.y + yScaled, transform.position.z + zScaled), new Vector3(transform.position.x + unitSize * density, transform.position.y + yScaled + unitSize, transform.position.z + zScaled));
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z), new Vector3(transform.position.x + unitSize * density, transform.position.y + unitSize * density, transform.position.z + unitSize * density));
    }
}
