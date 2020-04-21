using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ExecuteAlways alternative
[ExecuteInEditMode]
public class CreateTerrain : MonoBehaviour
{


    private TerrainData thisTerrain;
    // Start is called before the first frame update
    void Start()
    {
        thisTerrain = this.GetComponent<Terrain>().terrainData;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTerrain) //In editor mode with terrain object present
        {
            int hmRes = thisTerrain.heightmapResolution;
            float[,] heights = thisTerrain.GetHeights(0, 0, hmRes, hmRes);

            for (int i = 0; i < hmRes; i++)
            {
                for (int j = 0; j < hmRes; j++)
                {
                    float pn = totalNoise(i, j);
                    heights[i, j] = pn;
                }
            }

            thisTerrain.SetHeights(0, 0, heights);
        }
    }

    public int octaves = 8;
    public float zoom = 20.0f;
    [Range(0.0f, 1.0f)]
    public float persistence = 0.5f;

    float totalNoise(int x, int y)
    {
        float total = 0.0f;

        for (int i = 0; i < octaves; i++)
        {
            float freq = Mathf.Pow(2, i) / zoom;
            float amp = Mathf.Pow(persistence, i);

            total += Mathf.PerlinNoise(x * freq, y * freq) * amp;
        }
        return total;
    }
}
