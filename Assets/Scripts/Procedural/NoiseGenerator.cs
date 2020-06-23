using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    //Atributos
    public float frequency;
    public float scale;
    public int octaves = 10;
    public int width;
    public int height;

    private void Start()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            float noiseX = ((float)x / width) * frequency;
            float noise = PerlinNoise(x, octaves) / scale;

            for (int y = 0; y < height; y++)
            {
                
                if ((float)y/height > noise)
                {
                    texture.SetPixel(x, y, new Color(1, 1, 1));
                }
                else
                {
                    texture.SetPixel(x, y, new Color(0, 0, 0));
                }
            }
        }

        texture.Apply();

        GetComponent<Renderer>().material.mainTexture = texture;
    }

    //Metodo para generar ruido de perlin en 1d en un rango determinado
    public float PerlinNoise(float x, int octaves)
    {
        //Aqui se guarda el valor generado por el ruido
        float noise = 0f;

        //Obtiene el valor entero minimo y maximo de x
        int minX = Mathf.FloorToInt(x);
        int maxX = Mathf.CeilToInt(x);

        //Genera la semilla para el valor minX
        Random.InitState(minX);
        float minXSeed = Random.Range(0, 1f);

        //Genera la semilla para el valor maxX
        Random.InitState(maxX);
        float maxXSeed = Random.Range(0, 1f);

        //Hace una interpolacion entre minXSeed y maxXseed para saber cual semilla le corresponde a x
        float xSeed = Mathf.SmoothStep(minXSeed, maxXSeed, x - minX);

        //Guarda la escala actual
        float scale = 1.0f;

        //Guarda el total de escalas que se aplicaron
        float totalScale = 0.0f;

        //Recorre todos los octaves
        for (int o=0; o<octaves; o++)
        {
            //Multiplica la escala por el valor de xSeed y lo agrega a noise
            noise += scale * xSeed;

            //Guarda la escala aplicada
            totalScale += scale;

            //Divide la escala a la mitad
            scale = scale / 2.0f;
        }

        return noise / totalScale;
    }
}
