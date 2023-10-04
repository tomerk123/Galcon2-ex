using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager instance;

[SerializeField] private AudioSource _musicSource, _effectSource;

// CR: [discuss] important to destroy if an instance already exists.
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
