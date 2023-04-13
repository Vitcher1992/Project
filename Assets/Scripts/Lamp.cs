using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_audioSource.volume > 0)
            _animator.SetTrigger("Alarm");
    }
}