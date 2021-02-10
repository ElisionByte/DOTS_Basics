using UnityEngine;

public class TestColision : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        Debug.Log("Entered");
    }
    private void OnTriggerExit()
    {
        Debug.Log("Exit");
    }
}
