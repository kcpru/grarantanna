using UnityEngine;

public class OnDestroyActivation : MonoBehaviour
{
    [SerializeField] private GameObject triggerToActivate;

    private void OnDestroy() => triggerToActivate.SetActive(true);
}
