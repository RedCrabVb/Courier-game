using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Parkour;

namespace Game.Player
{
    public class Mark
    {
        private GameObject mark;
        private GameObject parentObject;

        private float StepTimer = 0.7f;
        private float StepTimerDown;
        private bool isRmark = true;

        public Mark(GameObject parentObject, GameObject mark)
        {
            this.parentObject = parentObject;
            this.mark = mark;
        }

        public void advancing(Collision collision)
        {
            if (collision.gameObject != null && collision.gameObject.GetComponent<CustomTag>() && collision.gameObject.GetComponent<CustomTag>().tags.Contains("stable_object"))
            {
                StepTimerDown = StepTimerDown > 0 ? StepTimerDown - Time.deltaTime : 0;
                if (StepTimerDown == 0)
                {
                    isRmark = !isRmark;
                    StepTimerDown = StepTimer;
                    Vector3 position = collision.contacts[0].point - ((isRmark) ? new Vector3(0f, 0f, -0.4f) : new Vector3(0f, 0f, 0.4f));
                    Quaternion rootation = new Quaternion(0f, parentObject.transform.rotation.y, 0f, 1f);
                    GameObject.Instantiate(mark, position, rootation);
                }
            }
        }
    }

}