using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PanelVideoManager : MonoBehaviour
{
    public GameObject panelVideoEfek;
    public static bool playVideoTembak;
    public static bool playVideoMenang;
    public static bool playVideoKalah;
    public VideoPlayer videoEfek;
    public VideoClip videoTembak;
    public VideoClip videoMenang;
    public VideoClip videoKalah;
    public static bool miss;

    public GameObject panelMiss;

    public SpriteRenderer matVideo;
    public SpriteRenderer matBlur;
    Color colVid;
    bool selesai;

    // Start is called before the first frame update
    void Start()
    {
        panelVideoEfek.SetActive(false);
        selesai = true;
        colVid = matVideo.color;
        colVid.a = 0f;
        matVideo.color = colVid;
        matBlur.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playVideoTembak && !videoEfek.isPlaying)
        {
            //videoEfek.SetDirectAudioMute(0, false);
            PlayVideoEfek(videoTembak);
            playVideoTembak = false;
        }
        if (playVideoMenang && !videoEfek.isPlaying)
        {
            //videoEfek.SetDirectAudioMute(0, true);
            PlayVideoEfek(videoMenang);
            playVideoMenang = false;
        }
        if (playVideoKalah && !videoEfek.isPlaying)
        {
            
            PlayVideoEfek(videoKalah);
            playVideoKalah = false;
        }
        if (!selesai && videoEfek.time >= videoEfek.clip.length - 0.1f)
        {
            selesai = true;

            if (videoEfek.clip == videoKalah)
            {
                ActionManager.kondisiKalah = true;
            }
            if (videoEfek.clip == videoMenang)
            {
                ActionManager.kondisiMenang = true;
            }
            Time.timeScale = 1;
            videoEfek.prepareCompleted -= OnVideoPrepared;
            colVid.a = 0f;
            matVideo.color = colVid;
            matBlur.enabled = false;
            videoEfek.Stop();
            panelVideoEfek.SetActive(false) ;
            
        }
        if (miss)
        {
            StartCoroutine(MissProses());
            miss = false;
        }
    }

    void PlayVideoEfek(VideoClip clipEfek)
    {
        selesai = false;
        Time.timeScale = 0;
        panelVideoEfek.SetActive(true) ;
        videoEfek.clip = clipEfek;
        videoEfek.prepareCompleted += OnVideoPrepared;

        // Mulai proses persiapan video
        videoEfek.Prepare();

    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        videoEfek.Play();
        colVid.a = 1f;
        matVideo.color = colVid;
        matBlur.enabled = true;
    }
    IEnumerator MissProses()
    {
        Time.timeScale = 0;
        panelMiss.SetActive(true) ;
        yield return new WaitForSecondsRealtime(0.9f);

        panelMiss.SetActive(false);
        Time.timeScale = 1;
    }
}
