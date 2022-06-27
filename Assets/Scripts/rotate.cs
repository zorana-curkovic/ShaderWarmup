using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [Range(1, 10)]
    public float speed;
    public bool rotX;
    public bool rotY;
    public bool rotZ;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = Vector3.zero;
        if (rotX) rotation.x += Time.deltaTime * speed;
        if (rotY) rotation.y += Time.deltaTime * speed;
        if (rotZ) rotation.z += Time.deltaTime * speed;

        transform.Rotate(rotation);
    }
}
