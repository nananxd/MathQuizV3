using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip soundToPlay;
    public AudioSource audioSource;

    [Range(0, 1f)]
    public float volume;
    [Range(0, 1f)]
    public float pitch;
    public bool isLoop;
    public bool playOnAwake;
}
public class SoundManager : MonoBehaviour
{
    [NonReorderable]
    public List<Sound> sounds;
    [SerializeField] private float fadeSoundDuration;
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Setup();
    }
    

    private void Setup()
    {
        foreach (var item in sounds)
        {
            item.audioSource = gameObject.AddComponent<AudioSource>();
            item.audioSource.clip = item.soundToPlay;
            item.audioSource.volume = item.volume;
            item.audioSource.pitch = item.pitch;
            item.audioSource.loop = item.isLoop;
            item.audioSource.playOnAwake = item.playOnAwake;

        }
    }

    public void PlaySound(string soundName)
    {
        var chosenSound =sounds.Find(s => s.name == soundName);
        if (chosenSound == null)
        {
            return;
        }
        //chosenSound.audioSource.volume = chosenSound.volume;
        //chosenSound.audioSource.pitch = chosenSound.pitch;
        //chosenSound.audioSource.loop = chosenSound.isLoop;
        //chosenSound.audioSource.playOnAwake = chosenSound.playOnAwake;

        
        chosenSound.audioSource.Play();
    }

    public void PlaySoundBG(string soundName)
    {
        var chosenSound = sounds.Find(s => s.name == soundName);
        if (chosenSound == null)
        {
            return;
        }
        chosenSound.audioSource.volume = chosenSound.volume;
        chosenSound.audioSource.pitch = chosenSound.pitch;
        chosenSound.audioSource.loop = chosenSound.isLoop;
        chosenSound.audioSource.playOnAwake = chosenSound.playOnAwake;


        chosenSound.audioSource.Play();
    }

    public void StopSound(string soundName)
    {
        var chosenSound = sounds.Find(s => s.name == soundName);
        if (chosenSound == null)
            return;

            StartCoroutine(StopSoundCoroutine(chosenSound));
    }

    public void StopSoundImidiate(string soundName)
    {
        var chosenSound = sounds.Find(s => s.name == soundName);
        if (chosenSound == null)
            return;
        chosenSound.volume = 0;
    }

    private IEnumerator StopSoundCoroutine(Sound sound)
    {
        float startVolume = sound.audioSource.volume;
        while (sound.audioSource.volume > 0)
        {
            sound.audioSource.volume -= startVolume * Time.deltaTime / fadeSoundDuration;
            yield return null;
        }

        sound.audioSource.volume = 0;
    }
}
