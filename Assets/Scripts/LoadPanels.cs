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
    public float length;
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
    [SerializeField] private string jsonPath = "panelLocations.json";
    private string fullPath;
    void Start()
    {
        fullPath = Application.dataPath + "/" + jsonPath;
        if (System.IO.File.Exists(fullPath))
        {
            GameObject gameObject;
            string jsonContent = System.IO.File.ReadAllText(fullPath);
            PanelLocationList sceneInfo = JsonUtility.FromJson<PanelLocationList>(jsonContent);
            for (int i = 0; i < sceneInfo.locations.Count; i++)
            {
                var location = sceneInfo.locations[i];
                Vector3 position = new Vector3(location.position[0], location.position[1], location.position[2]);
                Quaternion azimuth = Quaternion.AngleAxis(location.azimuth, Vector3.up);
                Quaternion tilt = Quaternion.AngleAxis(-location.tilt, Vector3.right);
                Quaternion full_rotation = azimuth * tilt;
                

                gameObject = Instantiate(panel, position, full_rotation);
                gameObject.GetComponent<RotatePanel>().index = i; // Assign index
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, location.length);
            }
            // lightSource.transform.rotation = Quaternion.Euler(sceneInfo.sun.elevation, sceneInfo.sun.azimuth, 0);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + fullPath);
        }
    }
}
