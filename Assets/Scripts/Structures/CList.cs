using System.Diagnostics;
using UnityEngine.UIElements;

public class CList<T>
{
    //Atributos
    private int length = 0;
    private T[] elements;

    private int currentPosition = 0;

    //Constructor por defecto
    public CList()
    {
        elements = new T[1];
    }

    //Constructor para inicializarlo con sus elementos
    public CList(T element)
    {
        elements = new T[1];
        Add(element);
    }

    //Sobrecarga el operador [] para acceder como un arreglo
    public T this[int i]
    {
        get
        {
            //Comprueba que exista el index
            if(i >= 0 && i < length)
            {
                //Retorna el elemento
                return elements[i];
            }

            //Esta fuera de rango, por lo tanto no es valido
            throw new System.IndexOutOfRangeException("Index out of range.");
        }
        set
        {

            //Comprueba que exista el index
            if (i >= 0 && i < length)
            {
                //Retorna el elemento
                elements[i] = value;
            }

            //Esta fuera de rango, por lo tanto no es valido
            throw new System.IndexOutOfRangeException("Index out of range.");
        }
    }

    //Metodo para obtener la longitud
    public int Length
    {
        get
        {
            return length;
        }
    }

    //Agrega un elemento al final de la lista
    public void Add(T element)
    {
        //Comprueba que aun haya espacio dentro del arreglo
        if(length < elements.Length)
        {
            //Agrega el elemento en la posicion actual
            elements[currentPosition] = element;

            //Aumenta la longitud y la posicion actual
            length++;
            currentPosition++;

            return;
        }

        //Si no hay espacio entonces duplica su tamaño y copia todos los datos
        T[] tempData = new T[elements.Length * 2];
        
        //Recorre todos los datos para copiarlos en tempData
        for(int i=0; i<elements.Length; i++)
        {
            tempData[i] = elements[i];
        }

        //Sustituye el arreglo por el nuevo con mas espacio
        elements = tempData;

        //Agrega el elemento en la posicion actual
        elements[currentPosition] = element;

        //Aumenta la longitud y la posicion actual
        length++;
        currentPosition++;
    }
}
