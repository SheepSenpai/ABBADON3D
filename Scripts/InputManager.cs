using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private WeaponSway weaponSway;
    private WeaponMotor weaponmotor;
    private SpawnEntities spawner;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        weaponmotor = GetComponentInChildren<WeaponMotor>();
        weaponSway = GetComponentInChildren<WeaponSway>();
        spawner = GetComponent<SpawnEntities>();
        onFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        onFoot.Shoot.performed += ctx => weaponmotor.Shoot();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Reload.performed += ctx => weaponmotor.StartReload();
        onFoot.SpawnEntities.performed += ctx => spawner.SpawnTargets();
        onFoot.Quit.performed += ctx => motor.Quit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector3>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        weaponSway.ProcessWeaponSway(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
