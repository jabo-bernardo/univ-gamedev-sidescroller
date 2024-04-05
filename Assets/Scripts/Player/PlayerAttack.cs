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
        if (Input.GetMouseButtonDown(0) && !isPrimaryCooldown && !GameManager.Instance.IsUserActionsDisabled())
        {
            
            StartCoroutine(PrimaryAttack());
        }
    }

    IEnumerator PrimaryAttack()
    {
        isPrimaryCooldown = true;
        primaryAttackObject.SetActive(true);

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 startingScreenPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        float initialAngle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        Vector3 angle = new Vector3(0, 0, initialAngle);

        // Check if mouse is on left side of player
        if (angle.z > 90 || angle.z < -90)
        {
            playerMovement.facingDirection = -1;
        }
        else
        {
            playerMovement.facingDirection = 1;
        }

        GameObject slashEffect = Instantiate(primarySlashObject, transform);
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f, -0.25f), transform.rotation);

        yield return new WaitForSeconds(primaryAttackCooldown);

        primaryAttackObject.SetActive(false);
        isPrimaryCooldown = false;
    }
}
