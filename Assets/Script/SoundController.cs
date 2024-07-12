using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void PlayThisSound(string clipName, float volumeMultiplier)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume *= volumeMultiplier;
        audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + clipName, typeof(AudioClip)));
    }
}
