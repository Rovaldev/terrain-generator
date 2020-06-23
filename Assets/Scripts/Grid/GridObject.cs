using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject<T>
{
    //Atributos
    private int length;
    private int width;
    private int height;
    private float unitSize;

    private DataObjects.Point origin;
    private T[,,] grid;

    //Metodo para obtener/cambiar la longitud
    public int Length
    {
        get { return length; }
        set { length = value; }
    }

    //Metodo para obtener/cambiar el ancho
    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    //Metodo para obtener/cambiar el alto
    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    //Metodo para obtener/modificar el tamaño por unidad
    public float UnitSize
    {
        get { return unitSize; }
        set { unitSize = value; }
    }

    //Metodo para obtener/cambiar el origen
    public DataObjects.Point Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    //Constructor para inicializar la malla
    public GridObject(int length, int width, int height, float unitSize, DataObjects.Point origin, T defaultValue)
    {
        //Guarda los datos
        this.length = length;
        this.width = width;
        this.height = height;
        this.unitSize = unitSize;
        this.origin = origin;

        //Inicializa el tamaño de la malla
        grid = new T[length, height, width];

        //inicializa los datos de la malla
        for(int x=0; x<length; x++)
        {
            for(int y=0; y<height; y++)
            {
                for(int z=0; z<width; z++)
                {
                    grid[x, y, z] = defaultValue;
                }
            }
        }
    }

    //Metodo para convertir coordenadas de la escena en coordenadas de la malla
    public DataObjects.Point WorldCoordinatesToGrid(float x, float y, float z)
    {
        //Guarda las coordenadas relativas a la malla y la escala al tamaño de la malla
        float relativeX = (x - (float)origin.x) / unitSize;
        float relativeY = (y - (float)origin.y) / unitSize;
        float relativeZ = (z - (float)origin.z) / unitSize;
        
        //Si alguna es negativa entonces no puede funcionar
        if (relativeX < 0 || relativeX >= length || relativeY < 0 || relativeY >= height || relativeZ < 0 || relativeZ >= width)
        {
            return new DataObjects.Point(-1, -1, -1);
        }

        //Retorna la coordenada convirtiendolas a enteros, esto hace que por ejemplo 4.9 siga perteneciendo a la coordenada 4 en la malla
        return new DataObjects.Point((int)relativeX, (int)relativeY, (int)relativeZ);
    }

    //Metodo para convertir coordenadas de la malla en coordenadas de la escena

    //Metodo para obtener el dato ubicado en la coordenada xyz de la malla
    public T GetDataFromGridCoordinates(int x, int y, int z)
    {
        //Comprueba que todos los valores esten dentro del rango
        if (x < 0 || x >= length || y < 0 || y >= height || z < 0 || z >= width)
        {
            return default(T);
        }

        //Retorna la informacion en la coordenada
        return grid[x, y, z];
    }

    //Metodo para guardar el dato en la coordenada xyz de la malla
    public bool SetDataFromGridCoordinates(int x, int y, int z, T value)
    {
        //Comprueba que todos los valores esten dentro del rango
        if (x < 0 || x >= length || y < 0 || y >= height || z < 0 || z >= width)
        {
            return false;
        }

        //Guarda el dato
        grid[x, y, z] = value;
        return true;
    }

    //Metodo para guardar el dato en la coordenada xyz de la escena
    public bool SetDataFromWorldCoordinates(float x, float y, float z, T value)
    {
        //Obtiene las coordenadas de la malla
        DataObjects.Point gridCoordinates = WorldCoordinatesToGrid(x, y, z);

        //Si esta dentro de rango
        if(gridCoordinates != new DataObjects.Point(-1, -1, -1))
        {
            //Guarda la informacion en la coordenada retornada
            grid[(int)gridCoordinates.x, (int)gridCoordinates.y,(int)gridCoordinates.z] = value;
            return true;
        }

        return false;
    }

    //Metodo para obtener el dato ubicado en la coordenada xyz de la escena
    public T GetDataFromWorldCoordinates(float x, float y, float z)
    {
        //Obtiene las coordenadas de la malla
        DataObjects.Point gridCoordinates = WorldCoordinatesToGrid(x, y, z);

        //Si esta dentro de rango
        if (gridCoordinates != new DataObjects.Point(-1, -1, -1))
        {
            //Guarda la informacion en la coordenada retornada
            return grid[(int)gridCoordinates.x, (int)gridCoordinates.y, (int)gridCoordinates.z];
        }

        return default(T);
    }
}
