using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered;
        PlayableDirector director;

        private void Awake()
        {
            alreadyTriggered = false;
            director = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.gameObject.tag == ("Player"))
            {
                director.Play();
                alreadyTriggered = true;
            }
        }
    }
}

