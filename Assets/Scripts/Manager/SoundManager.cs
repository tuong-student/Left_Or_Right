using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class SoundManager : MonoBehaviorInstance<SoundManager>
{
    [SerializeField] AudioSource SubPlayer, MainPlayer;
    [SerializeField] AudioClip click, success, fail, improve;

    public void PlayAudio(SoundType type)
    {
        switch (type)
        {
            case SoundType.Click:
                SubPlayer.clip = click;
                SubPlayer.Play();
                break;
            case SoundType.Improve:
                SubPlayer.clip = improve;
                SubPlayer.Play();
                break;
            case SoundType.Fail:
                SubPlayer.clip = fail;
                MainPlayer.volume = 0.3f;
                SubPlayer.Play();
                break;
            case SoundType.Success:
                SubPlayer.clip = success;
                MainPlayer.volume = 0.3f;
                SubPlayer.Play();
                break;
        }
    }
}

public enum SoundType
{
    Click,
    Improve,
    Success,
    Fail
}
