using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 3;

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.StopAllCoroutines();
        AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeBGMOut(_fadeDuration));
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.Instance.StopAllCoroutines();
        AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeBGMIn(_fadeDuration));
    }
}
