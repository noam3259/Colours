using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCursor : MonoBehaviour
{
    public Vector2 sensitivity;
    private Vector2 rotation;//the current rotation
    public float maxVerticalAngelFromHorizon;


    private Vector2 GetInput()//get the cursor x & y
    {

        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (input!=new Vector2(0,0))
        {
            Debug.Log("");
        }

        return input;
    }

    private float ClampVerticalAngel(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngelFromHorizon, maxVerticalAngelFromHorizon);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 wantedVelocity = GetInput() * sensitivity;

        rotation += wantedVelocity * Time.deltaTime;
        rotation.y = ClampVerticalAngel(rotation.y);
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);


    }
}
