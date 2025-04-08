using UnityEngine;

public class RotatePanel : MonoBehaviour
{   
    [SerializeField] 
    public float rotation = 0f; // Rotation around the Z-axis
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private bool direction = true; // true for clockwise, false for counter-clockwise
    private int directionValue = 1; // 1 for clockwise, -1 for counter-clockwise
    public int index = -1;
    private Vector3 axis = Vector3.forward;
    private Quaternion locate;
    private SceneDirector director;


    private void Awake()
    {
        if (!direction)
        {
            directionValue = -1; // Set to -1 for counter-clockwise rotation
        }
    }

    private void Start()
    {
        locate = transform.rotation;
        director = GameObject.FindGameObjectsWithTag("SceneDirector")[0].GetComponent<SceneDirector>();
        if (director == null)
        {
            Debug.LogError("SceneDirector not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (director != null && index >= 0)
        {
            rotation = director.GetAngle(index); // Assuming the first value is the rotation angle}
        }
        transform.rotation = locate * Quaternion.AngleAxis(directionValue * rotation, axis);
    }
}
