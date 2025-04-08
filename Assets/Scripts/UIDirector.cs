using TMPro;
using UnityEngine;

public class UIDirector : MonoBehaviour
{
    private SceneDirector sceneDirector;
    private string key = "0";
    private TextMeshProUGUI textMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneDirector = GameObject.FindGameObjectsWithTag("SceneDirector")[0].GetComponent<SceneDirector>();
        if (sceneDirector == null)
        {
            Debug.LogError("SceneDirector not found in the scene.");
        }
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        key = sceneDirector.GetKey();
        textMesh.text = key;

    }
}
