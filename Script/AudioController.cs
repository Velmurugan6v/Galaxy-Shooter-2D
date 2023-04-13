using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField] AudioSource _sound;
    [SerializeField] AudioClip[] _sfx;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    public void PlaySFX(int sfxNo)
    {
        _sound.clip = _sfx[sfxNo];
        _sound.Play();
    }
}
