using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    [SerializeField]
    float mySpeed;
    [SerializeField]
    Vector2 myDirection;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Debug Horizontal");
        float y = Input.GetAxisRaw("Debug Vertical");

        myDirection = new Vector2(x, y);

        myRigidbody.velocity =  myDirection.normalized * mySpeed;
    }
}
