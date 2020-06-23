using UnityEditor;
using UnityEngine;

public class PointCloudReader : MonoBehaviour
{
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            string error;
            CloudPoint cloudPoint = ConvertXYZFileToData(out error);

            if(cloudPoint != null)
            {
                Debug.Log("Lectura de archivo exitosa. Los elementos son:");
                CList< DataObjects.Point > points = cloudPoint.GetElements();
                Debug.Log(points.Length);
                for(int i=0; i<points.Length; i++)
                {
                    Debug.Log(i+"-> X: " + points[i].x + ". Y: " + points[i].y + ". Z: " + points[i].z + ".");
                }
                Debug.Log("Min: X: " + cloudPoint.GetBoundingBoxMinPoint().x + ". Y: " + cloudPoint.GetBoundingBoxMinPoint().y + ". Z: " + cloudPoint.GetBoundingBoxMinPoint().z + ".");
                Debug.Log("Max: X: " + cloudPoint.GetBoundingBoxMaxPoint().x + ". Y: " + cloudPoint.GetBoundingBoxMaxPoint().y + ". Z: " + cloudPoint.GetBoundingBoxMaxPoint().z + ".");
            }
            else
            {
                Debug.Log(error);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
    }

    //Metodo para abrir un archivo con un formato obj
    public static string OpenOBJFile()
    {
        return EditorUtility.OpenFilePanel("Objeto OBJ", "", "obj");
    }

    public static CloudPoint ConvertOBJFileToData()
    {

    }

    //Metodo para abrir un archivo con un formato coincidente xyz
    public static string OpenXYZFile()
    {
        //Abre una ventana de archivos para elegir el archivo que se quiere y guarda su direccion
        return EditorUtility.OpenFilePanel("Cloud Point File", "", "xyz");
    }

    //Convierte el formato xyz a una lista con todos los puntos
    public static CloudPoint ConvertXYZFileToData(out string errorInfo)
    {
        //Primero pregunta por el archivo
        string directory = OpenXYZFile();

        //Si el archivo existe
        if (directory.Length > 0)
        {
            //Obtiene todas las lineas del archivo
            string[] lines = System.IO.File.ReadAllLines(directory);

            //Si el archivo si tiene informacion
            if(lines.Length > 0)
            {
                //Comprueba que la primera linea del archivo sea la cantidad de puntos que contiene
                int numberOfAtoms;
                bool hasNumberOfAtoms = int.TryParse(lines[0], out numberOfAtoms);

                //Si tiene la cantidad de puntos entonces puede ser un archivo xyz valido
                if (hasNumberOfAtoms)
                {
                    //Comprueba que el numero de puntos sea mayor a 0
                    if(numberOfAtoms > 0)
                    {
                        //Crea un nuevo objeto para almacenar los datos
                        CloudPoint cloudPoint = new CloudPoint();

                        //Guarda la cantidad de puntos que tiene
                        cloudPoint.SetNumberOfAtoms(numberOfAtoms);

                        //Aqui se guarda la magnitud de el punto mas pequeño y el punto más grande para saber el tamaño del objeto
                        float boundingBoxMinPointMagnitude = 0;
                        float boundingBoxMaxPointMagnitude = 0;

                        //Recorre a partir de la tercera linea cada uno de los elementos hasta la cantidad de puntos indicada por numberOfAtoms
                        for (int i = 2; i < lines.Length && i-2 < numberOfAtoms; i++)
                        {
                            //Separa los elementos de la linea en sus 4 componentes (nombre, x, y, z)
                            string[] lineInfo = lines[i].Split(' ');

                            //Comprueba que tenga sus 4 componentes
                            if (lineInfo.Length == 4)
                            {
                                //Arreglo que guarda las componentes x y z
                                double[] components = new double[3];

                                //Recorre cada una de las componentes encontradas (sin tomar en cuenta el nombre)
                                for (int j = 1; j < 4; j++)
                                {
                                    //Intenta convertir el string en un numero
                                    double component;
                                    bool isComponent = double.TryParse(lineInfo[j], out component);

                                    //Si pudo convertirla en un numero quiere decir que es un componente
                                    if (isComponent)
                                    {
                                        //Guarda el componente
                                        components[j - 1] = component;

                                        //Continua con la siguiente iteracion
                                        continue;
                                    }
                                    
                                    //Si llega hasta aqui quiere decir que no es un punto y por lo tanto el formato es incorrecto
                                    errorInfo = "Error: El formato es incorrecto. El punto " + lineInfo[0] + " no pudo ser convertido a una coordenada.";
                                    return null;
                                }

                                //Crea un objeto Point y lo guarda en el objeto cloudPoint
                                DataObjects.Point element = new DataObjects.Point(components[0], components[1], components[2]);
                                cloudPoint.AddPoint(element);

                                //Comprueba si la magnitud del punto es menor a boundingBoxMinPointMagnitude, de ser asi entonces es ahora el mas pequeño
                                if(element.magnitude < boundingBoxMinPointMagnitude || i==2)
                                {
                                    cloudPoint.SetBoundingBoxMinPoint(element);
                                    boundingBoxMinPointMagnitude = element.magnitude;
                                }
                                //Comprueba si la magnitud del punto es mayor a boundingBoxMaxPointMagnitude, de ser asi entonces es ahora el mas grande
                                else if (element.magnitude > boundingBoxMaxPointMagnitude)
                                {
                                    cloudPoint.SetBoundingBoxMaxPoint(element);
                                    boundingBoxMaxPointMagnitude = element.magnitude;
                                }

                                //Continua con la siguiente iteracion
                                continue;
                            }

                            //Si llego aqui quiere decir que no hay datos suficientes
                            errorInfo = "Error: Datos insuficientes en la linea " + (i + 1) + ".";
                            return null;
                        }

                        //Si llego aqui entonces no hay error y el formato es correcto
                        errorInfo = "";
                        return cloudPoint;
                    }

                    //Si llego aqui quiere decir que no tiene puntos y por lo tanto no es valido
                    errorInfo = "Error: La cantidad de puntos no es valida. Tiene que existir al menos un punto.";
                    return null;
                }

                //Si llego aqui entonces quiere decir que no es un formato xyz valido
                errorInfo = "Error: El formato no es valido. No existe un valor para indicar cuantos puntos existen";
                return null;
            }
        }

        //Indica que el directorio no fue asignado
        errorInfo = "Error: El directorio no fue asignado.";
        return null;
    }
}
