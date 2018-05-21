using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private float _volumeMin = 1.0f;
    [SerializeField]
    private float _volumeMax = 1.0f;

    [SerializeField]
    private float _pitchMin = 1.0f;
    [SerializeField]
    private float _pitchMax = 1.0f;

    [SerializeField]
    private bool _playOnStart = false;

    public void Start()
    {
        if (_playOnStart)
            Play(_source.loop);
    }

    public void Play(bool looped)
    {
        _source.loop = looped;
        _source.volume = Random.Range(_volumeMin, _volumeMax);
        _source.pitch = Random.Range(_pitchMin, _pitchMax);

        _source.Play();
    }

    private void Update()
    {
        if (!_source.isPlaying)
            Destroy(gameObject);
    }

    public static MyAudio Create(MyAudio audioPrefab, Vector3 position)
    {
        return Instantiate(audioPrefab, position, Quaternion.identity);
    }
}
