using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEfekPlayer : MonoBehaviour
{

    public AudioSource audioPlayer;
    public AudioSource audioPlayerEfek;
    public AudioClip soundKoin;
    public AudioClip soundSmoke;
    public AudioClip soundLempar;
    public AudioClip soundJalan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSoundKoin()
    {
        audioPlayerEfek.clip = soundKoin;
        audioPlayerEfek.time = 0.5f;
        audioPlayerEfek.Play();
        
    }
    public void playSoundSmoke()
    {
        audioPlayerEfek.clip = soundSmoke;
        audioPlayerEfek.time = 0.8f;
        audioPlayerEfek.Play();
        
    }
    public void playSoundLempar()
    {
        audioPlayer.loop = false;
        audioPlayer.clip = soundLempar;
        audioPlayer.time = 0.01f;
        audioPlayer.Play();
    }

    public void playSoundJalan(bool isWalk)
    {
        if (isWalk && !audioPlayer.isPlaying)
        {
            audioPlayer.loop = true;
            audioPlayer.clip = soundJalan;
            audioPlayer.Play();
        }
        else if (!isWalk && audioPlayer.isPlaying && audioPlayer.clip == soundJalan)
        {
            audioPlayer.loop = false;
            audioPlayer.Stop();
        }
    }
}
