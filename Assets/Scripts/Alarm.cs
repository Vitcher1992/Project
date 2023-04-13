using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _alarm;

    private AudioSource _audioSource;
    private Coroutine _increaseSoundJob;
    private Coroutine _decreaseVolumeJob;

    private readonly float _maxVolume = 1;
    private readonly float _step = 0.1f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _alarm;
        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            PlaySound();
            _increaseSoundJob = StartCoroutine(IncreaseSound());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(_increaseSoundJob);
            _decreaseVolumeJob = StartCoroutine(DecreaseVolume());
        }
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private IEnumerator IncreaseSound()
    {
        for (float i = 0; i != _maxVolume; i += _step)
        {
            _audioSource.volume = i;

            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator DecreaseVolume()
    {
        for (float i = _audioSource.volume; i != 0; i -= _step)
        {
            _audioSource.volume = i;

            if (_audioSource.volume == 0)
            {
                _audioSource.Stop();
                StopCoroutine(_decreaseVolumeJob);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}