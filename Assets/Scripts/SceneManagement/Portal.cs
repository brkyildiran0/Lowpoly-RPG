using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    enum PortalID
    {
        A, B, C, D
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] int targetSceneIndex = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] PortalID portalID;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && targetSceneIndex != -1)
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(targetSceneIndex);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            GameObject.Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.portalID != portalID) continue;
                return portal;
            }
            return null;
        }
    }
}
