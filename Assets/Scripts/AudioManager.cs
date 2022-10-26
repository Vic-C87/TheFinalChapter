using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }



    // Start is called before the first frame update
    void Awake()
    {
        ActivateSound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateSound()
    {

    }
}
