using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    [SerializeField]
    float mySpeed;
    [SerializeField]
    Vector3 myDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Debug Horizontal");
        float y = Input.GetAxis("Debug Vertical");

        myDirection = new Vector3(x, y, 0);

        transform.position += myDirection * mySpeed * Time.deltaTime;
    }
}
