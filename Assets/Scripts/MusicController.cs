using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip backgroundMusicStart;
    public AudioClip backgroundMusicLoop;
    public AudioClip backgroundMusicEnd;

    public GameObject youwontext;
    public GameObject youlosetext;
    public GameObject musicbutton;
    public GameObject welcomebutton;

    public bool startedtoplay;
    public bool endMusicSchedule;
    
    private Coroutine musicCoroutine;
    void Start()
    {
        backgroundMusicStart.LoadAudioData();
        backgroundMusicLoop.LoadAudioData();
        backgroundMusicEnd.LoadAudioData();
        
        // if welcomepage music disabled start as disabled
        if (!welcomebutton.GetComponent<WelcomeMusicButtonBehaviour>().music)
            musicbutton.GetComponent<PauseMusicButtonBehaviour>().music = false;
    }
    void Update()
    {
        
        // check if game ended
        if (youlosetext.activeInHierarchy || youwontext.activeInHierarchy)
            endMusicSchedule = true;
        
        else if (gameObject.activeSelf && !startedtoplay)
        {
            musicSource.Stop();
            musicCoroutine = StartCoroutine(ManageBackgroundMusic());
            startedtoplay = true;
        }
        
        if(Time.timeScale == 1 && musicbutton.GetComponent<PauseMusicButtonBehaviour>().music)
            musicSource.UnPause();
        
        else
            musicSource.Pause();
    }
    IEnumerator ManageBackgroundMusic()
    {
        yield return new WaitForSeconds(3f); // 3 seconds delay for start

        // intro
        musicSource.clip = backgroundMusicStart;
        musicSource.Play();
        
        yield return  WaitDependingTimeScale(backgroundMusicStart.length);

        // loop
        musicSource.clip = backgroundMusicLoop;
        musicSource.loop = true;
        musicSource.Play();
        
        // check if game ended
        while (!endMusicSchedule)
        {
            yield return null;
        }

        // end
        musicSource.loop = false;
        musicSource.Stop();

        double endPlayTime = AudioSettings.dspTime + 0.1; 
        
        musicSource.clip = backgroundMusicEnd;
        musicSource.PlayScheduled(endPlayTime);
    }

    public void ResetMusic() // for playing the game again
    {
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }

        musicSource.Stop();
        musicSource.clip = null; 
        startedtoplay = false;
        endMusicSchedule = false;
    }

    IEnumerator WaitDependingTimeScale(float time)
    {
        while (time > 0)
        {
            if (Time.timeScale == 1f && musicbutton.GetComponent<PauseMusicButtonBehaviour>().music)
                time -= Time.deltaTime;

            yield return null;
        }
    }
}
