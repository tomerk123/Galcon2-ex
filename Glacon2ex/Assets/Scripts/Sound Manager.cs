using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager instance;

[SerializeField] private AudioSource _musicSource, _effectSource;

void Awake()
{
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(this);
    }
}

public void PlaySound(AudioClip clip)
{
  _effectSource.PlayOneShot(clip); 
}

}
