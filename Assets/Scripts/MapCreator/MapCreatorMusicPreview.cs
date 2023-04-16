using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreatorMusicPreview : MonoBehaviour
{
    [SerializeField] private MapCreatorDataSaver dataSaver;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip music;

    private float _currentTime;
    public float CurrentTime
    {
        get { return _currentTime; }
    }

    private void Start()
    {
        musicSlider.maxValue = music.length;
        audioSource.clip = music;
    }

    public void SetTime(Single value)
    {
        if (audioSource.isPlaying) return;
        
        _currentTime = value;

        List<MapCreatorKey> keys = dataSaver.KeyList;
        foreach (MapCreatorKey key in keys)
        {
            if (key.KeyData.SpawnTime + key.KeyData.LifeTime >= _currentTime)
            {
                if (_currentTime < key.KeyData.SpawnTime) key.gameObject.SetActive(false);
                else key.gameObject.SetActive(true);
            }
            else if (key.KeyData.LifeTime != 0) key.gameObject.SetActive(false);
        }
    }

    public void PlayPreview()
    {
        if (audioSource.isPlaying)
        {
            _currentTime = audioSource.time;

            audioSource.Stop();
            StopAllCoroutines();
        }
        else
        {
            audioSource.time = _currentTime;

            List<MapCreatorKey> keys = dataSaver.KeyList.FindAll(key => key.KeyData.SpawnTime + key.KeyData.LifeTime > _currentTime);
            foreach (MapCreatorKey key in keys)
            {
                float spawnTime = key.KeyData.SpawnTime - _currentTime;
                float lifeTime = spawnTime + key.KeyData.LifeTime;

                StartCoroutine(KeyCoroutine(key.gameObject, spawnTime, lifeTime));
            }

            audioSource.Play();
            StartCoroutine(MusicSliderPreviewCoroutine());
        }

        musicSlider.interactable = !musicSlider.interactable;
    }

    private IEnumerator KeyCoroutine(GameObject keyObject, float spawnDelay, float lifeTime)
    {
        if (spawnDelay > 0)
        {
            yield return new WaitForSeconds(spawnDelay);
            keyObject.SetActive(true);
        }
        yield return new WaitForSeconds(lifeTime);
        keyObject.SetActive(false);
    }

    private IEnumerator MusicSliderPreviewCoroutine()
    {
        while (true)
        {
            musicSlider.value = audioSource.time;
            yield return new WaitForEndOfFrame();
        }
    }
}
