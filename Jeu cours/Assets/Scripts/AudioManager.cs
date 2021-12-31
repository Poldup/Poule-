using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup soundMixerGroup;

    public static AudioManager Instance;
    public AudioClip flap1;
    public AudioClip flap2;
    public AudioClip bigflap;
    public AudioClip hurt1;
    public AudioClip hurt2;
    public List<AudioClip> walk;


    public AudioSource pouleAudio;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip clip, float vol)
    {
        GameObject TempAudio = new GameObject("TempAudio");
        AudioSource source = TempAudio.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = soundMixerGroup;
        source.volume = vol;
        source.Play();
        Destroy(TempAudio, clip.length);
    }
    

    void Flap1()
    {
        pouleAudio.clip = flap1;
        pouleAudio.Play();
    }

    void Flap2()
    {
        pouleAudio.clip = flap2;
        pouleAudio.Play();
    }

    void BigFlap()
    {
        pouleAudio.clip = bigflap;
        pouleAudio.Play();
    }

    void Walk()
    {
        int num = Random.Range(0, 3);
        pouleAudio.clip = walk[num];
        pouleAudio.Play();
    }
}
