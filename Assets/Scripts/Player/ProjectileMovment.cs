using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovment : MonoBehaviour
{
    [SerializeField]
    float mySpeed;
    [SerializeField]
    float myLifeTime;
    [SerializeField]
    float myDamage;

    float myInstantiationTime;
    Transform myPlayer;
    Vector2 myDirection;

    // Start is called before the first frame update
    void Start()
    {
        myInstantiationTime = Time.realtimeSinceStartup;
        myPlayer = GameManager.myInstance.GetPlayer();
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

    public void SetDirection(Vector2 aDirection)
    {
        myDirection = aDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health healthScript = collision.GetComponent<Health>();
            healthScript.health -= myDamage;
        }

        Destroy(this.gameObject);
    }
}
