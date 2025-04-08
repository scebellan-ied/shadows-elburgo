using UnityEngine;

public class LightDirector : MonoBehaviour
{
    private (float, float) sunPosition;
    private SceneDirector director;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        director = GameObject.FindGameObjectsWithTag("SceneDirector")[0].GetComponent<SceneDirector>();
        if (director == null)
        {
            Debug.LogError("SceneDirector not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        sunPosition = director.GetSunPosition();
        transform.rotation = Quaternion.Euler(sunPosition.Item1, sunPosition.Item2 + 90, 0);
    }
}
