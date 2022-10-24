using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);

    }
}
