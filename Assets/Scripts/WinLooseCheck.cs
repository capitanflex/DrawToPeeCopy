using UnityEngine;



public class WinLooseCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            GameEvents.OnLevelFailed?.Invoke();
        }
        
        if (other.gameObject.CompareTag("Finish"))
        {
            GameEvents.OnFinished?.Invoke();
        }
    }

    
}
