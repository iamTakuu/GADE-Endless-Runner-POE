using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Music[] musics;
    private string currentMusicID;
    private void Awake()
    {
        if (Instance !=null && Instance!=this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);

        foreach (Music song in musics)
        {
            song.audioSource = gameObject.AddComponent<AudioSource>();
            song.audioSource.clip = song.musicClip;
            
        }
        SwitchMusic("Menu");
        //currentMusicID = "Menu";
    }

    private void OnEnable()
    {
        EventsManager.Instance.BMGSwitch += SwitchMusic;
    }

    private void OnDisable()
    {
        EventsManager.Instance.BMGSwitch -= SwitchMusic;

    }

    private void SwitchMusic(string musicID)
    {
        //Stop Current Music
        foreach (var song in musics)
        {
            if (song.clipID == currentMusicID)
            {
                song.audioSource.Stop();
                
                break;
            }
        }
        //Change the ClipID
        currentMusicID = musicID;
        
        //Play New Music
        foreach (var song in musics)
        {
            if (song.clipID == currentMusicID)
            {
                if (!song.audioSource.isPlaying)
                {
                    song.audioSource.loop = true;
                    song.audioSource.Play();
                    break; 
                }
                
            }
        }
    }

    public void AdjustMasterVolume(float value)
    {
        AudioListener.volume = value; 
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
}
