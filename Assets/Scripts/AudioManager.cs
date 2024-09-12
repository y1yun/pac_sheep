using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource BGM;
    public AudioSource empty;

    void Start()
    {
        intro.Play();
        Invoke("AfterIntro", intro.clip.length);
    }

    void AfterIntro()
    {
        BGM.loop = true;
        empty.loop = true;
        BGM.Play();
        empty.Play();
    }
}
