using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PanelLocation
{
    public List<float> position;
    public float azimuth;
    public float tilt;
    public float rotation;
    public float size;
}

[System.Serializable]
public class SunPosition
{
    public float azimuth;
    public float elevation;
}

[System.Serializable]
public class PanelLocationList
{
    public List<PanelLocation> locations;
    public SunPosition sun;
}

public class LoadPanels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject lightSource;
    void Start()
    {
        string jsonPath = Application.dataPath + "/panelLocations.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonContent = System.IO.File.ReadAllText(jsonPath);
            PanelLocationList sceneInfo = JsonUtility.FromJson<PanelLocationList>(jsonContent);
            foreach (var location in sceneInfo.locations)
            {
                Debug.Log("Creating panel at position: " + location.position);
                Vector3 position = new Vector3(location.position[0], location.position[2], location.position[1]);
                Quaternion azimuth = Quaternion.AngleAxis(location.azimuth, new Vector3(0, 1, 0));
                Quaternion tilt = Quaternion.AngleAxis(location.tilt, new Vector3(0, 0, 1));
                Quaternion rotation = Quaternion.AngleAxis(location.rotation, new Vector3(1, 0, 0));
                Quaternion full_rotation = azimuth * tilt * rotation;

                // Quaternion rotation = Quaternion.Euler(0, location.azimuth, location.tilt);
                Instantiate(panel, position, full_rotation);
            }
            lightSource.transform.rotation = Quaternion.Euler(sceneInfo.sun.elevation, sceneInfo.sun.azimuth, 0);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + jsonPath);
        }
    }
}
