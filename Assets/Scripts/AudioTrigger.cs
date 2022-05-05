using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundToPlay;
    public bool remoteSoundEffect; //Check this in Inspector if you want this played elsewhere
    public GameObject remoteSFXLocation;
    public AudioSource sourceToPlay; // THIS NEEDS TO BE AN AUDIOSOURCE COMPONENT IN YOUR LEVEL! Maybe 'SFXSytem'
    public float volume;

    private void Start()
    {
        if (remoteSFXLocation == null)
        {
            remoteSoundEffect = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (remoteSoundEffect)
            {
                //PlayClipAtPoint(soundToPlay, remoteSFXLocation.gameObject.transform.position); // THIS PLAYS IT AT THE ITEM PICKUP LOCATION
            }
            else
            {
                sourceToPlay.PlayOneShot(soundToPlay, volume); //THIS PLAYS IT AT THE PLAYER LOCATION
            }
            // If this is a pickup, you'd TOTALLY also do something here that shows the player has picked it up - Maybe flag a boolean in another script, etc
            Destroy(gameObject); //IF YOU KEEP THIS, THE ITEM WILL BE REMOVED ONCE IT IS RUN OVER!
            // IF YOU WANT THIS TO JUST BE A SFX TRIGGER ZONE, comment out the DESTROY line above, and make the collider an invisible trigger.
        }
    }

}
