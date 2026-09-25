using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
public class Movement : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    private Rigidbody2D rigidbody_2D;
    public Vector2 movementForce;
    private bool movementKeyPressed = false;
    private const float defaultSpeed = 8f;
    [SerializeField] public float defaultBoostTime = 1.5f;
    public float speed = defaultSpeed;
    public float boostTimeRemaining;
    
    void Start() {
        rigidbody_2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        movementKeyPressed = false;
        movementForce = Vector2.zero;
        if(inputController.isWPressed) {
            movementKeyPressed = true;
            movementForce += Vector2.up;
        } else if(inputController.isSPressed) {
            movementKeyPressed = true;
            movementForce += Vector2.down;
        }

        if(inputController.isAPressed) {
            movementKeyPressed = true;
            movementForce += Vector2.left;
        } else if(inputController.isDPressed) {
            movementKeyPressed = true;
            movementForce += Vector2.right;
        }

        if(!movementKeyPressed) {
            rigidbody_2D.velocity = rigidbody_2D.velocity * 0.8f;
        }

        movementForce = (Vector2)Vector3.Normalize(movementForce) * 0.25f;
        
        if(rigidbody_2D.velocity.magnitude < speed) {
            rigidbody_2D.AddForce(movementForce, ForceMode2D.Impulse);
        } else {
            rigidbody_2D.velocity = rigidbody_2D.velocity.normalized * speed;
        }

        if(boostTimeRemaining > 0f) {
            boostTimeRemaining -= Time.deltaTime;
        } else {
            resetSpeed();
        }
    }

    public void boostSpeedByAmplifying(float multiplier, float time) {
        speed = defaultSpeed * multiplier;
        boostTimeRemaining = time;
    }
    
    public void boostSpeedByAdding(float addition, float time) {
        speed = defaultSpeed + addition;
        boostTimeRemaining = time;
    }

    public void boostSpeedByAmplifying(float multiplier) {
        speed = defaultSpeed * multiplier;
        boostTimeRemaining = defaultBoostTime;
    }

    public void boostSpeedByAdding(float addition) {
        speed = defaultSpeed + addition;
        boostTimeRemaining = defaultBoostTime;
    }

    public void resetSpeed() {
        speed = defaultSpeed;
    }
}
