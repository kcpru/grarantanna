using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TutorialBrain.CurrentTutorial.DisplayText();
            Destroy(gameObject);
        }
    }
}
