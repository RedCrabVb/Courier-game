using UnityEngine;
using System.Collections.Generic;

namespace Game.Parkour
{
    public class CustomTag : MonoBehaviour
    {
        public bool IsEnabled = true;

        [SerializeField]
        public List<string> tags = new List<string>();
        //Planting - не нужно нажимать пробел
        //stable_object - на объекте можно стоять
        //Wall - можно активно взаимодействовать 
        //Ground, Stone - звуки
        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }
    }
}
