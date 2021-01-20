using UnityEngine;

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
        if (other.tag != "Player")
            return;
       for(int i = 0; i < AllSavePoint.Length; i++)
       {
            if (AllSavePoint[i].isActive)
            {
                eye.SetTrigger("Dead");
                other.gameObject.transform.position = AllSavePoint[i].getSpawnPoint().position;
            }
       }
    }
}
