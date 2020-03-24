using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Collider2D swordCol;
    private Animator anim;
    private bool canHit = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canHit)
        {
            Hit();
        }
    }

    private void Hit ()
    {
        canHit = false;
        anim.SetTrigger("hit");
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
