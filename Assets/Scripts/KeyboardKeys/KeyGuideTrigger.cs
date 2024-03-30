using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuideTrigger : MonoBehaviour
{
    public KeyCode keyToTrigger;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(keyToTrigger))
        {
            anim.SetBool("isClicking", true);
            return;
        }

        anim.SetBool("isClicking", false);
    }
}
