using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum PlayerId { PlayerOne, PlayerTwo, PlayerThree, PlayerFour }

public class DB_CharacterController : MonoBehaviour {
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 200f;

    [SerializeField]
    private float throwDelay = 0.5f;

    [SerializeField]
    private float throwForce = 50f;

    [SerializeField]
    private PlayerId pid = PlayerId.PlayerOne;

    //Store axis names for multiplayer
    private string[] axisNames;

    private Vector3 lastMoveDirection;

    //Temp
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private Transform handTransform;

    private void Start() {
        if (!this.GetComponent<Animator>()) {
            Debug.LogError("Animator is required on a Character");
        }

        axisNames = new string[3];

        //Get Axises
        switch (pid) {
            case PlayerId.PlayerOne:
                axisNames[0] = "PlayerOneHorizontal";
                axisNames[1] = "PlayerOneVertical";
                axisNames[2] = "PlayerOneFire";
                break;
            case PlayerId.PlayerTwo:
                axisNames[0] = "PlayerTwoHorizontal";
                axisNames[1] = "PlayerTwoVertical";
                axisNames[2] = "PlayerTwoFire";
                break;
            case PlayerId.PlayerThree:
                axisNames[0] = "PlayerThreeHorizontal";
                axisNames[1] = "PlayerThreeVertical";
                axisNames[2] = "PlayerThreeFire";
                break;
            case PlayerId.PlayerFour:
                axisNames[0] = "PlayerFourHorizontal";
                axisNames[1] = "PlayerFourVertical";
                axisNames[2] = "PlayerFourFire";
                break;
            default:
                break;
        }
    }

    private void Update() {
        HandleMovement(Input.GetAxis(axisNames[0]), Input.GetAxis(axisNames[1]));

        if (Input.GetButtonDown(axisNames[2])) {
            StartCoroutine("ThrowBall");
        }
    }

    private void HandleMovement(float axisX, float axisY) {
        Vector3 moveDirection = new Vector3(axisX , 0, -axisY);
        moveDirection.Normalize();
        this.transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        
        if (moveDirection != Vector3.zero) {
            lastMoveDirection = moveDirection;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            this.GetComponent<Animator>().SetBool("isWalking", true);
        } else this.GetComponent<Animator>().SetBool("isWalking", false);
    }

    private IEnumerator ThrowBall() {
        if (lastMoveDirection.x >= 0) this.GetComponent<Animator>().SetTrigger("onThrowRight");
        else this.GetComponent<Animator>().SetTrigger("onThrowLeft");

        yield return new WaitForSeconds(throwDelay);

        GameObject b = GameObject.Instantiate(ballPrefab, handTransform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().AddForce(new Vector3(lastMoveDirection.x * throwForce, 0, lastMoveDirection.z * throwForce));
        Physics.IgnoreCollision(b.GetComponent<Collider>(), GetComponent<Collider>());

    }
}
