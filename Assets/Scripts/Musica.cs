using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Musica : MonoBehaviour
{

    private AudioSource _audioSource;

    private static Musica instance;
    public static Musica GetInstance() {
        return instance;
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
}
    
    
    