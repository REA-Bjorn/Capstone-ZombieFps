using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAudio : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> mainMenuMusic;
    [SerializeField] private List<AudioClip> level1Music;
    [SerializeField] private List<AudioClip> level2Music;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case GameManager.KEY_MAINMENU:
                StartCoroutine(PlayMainMenuMusic());
                break;
            case GameManager.KEY_LEVEL1:
                StartCoroutine(PlayLevel1Music());
                break;
            case GameManager.KEY_LEVEL2:
                StartCoroutine(PlayLevel2Music());
                break;
            default:
                break;
        }
    }

    private IEnumerator PlayMainMenuMusic()
    {
        int idx = Random.Range(0, mainMenuMusic.Count);
        musicSource.clip = mainMenuMusic[idx];
        musicSource.Play();
        yield return new WaitForSeconds(mainMenuMusic[idx].length);
        StartCoroutine(PlayMainMenuMusic());
    }

    private IEnumerator PlayLevel1Music()
    {
        int idx = Random.Range(0, level1Music.Count);
        musicSource.clip = level1Music[idx];
        musicSource.Play();
        yield return new WaitForSeconds(level1Music[idx].length);
        StartCoroutine(PlayLevel1Music());
    }

    private IEnumerator PlayLevel2Music()
    {
        int idx = Random.Range(0, level2Music.Count);
        musicSource.clip = level2Music[idx];
        musicSource.Play();
        yield return new WaitForSeconds(level2Music[idx].length);
        StartCoroutine(PlayLevel2Music());
    }
}
