using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    public float Speed = 0f;
    public float YSpeed = 0f;
    public float Mass = 10;
    public float Force = 1;
    public float Drag = 1;
    public float Gravity = -9.8f;
    private float _gAccel;
    private float _acceleration;
    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _acceleration = Force / Mass;
        Speed += _acceleration * 1;
        _gAccel = Gravity / Mass;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Speed *= (1 - Time.deltaTime * Drag);
        YSpeed += _gAccel * Time.deltaTime;
        this.transform.Translate(0,YSpeed,Speed * Time.deltaTime);
    }
}
