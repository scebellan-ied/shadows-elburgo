using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]
    private string filePath = Application.dataPath + "/positions.csv";
    public bool nrel = false;
    private List<(string, (float, float), float, float[])> angles;
    private int index = 7;
    private int maxIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        angles = new List<(string, (float, float), float, float[])>();

        string[] csvLines = System.IO.File.ReadAllLines(filePath);
        string[] splitline;
        string key;
        (float, float) sunPosition;
        float nrel;

        foreach (string line in csvLines)
        {
            splitline = line.Split(',');
            key = splitline[0];
            sunPosition = (float.Parse(splitline[1], NumberStyles.Float, CultureInfo.InvariantCulture), 
                float.Parse(splitline[2], NumberStyles.Float, CultureInfo.InvariantCulture));
            nrel = float.Parse(splitline[3], NumberStyles.Float, CultureInfo.InvariantCulture);
            float[] values = new float[splitline.Length - 4];
            for (int i = 4; i < splitline.Length; i++)
            {
                if (float.TryParse(splitline[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                {
                    values[i - 4] = value;
                }
                else
                {
                    Debug.LogError($"Failed to parse value '{splitline[i]}' as float.");
                }
            }
            angles.Add((key, sunPosition, nrel, values));
        }
        maxIndex = angles.Count - 1;
    }

    public void Next()
    {
        if (index < maxIndex)
        {
            index++;
            Debug.Log("Current instant: " + angles[index].Item1);
        }
        else
        {
            Debug.Log("Last instant reached: " + angles[index].Item1);
        }
    }

    public void Previous()
    {
        if (index > 0)
        {
            index--;
            Debug.Log("Current instant: " + angles[index].Item1);
        }
        else
        {
            Debug.Log("First instant reached: " + angles[index].Item1);
        }
    }



    public float GetAngle(int i)
    {
        if (nrel){
            return angles[index].Item3;
        }
        return angles[index].Item4[i];
    }

    /// <summary>
    /// Get the sun position (azimuth, elevation) for the current index.
    /// </summary>
    /// <returns> azimuth and elevation of the sun in degrees</returns>
    public (float, float) GetSunPosition()
    {
        return angles[index].Item2;
    }

    public string GetKey()
    {
        string suffix = "";
        if (nrel)
        {
            suffix = " - NREL";
        }
        return angles[index].Item1 + suffix;
    }
}
