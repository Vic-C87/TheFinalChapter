using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtPickUp : MonoBehaviour
{
    [SerializeField]
    float myRotationSpeed;
    [SerializeField]
    float myHPStrenght;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 360 * myRotationSpeed * Time.deltaTime);
    }

    public float PickUpHealth()
    {
        return myHPStrenght;
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
