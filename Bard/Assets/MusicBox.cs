using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] MusicBoxSO musicBoxSO;
    // Start is called before the first frame update
    void Awake() {
        audioSource.time = musicBoxSO.playbackPosition;
    }

}
