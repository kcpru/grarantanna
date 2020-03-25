using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Collider2D swordCol;
    public bool canPlayerHit = true;
    private Animator anim;
    private bool canHit = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && canHit && canPlayerHit)
        {
            Hit();
        }
    }

    private void Hit ()
    {
        canHit = false;
        anim.SetTrigger("hit");
        SoundsManager.CurrentManager.PlaySound(SoundsManager.ATTACK_SOUND);
    }

    public void ActivateCollider ()
    {
        swordCol.enabled = true;
    }

    public void DeactivateCollider ()
    {
        swordCol.enabled = false;
    }

    public void EndOfHitMotion ()
    {
        canHit = true;
    }
}
