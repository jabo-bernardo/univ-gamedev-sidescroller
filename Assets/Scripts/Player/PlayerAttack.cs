using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject primaryAttackObject;
    // public GameObject secondaryAttackObject;

    public float primaryAttackCooldown = 0.25f;
    // public float secondaryAttackCooldown = 2f;

    [Header("Effects")]
    public GameObject primarySlashObject;
    public Animator primaryAttackEffect;
    public int primaryAttackEffectVariations = 3;

    private bool isPrimaryCooldown;
    // private bool isSecondaryCooldown;

    private PlayerMovement playerMovement;
    public LookAtMouse lookAtMouse;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Check if mouse is on left side of player
        if (lookAtMouse.angle.z > 90 || lookAtMouse.angle.z < -90)
        {
            playerMovement.currentDirection = -1;
            playerMovement.sr.flipX = true;
        }
        else
        {
            playerMovement.currentDirection = 1;
            playerMovement.sr.flipX = false;
        }
        if (Input.GetMouseButtonDown(0) && !isPrimaryCooldown && !GameManager.Instance.IsUserActionsDisabled())
        {
            
            StartCoroutine(PrimaryAttack());
        }
    }

    IEnumerator PrimaryAttack()
    {
        isPrimaryCooldown = true;
        primaryAttackObject.SetActive(true);

        GameObject slashEffect = Instantiate(primarySlashObject, transform);
        slashEffect.GetComponent<SpriteRenderer>().flipX = playerMovement.currentDirection == 1 ? false : true;
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f * playerMovement.currentDirection, -0.25f), transform.rotation);

        yield return new WaitForSeconds(primaryAttackCooldown);

        primaryAttackObject.SetActive(false);
        isPrimaryCooldown = false;
    }
}
