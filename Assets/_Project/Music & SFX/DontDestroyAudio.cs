using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    // Start is called before the first frame update
    
    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed = true;
    
    // void Awake()
    // {
    //     DontDestroyOnLoad(transform.gameObject);
    // }

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if(!alreadyPlayed)
        {
            audio.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }


}
