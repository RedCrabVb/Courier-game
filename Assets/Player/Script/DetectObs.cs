using UnityEngine;
using System.Collections.Generic;

namespace Game.Parkour
{
    public class DetectObs : MonoBehaviour
    {
        public string ObjectTagName = "";
        public bool Obstruction;
        public List<string> tags;
        private GameObject Object;
        private Collider colnow;

        void OnTriggerStay(Collider col)
        {
            bool is_object_find = (ObjectTagName != "" && !Obstruction);
            if (is_object_find)
            {
                bool does_the_object_have_the_correct_tag = col.GetComponent<CustomTag>() && col.GetComponent<CustomTag>().HasTag(ObjectTagName) && col.GetComponent<CustomTag>().IsEnabled;
                bool is_correct_object = col != null;
                if (is_correct_object && does_the_object_have_the_correct_tag) // checks if the object has the right tag
                {
                    Obstruction = true;
                    Object = col.gameObject;
                    colnow = col;
                    tags = col.GetComponent<CustomTag>().tags;
                }
            }
        }


        private void Update()
        {
            if (Object != null && (!colnow.enabled || !Object.activeInHierarchy))
            {
                Obstruction = false;
            }
        }


        void OnTriggerExit(Collider col)
        {
            bool is_not_getting_out_of_an_obstacle = (col == colnow);
            if (is_not_getting_out_of_an_obstacle)
                Obstruction = false;
        }

    }
}