using UnityEngine;

public class BoomboxDemo : MonoBehaviour {

    public Boombox radio;

	void Update () {
        if (Input.GetKeyUp(KeyCode.E)) {
            radio.Playing = !radio.Playing;
        }	
	}
}
