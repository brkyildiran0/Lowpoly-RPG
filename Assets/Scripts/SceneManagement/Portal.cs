using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int targetSceneIndex = -1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && targetSceneIndex != -1)
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
    }
}
