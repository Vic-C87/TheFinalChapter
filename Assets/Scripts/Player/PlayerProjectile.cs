using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    float mySpeed;
    [SerializeField]
    float myLifeTime;
    [SerializeField]
    float myDamage;

    float myInstantiationTime;

    Vector2 myDirection;


    // Start is called before the first frame update
    void Start()
    {
        myInstantiationTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (myLifeTime < Time.realtimeSinceStartup - myInstantiationTime)
        {
            Destroy(this.gameObject);
        }

    }

    void FixedUpdate()
    {
        transform.position += (Vector3)myDirection * mySpeed * Time.deltaTime;
    }

    public void SetDirection(Vector2 aDirection, float aDamageAmount = 0)
    {
        myDirection = aDirection;
        if (myDamage == 0)
            myDamage = aDamageAmount;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(myDamage);
            //Add hiteffect
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Obstacles"))
        {
            //Add hiteffect
            Destroy(this.gameObject);
        }

    }
}
