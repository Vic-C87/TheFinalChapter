using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public float gameVolume;

    private AudioSource gameSound;
    public AudioClip walkSound;
    public AudioClip hitSound;
    public AudioClip whisperSound;
    public AudioClip screamSound;
    public AudioClip rangeSound;
    public AudioClip meleeSound;
    public AudioClip cryingSound;

    public enum ESoundNames
    {
        Whispers,
        Walk,
        Hit,
        Range,
        Melee,
        Scream,
        Cry,

    }

    Dictionary<ESoundNames, AudioClip> audioDictionary = new Dictionary<ESoundNames, AudioClip>();

    // Start is called before the first frame update
    void Awake()
    {
        gameSound = GetComponent<AudioSource>();

        audioDictionary.Add(ESoundNames.Whispers, whisperSound);
        audioDictionary.Add(ESoundNames.Walk, walkSound);
        audioDictionary.Add(ESoundNames.Hit, hitSound);
        audioDictionary.Add(ESoundNames.Scream, screamSound);
        audioDictionary.Add(ESoundNames.Range, rangeSound);
        audioDictionary.Add(ESoundNames.Melee, meleeSound);
        audioDictionary.Add(ESoundNames.Cry, cryingSound);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActivateSound(ESoundNames sounds)
    {
        gameSound.PlayOneShot(audioDictionary[sounds], gameVolume);
    }
}
