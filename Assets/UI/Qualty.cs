using UnityEngine;
using UnityEngine.UI;

public class Qualty : MonoBehaviour
{
    public Dropdown dropdown;
    public void checkdropdown()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
}