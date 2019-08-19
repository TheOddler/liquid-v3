using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class DebugValueChecker : MonoBehaviour
{
    [SerializeField]
    Simulation _simulation = null;

    InputMaster _input;

    Texture2D _textureCache = null;
    float[] _floatsCache = null;

    void Awake()
    {
        _textureCache = new Texture2D(_simulation.GridPixelCount, _simulation.GridPixelCount, TextureFormat.RGBAFloat, false);
        _floatsCache = new float[_simulation.GridPixelCount * _simulation.GridPixelCount * 4];
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateCache(_simulation.CurrentOutflowFluxRLBT);
        // LogCacheMaxMinAvg("Flux R", 0);
        // LogCacheMaxMinAvg("Flux L", 1);
        // LogCacheMaxMinAvg("Flux B", 2);
        // LogCacheMaxMinAvg("Flux T", 3);

        // UpdateCache(_simulation.CurrentWaterSandRockSediment);
        // LogCacheMaxMinAvg("Water", 0);
        // LogCacheMaxMinAvg("Sand", 1);
        // LogCacheMaxMinAvg("Rock", 2);
        // LogCacheMaxMinAvg("Sediment", 3);

        // UpdateCache(_simulation.CurrentOutflowFluxRLBT);
        // LogCacheCenter("Flux R", 0);
        // LogCacheCenter("Flux L", 1);
        // LogCacheCenter("Flux B", 2);
        // LogCacheCenter("Flux T", 3);

        // UpdateCache(_simulation.CurrentWaterSandRockSediment);
        // LogCacheCenter("Water", 0);
        // LogCacheCenter("Sand", 1);
        // LogCacheCenter("Rock", 2);
        // LogCacheCenter("Sediment", 3);

        UpdateCache(_simulation.CurrentWaterSandRockSediment);
        LogCacheTotal("Water", 0);
    }

    private void UpdateCache(RenderTexture renderTexture)
    {
        // update heights texture
        var currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        _textureCache.ReadPixels(new Rect(0, 0, _textureCache.width, _textureCache.height), 0, 0, false);
        _textureCache.Apply();

        RenderTexture.active = currentActiveRT;

        // convert to color floats
        var rawColors = _textureCache.GetRawTextureData();
        System.Buffer.BlockCopy(rawColors, 0, _floatsCache, 0, rawColors.Length);
    }

    void DumpCache(string header)
    {
        // Dump to console
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < _textureCache.height; ++y)
        {
            for (int x = 0; x < _textureCache.width; ++x)
            {
                int i = (y * _textureCache.width + x) * 4;
                float water = _floatsCache[i];
                float sand = _floatsCache[i + 1];
                float rock = _floatsCache[i + 2];
                float sediment = _floatsCache[i + 3];

                sb.Append("(");
                sb.Append(water);
                sb.Append(", ");
                sb.Append(sand);
                sb.Append(", ");
                sb.Append(rock);
                sb.Append(", ");
                sb.Append(sediment);
                sb.Append("), ");
            }
            sb.AppendLine();
        }

        Debug.Log(header);
        Debug.Log(sb);
    }

    void CheckCacheForNaN(string name)
    {
        for (int i = 0; i < _floatsCache.Length; ++i)
        {
            if (float.IsNaN(_floatsCache[i]))
            {
                Debug.Log("Found NaN in: " + name);
                Debug.Break();
            }
        }
    }

    void LogCacheMaxMinAvg(string name, int offset)
    {
        float min = float.PositiveInfinity;
        float max = float.NegativeInfinity;
        float total = 0;
        for (int y = 0; y < _textureCache.height; ++y)
        {
            for (int x = 0; x < _textureCache.width; ++x)
            {
                int i = (y * _textureCache.width + x) * 4;
                float height = _floatsCache[i + offset];

                if (height < min) min = height;
                if (height > max) max = height;
                total += height;
            }
        }
        float avg = total / (_textureCache.height * _textureCache.width);
        Debug.Log(name + " - Min: " + min + "; Max: " + max + "; Avg: " + avg);
    }

    void LogCacheCenter(string name, int offset)
    {
        int i = (_textureCache.height / 2 * _textureCache.width + _textureCache.width / 2) * 4;
        float height = _floatsCache[i + offset];

        Debug.Log(name + " - Val: " + height);
    }

    void LogCacheTotal(string name, int offset)
    {
        float total = 0;
        for (int y = 0; y < _textureCache.height; ++y)
        {
            for (int x = 0; x < _textureCache.width; ++x)
            {
                int i = (y * _textureCache.width + x) * 4;
                float height = _floatsCache[i + offset];
                total += height;
            }
        }
        Debug.Log(name + " - Total: " + total);
    }
}
