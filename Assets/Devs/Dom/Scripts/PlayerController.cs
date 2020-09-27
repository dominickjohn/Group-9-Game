﻿using System.Security.Cryptography;
using UnityEngine;

/*
Controls the player input and moves character on screen
*/

// this is cade, testing the github usage

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Animator animator;    // Where to send animation data to
    public float moveSpeed = 5f;    // How fast player will move

    [SerializeField]
    public Weapon[] weapons = new Weapon[8];
    
    private CharacterController _characterController;
    public Vector3 facingDirection = Vector3.back;
    private Vector3 _facingDirectionRaw = Vector3.back;
    private Vector3 _moveVector;
    private float _speedMod = 1f;

    public int currentWeapon = 0;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    
    private void LateUpdate()
    {
        // Get player input data
        _moveVector.x = Input.GetAxis("Horizontal");
        _moveVector.z = Input.GetAxis("Vertical");

        if (currentWeapon != 0)
        {
            //_facingDirectionRaw.x = Input.GetAxis("HorizontalLook");
            //_facingDirectionRaw.z = Input.GetAxis("VerticalLook");

            _facingDirectionRaw.x = ((Input.mousePosition.x - (Screen.width * 0.5f)) / Screen.width);
            _facingDirectionRaw.z= ((Input.mousePosition.y - (Screen.height * 0.5f)) / Screen.height);
        }
        else
        {
            _facingDirectionRaw = _moveVector;
        }

        _speedMod = Input.GetKey(KeyCode.LeftShift) ?  0.5f : 1f;
        
        // Prevent movement from exceeding a factor of 1, useful for diagonals.
        _moveVector = Vector3.ClampMagnitude(_moveVector, _speedMod);

        // If moving, change the facing direction
        if (_facingDirectionRaw != Vector3.zero)
        {
            facingDirection = _facingDirectionRaw.normalized;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            // Half speed when attacking or aiming
            _speedMod *= 0.5f;
        }
        
        if(Input.GetKeyDown(KeyCode.Tilde) || Input.GetKeyDown(KeyCode.BackQuote))
        {
            EquipWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipWeapon(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EquipWeapon(6);
        }

        // Update the character controller to perform the move
        _characterController.SimpleMove(_moveVector * moveSpeed);
        transform.forward = facingDirection;


        if (Input.GetKeyDown(KeyCode.E))
        {
            UseObjects();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapons[currentWeapon]?.OnWeaponDown();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weapons[currentWeapon]?.OnWeaponRelease();
        }
        
        // Send animator the movement data for visuals
        UpdateAnimator();
    }

    private void EquipWeapon(int weaponSlot)
    {
        UnequipAllWeapons();
        currentWeapon = weaponSlot;
        weapons[currentWeapon]?.gameObject.SetActive(true);
    }

    private void UnequipAllWeapons()
    {
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i]?.gameObject.SetActive(false);
        }

        currentWeapon = 0;
    }
    
    private void UseObjects()
    {
        var selectedObjects = Physics.SphereCastAll(transform.position + facingDirection * 0.2f, 0.35f, facingDirection, 0.5f);
        foreach (var hit in selectedObjects)
        {
            if (hit.transform.gameObject.GetComponent<Interactable>() != null)
            {
                hit.transform.gameObject.GetComponent<Interactable>().OnUse();
                break;
            }
        }
    }

    
    private void UpdateAnimator()
    {
        animator.SetFloat("x", facingDirection.x);
        animator.SetFloat("y", facingDirection.z);
        animator.SetFloat("speed", _moveVector.magnitude);
        animator.GetComponent<SpriteRenderer>().flipX = facingDirection.x >= 0;
        animator.transform.eulerAngles = Vector3.zero;
    }
}
