using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector director;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            director.played += DisableControl;
            director.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
        }
    }
}

