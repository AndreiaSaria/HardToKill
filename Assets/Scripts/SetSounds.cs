using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSounds : MonoBehaviour
{
    private AudioSource source;
    private float initialPitch;

    public AudioClip step;
    public AudioClip death;
    public AudioClip damaged;

    public float pitchRange = 0.2f;

    void Start()
    {
        source = this.GetComponent<AudioSource>();
        source.enabled = true;
        initialPitch = source.pitch;
    }

    public void Step()
    {
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(step);

    }

    public void DieSound()
    {
        source.pitch = initialPitch + Random.Range(-pitchRange, +pitchRange);
        source.PlayOneShot(death);
    }

    public void DamagedSound()
    {
        source.pitch = initialPitch;
        source.PlayOneShot(damaged);
    }
}
