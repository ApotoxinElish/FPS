using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SoundEffectControl : MonoBehaviour
{
    // Start is called before the first frame update
    private static AudioSource BGM;
    public Dictionary<int,AudioClip[]> SoundSource = new Dictionary<int, AudioClip[]>();
    public AudioClip[] Source1;
    public AudioClip[] Source2;
    public AudioClip[] Source3;
    void Awake()
    {
        SoundSource.Add(0,Source1);
        SoundSource.Add(1,Source2);
        SoundSource.Add(2,Source3);
        BGM = gameObject.GetComponent<AudioSource>();
    }

    public void debug()
    {
        Debug.Log("show access");
    }

    public void InitialBGM(AudioSource audio)
    {
        BGM = audio;
    }
    public static float GetVolume()
    {
        return BGM.volume;
    }

    public bool SoundIsplaying()
    {
        return BGM.isPlaying;
    }
    

    public static void ChangeVolume(float value)
    {
        BGM.volume = value;
    }

    public void PlayEffect(int sourceindex , int clipindex)
    {
        if (sourceindex>= SoundSource.Count||clipindex >= SoundSource[sourceindex].Length  )
        { return;}
        Debug.Log("show");
        BGM.clip = SoundSource[sourceindex][clipindex];
        BGM.Play();
    }

    public void StopEffect()
    {
        BGM.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
