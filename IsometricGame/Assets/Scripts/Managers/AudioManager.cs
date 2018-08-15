using UnityEngine;

[System.Serializable]
public class Sound
{
    public string fileName;
    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume = 0.5f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1.0f;
    [Range(0.0f, 0.5f)]
    public float volumeVarience = 0.1f;
    [Range(0.0f, 0.5f)]
    public float pitchVarience = 0.1f;
    AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volumeVarience / 2, volumeVarience /2));
        source.pitch = pitch * (1 + Random.Range(-pitchVarience / 2, pitchVarience / 2));
        source.Play();
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Too many AudioManagers!");
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _gameObject = new GameObject("Sound_" + i + "_" + sounds[i].fileName);
            _gameObject.transform.SetParent(this.transform);
            sounds[i].SetSource (_gameObject.AddComponent<AudioSource>());

        }
    }

    public void PlaySound(string _fileName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].fileName == _fileName)
            {
                sounds[i].Play();
                return;
            }
        }
    }
}
