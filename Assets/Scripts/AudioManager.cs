using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public float gameVolume;
    AudioSource gameSound;

    public enum ESoundNames
    {
        Whispers

    }

    Dictionary<ESoundNames, AudioClip> audioDictionary = new Dictionary<ESoundNames, AudioClip>();

    // Start is called before the first frame update
    void Awake()
    {
        gameSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateSound(ESoundNames sounds)
    {
        gameSound.PlayOneShot(audioDictionary[sounds], gameVolume);
    }
}
