using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PanelLocation
{
    public List<float> position;
    public float azimuth;
    public float tilt;
    // public float rotation;
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
    // public SunPosition sun;
}

public class LoadPanels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject lightSource;
    [SerializeField] private string jsonPath = Application.dataPath + "/panelLocations.json";
    void Start()
    {
        if (System.IO.File.Exists(jsonPath))
        {
            GameObject gameObject;
            string jsonContent = System.IO.File.ReadAllText(jsonPath);
            PanelLocationList sceneInfo = JsonUtility.FromJson<PanelLocationList>(jsonContent);
            for (int i = 0; i < sceneInfo.locations.Count; i++)
            {
                var location = sceneInfo.locations[i];
                Vector3 position = new Vector3(location.position[1], location.position[2], location.position[0]);
                Quaternion azimuth = Quaternion.AngleAxis(location.azimuth, Vector3.up);
                Quaternion tilt = Quaternion.AngleAxis(-location.tilt, Vector3.right);
                Quaternion full_rotation = azimuth * tilt;

                gameObject = Instantiate(panel, position, full_rotation);
                gameObject.GetComponent<RotatePanel>().index = i; // Assign index
            }
            // lightSource.transform.rotation = Quaternion.Euler(sceneInfo.sun.elevation, sceneInfo.sun.azimuth, 0);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + jsonPath);
        }
    }
}
