using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whispers : MonoBehaviour
{
    AudioSource audioSourceWhispers;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceWhispers = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playWispers()
    {
        audioSourceWhispers.Play();
    }
}
