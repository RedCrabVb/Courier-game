using System.Collections;
using UnityEngine;
using Game.Player;

namespace Game.save
{
    public class Dead : MonoBehaviour
    {
        private SavePoint[] AllSavePoint;
        public Animator eye;
        private void Start()
        {
            AllSavePoint = FindObjectsOfType<SavePoint>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<RigidbodyFirstPersonController>())
                return;
            for (int i = 0; i < AllSavePoint.Length; i++)
            {
                if (AllSavePoint[i].isActive)
                {
                    StartCoroutine(dead(other, AllSavePoint[i].getSpawnPoint().position));
                }
            }
        }
        IEnumerator dead(Collider player, Vector3 respawnPoint)
        {
            eye.SetTrigger("Dead");
            player.gameObject.transform.position = respawnPoint;
            player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            player.gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
            yield return new WaitForSeconds(1.5f);
            player.gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        }
    }
}