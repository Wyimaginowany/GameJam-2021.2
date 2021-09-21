using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
