using UnityEngine;

public class RotatePanel : MonoBehaviour
{   
    [SerializeField] 
    private float rotation = 0f; // Rotation around the Z-axis
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 axis = Vector3.right;
    private Quaternion locate;

    void Start()
    {
        locate = transform.rotation; // Store the initial rotation of the panel
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = locate * Quaternion.AngleAxis(rotation, axis);
    }
}
