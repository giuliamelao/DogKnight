using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptDog : MonoBehaviour
{
    private Rigidbody rbd;
    public float vel = 15;
    private Quaternion rotIni;
    public float velRotY;
    private float contY = 0;
    public float dist = 100f;
    public float jump = 500f;
    public bool onGround;
    private Animator anim;
    private bool isMoving;


    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        rotIni = transform.localRotation;

        velRotY = 300;
        onGround = true;
    }

    void Update()
    {
        float frente = Input.GetAxis("Vertical");
        float lado = Input.GetAxis("Horizontal");

        Vector3 movimento = new Vector3(lado, 0, frente);
        rbd.velocity = transform.TransformDirection(new Vector3(lado * vel, rbd.velocity.y, frente * vel));

        contY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * velRotY;

        Quaternion rotY = Quaternion.AngleAxis(contY, Vector3.up);
        transform.localRotation = rotIni * rotY;

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbd.AddForce(Vector3.up * jump, ForceMode.Impulse);
            onGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            onGround = true;
        }
    }
}