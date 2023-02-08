using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Boombox : MonoBehaviour {

    public bool playOnAwake;
    private bool playing;
    public bool Playing {
        get {
            return playing;
        }
        set {
            if (playing != value) {
                Animator animator = GetComponent<Animator>();
				AudioSource audioSource = GetComponent<AudioSource>();
                if (value == true) {
					animator.SetTrigger("Turn On");
					audioSource.Play();
                }
                else {
                    animator.SetTrigger("Turn Off");
                    audioSource.Pause();
                }
            }
            playing = value;
        }
    }

    private void Start() {
        if (playOnAwake == true) Playing = true; 
    }
}
