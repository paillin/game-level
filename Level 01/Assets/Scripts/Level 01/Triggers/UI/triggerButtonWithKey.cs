 using UnityEngine;
 using UnityEngine.UI;
 
 public class triggerButtonWithKey : MonoBehaviour
{
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
//apply this script to your button and choose what button you want to trigger.//