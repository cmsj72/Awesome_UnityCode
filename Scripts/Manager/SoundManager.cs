using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager instance = null;

    private AudioSource bgmPlayer;

    [SerializeField] private AudioClip introBgm;
    //출처 https://youtu.be/L5mC4wV_HCM
    [SerializeField] private AudioClip gameBgm;
    //출처 https://youtu.be/uMw09BvCuwo
    public float volumeUnit;
    public AudioClip IntroBgm
    {
        get { return IntroBgm; }
    }
    public AudioClip GameBgm
    {
        get { return gameBgm; }
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        Init();
    }

    private void Init()
    {
        bgmPlayer = this.GetComponent<AudioSource>();
    }

    public void BgmPlay(bool flag)
    {
        if (flag) bgmPlayer.Play();
        else bgmPlayer.Stop();
    }
}
