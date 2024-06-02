using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SoundSkripKontrol : MonoBehaviour
{

    public AudioSource audioSource; 
    public AudioSource audioEffect;
    public AudioSource efekPlayer;

    public AudioSource[] audioSFX;

    public Slider volumeSlider;
    public Slider EfectVolumeSlider;

    public AudioClip bgm;
    public AudioClip bgKalah;
    public AudioClip bgMenang;

    public AudioClip efekTombol;



    public bool playWin;
    public bool playLose;

    //public VideoPlayer vp;

    private AudioSource[] allAudioSources;
    private bool[] wasPlaying;
    public static bool lagiPause;



    void Start()
    {
        playLose = false;
        playWin = false;
        float valueBGM;
        float valueSFX;
        // Pastikan volume slider memiliki nilai volume yang benar
        SaveManager.LoadOptionSound(out valueBGM, "BGM");
        SaveManager.LoadOptionSound(out valueSFX, "SFX");
        volumeSlider.value = valueBGM;
        EfectVolumeSlider.value = valueSFX;
        audioSource.volume = volumeSlider.value;
        
        foreach (AudioSource source in audioSFX)
        {
            source.volume = EfectVolumeSlider.value;
        }

        

        audioSource.clip = bgm;
        audioSource.loop = true;

        audioEffect.clip = efekTombol;

        allAudioSources = FindObjectsOfType<AudioSource>();
        wasPlaying = new bool[allAudioSources.Length];
    }
    private void Update()
    {
     
        if (playWin)
        {
            audioSource.clip = bgMenang;
            audioSource.Play();
            audioSource.loop = false;
            playWin = false;
        }
        if (playLose)
        {
            audioSource.clip = bgKalah;
            audioSource.Play();
            audioSource.loop = false;
            playLose = false;
        }


        

    }
    // Method ini akan dipanggil saat slider diubah
    public void SetVolume(string audio)
    {
        // Atur volume audio sesuai nilai slider
        switch (audio)
        {
            case "BGM":
                SaveManager.SaveOptionSound(volumeSlider.value, audio);
                audioSource.volume = volumeSlider.value;
                break;
            case "SFX":
                SaveManager.SaveOptionSound(EfectVolumeSlider.value, audio);
                foreach (AudioSource source in audioSFX)
                {
                    source.volume = EfectVolumeSlider.value;
                }
                
                break;
            default:
                break;
        }
        
        
        
        
    }

    public void AudioTombol()
    {
        audioEffect.time = 0.05f;
        audioEffect.Play();

    }

    public void PauseAllAudio()
    {
        lagiPause = true;
        audioSource.Pause();
        for (int i = 0; i < audioSFX.Length; i++)
        {
            // Simpan status bermain setiap AudioSource sebelum di-pause
            
                audioSFX[i].Pause();
            
        }
    }

    public void ResumeAllAudio()
    {
        lagiPause = false;
        audioSource.UnPause();
        for (int i = 0; i < audioSFX.Length; i++)
        {
            // Lanjutkan pemutaran hanya jika AudioSource sebelumnya bermain
           
                audioSFX[i].UnPause();
            
        }
    }

   
}

