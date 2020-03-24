using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;

    private void Start() => anim = GetComponent<Animator>();

    public void Open() => anim.SetBool("open", true);

    public void Close () => anim.SetBool("open", false);
}