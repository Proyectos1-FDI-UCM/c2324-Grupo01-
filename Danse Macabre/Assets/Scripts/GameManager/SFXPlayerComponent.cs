using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hecho por Bing
public class SFXPlayerComponent : MonoBehaviour
{
    //solo para objetos
    [SerializeField]
    private AudioClip m_AudioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MusicManager.Instance.PlaySoundEffect(m_AudioClip);
    }
}