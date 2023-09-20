using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager instance;

[SerializeField] private AudioSource _musicSource, _effectSource;


void Awake()
{
    instance = this;
    DontDestroyOnLoad(gameObject);

}

public void PlaySound(AudioClip clip)
{
  _effectSource.PlayOneShot(clip); 
}

}
