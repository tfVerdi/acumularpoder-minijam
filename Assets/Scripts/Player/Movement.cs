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
    public InputController inputController;
    protected Rigidbody2D rigidbody_2D;
    public Vector2 movementForce;
    bool movementKeyPressed = false;
    private const float defaultSpeed = 5f;
    public float speed = defaultSpeed;
    public bool boosted = false;
    
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
    }

    public IEnumerator boostSpeed(float multiplier, float seconds) {
        speed = defaultSpeed * multiplier;
        boosted = true;
        yield return new WaitForSeconds(seconds); 
        speed = defaultSpeed;
        boosted = false;
    }

    public IEnumerator boostSpeed(float multiplier) {
        float seconds = 1f;
        speed = defaultSpeed * multiplier;
        boosted = true;
        yield return new WaitForSeconds(seconds); 
        speed = defaultSpeed;
        boosted = false;
    }
}
