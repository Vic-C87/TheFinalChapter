using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    private float horizontalAim;
    private float verticalAim;

    public Vector3 Direction;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        Direction = Vector3.left;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        horizontalAim = Input.GetAxis("HorizontalA");
        verticalAim = Input.GetAxis("VerticalA");

        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);

        Vector3 temp = new Vector3(horizontalAim, verticalAim, 0);
        if (temp != Vector3.zero)
        {
            Direction = temp;
        }


    }
}
