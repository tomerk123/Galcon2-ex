using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
public static SoundManager Instance;

[SerializeField] private AudioSource _musicSource, _effectSource;


void Awake()
{
    Instance = this;
    DontDestroyOnLoad(gameObject);

}

public void PlaySound(AudioClip clip)
{
  _effectSource.PlayOneShot(clip); 
}





}
