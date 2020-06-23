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
            for (int y = 0; y < height; y++)
            {
                float noise = Mathf.PerlinNoise((float)x/width*scale, (float)y/height*scale);

                texture.SetPixel(x, y, new Color(noise, noise, noise));
            }
        }

        texture.Apply();

        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
