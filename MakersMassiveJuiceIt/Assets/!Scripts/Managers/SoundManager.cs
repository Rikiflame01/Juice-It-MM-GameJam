using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Music Settings")]
    public AudioSource musicSource;
    public AudioSource queuedMusicSource;
    public AudioClip[] musicPlaylist;

    public int currentTrackIndex = 0;
    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float fadeDuration = 2f;

    [Header("SFX Settings")]
    public AudioSource sfxSource;
    public List<SFX> sfxList;

    private Coroutine volumeCoroutine;
    private Coroutine fadeCoroutine;
    private Coroutine trackMonitorCoroutine;
    private bool isMusicPlaying = false;
    private bool isFading = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeSFX();
    }

    void Start()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("Music Source is not assigned.");
            return;
        }

        if (queuedMusicSource == null)
        {
            Debug.LogWarning("Queued Music Source is not assigned.");
            return;
        }

        if (musicPlaylist == null || musicPlaylist.Length == 0)
        {
            Debug.LogWarning("Music Playlist is empty.");
            return;
        }

        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.volume = 0f;
        musicSource.Play();
    }

    #region SFX

    [System.Serializable]
    public class SFX
    {
        public string name;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource source;
    }

    void InitializeSFX()
    {
        foreach (var sfx in sfxList)
        {
            if (sfx.clip == null || string.IsNullOrEmpty(sfx.name))
            {
                Debug.LogWarning("SFX entry is missing a name or AudioClip.");
                continue;
            }

            GameObject sfxObject = new GameObject($"SFX_{sfx.name}");
            sfxObject.transform.parent = this.transform;

            sfx.source = sfxObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.playOnAwake = false;
            sfx.source.spatialBlend = 0f;

        }
    }

    public void PlaySFX(string sfxName)
    {
        SFX sfx = sfxList.Find(s => s.name.Equals(sfxName, System.StringComparison.OrdinalIgnoreCase));
        if (sfx != null && sfx.source != null)
        {
            sfx.source.Play();
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found or AudioSource is missing.");
        }
    }

    public void PlaySFX(string sfxName, float volume)
    {
        SFX sfx = sfxList.Find(s => s.name.Equals(sfxName, System.StringComparison.OrdinalIgnoreCase));
        if (sfx != null && sfx.source != null)
        {
            sfx.source.volume = Mathf.Clamp01(volume);
            sfx.source.Play();
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found or AudioSource is missing.");
        }
    }


    public void PlaySFX(string sfxName, Vector3 position)
    {
        SFX sfx = sfxList.Find(s => s.name.Equals(sfxName, System.StringComparison.OrdinalIgnoreCase));
        if (sfx != null && sfx.clip != null)
        {
            AudioSource.PlayClipAtPoint(sfx.clip, position);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found or AudioClip is missing.");
        }
    }

    public void PlayRandomSFX(Vector3 position)
    {
        SFX sfx = sfxList[Random.Range(0, 3)];
        if (sfx != null && sfx.clip != null)
        {
            AudioSource.PlayClipAtPoint(sfx.clip, position);
        }
    }

    #endregion
}