using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovment : MonoBehaviour
{
    public float projectileSpeed;
    private PlayerMovment playerMovmentScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovmentScript = GameObject.Find("Player").GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
