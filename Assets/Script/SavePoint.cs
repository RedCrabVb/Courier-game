using UnityEngine;
using Game.Player;

namespace Game.save
{
    public class SavePoint : MonoBehaviour
    {
        public bool isActive;
        [SerializeField] private Transform SpawnPoint;
        [SerializeField] private GameObject LightBulb;
        private SavePoint[] AllSavePoint;

        private void Start()
        {
            AllSavePoint = FindObjectsOfType<SavePoint>();
        }

        public void EnterOtherObj()
        {
            isActive = false;
            LightBulb.SetActive(false);
        }

        public Transform getSpawnPoint()
        {
            return SpawnPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<RigidbodyFirstPersonController>())
                return;
            for (int i = 0; i < AllSavePoint.Length; i++)
            {
                if (AllSavePoint[i].isActive)
                {
                    AllSavePoint[i].isActive = false;
                    AllSavePoint[i].EnterOtherObj();
                }
            }
            isActive = true;
            LightBulb.SetActive(isActive);
        }
    }
}