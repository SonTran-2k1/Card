using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
public class MusicManager : RingSingleton<MusicManager>
{
    public MusicController _musicController;

    private void Start()
    {
        PlayerBackGround();
    }

    public void PlayAudio_Grenade()
    {
        //_musicController.audioSource_Grenade.PlayOneShot(_musicController.audioClip_Grenade);
    }

    public void PlayerBackGround()
    {
        //_musicController.audioSource_BackGround.Play();
    }
}
