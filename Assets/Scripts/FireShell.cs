using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FireShell : MonoBehaviour {

    public GameObject Bullet;
    public GameObject Turret;
    public GameObject Enemy;
    public Transform TurretBase;

    private float _speed = 15.0f;
    private float _rotSpeed = 5.0f;
    private float _moveSpeed =  1f;

    static float delayReset = 0.1f;
    float delay = delayReset;

    void CreateBullet() {

        GameObject shell = Instantiate(Bullet, Turret.transform.position, Turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = _speed * TurretBase.forward;
    }

    float? RotateTurret() {

        float? angle = CalculateAngle(false);

        if (angle != null) {

            TurretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    float? CalculateAngle(bool low) {

        Vector3 targetDir = Enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float sSqr = _speed * _speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {

            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        } else
            return null;
    }

    void Update() {

        delay -= Time.deltaTime;
        Vector3 direction = (Enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * _rotSpeed);
        float? angle = RotateTurret();

        if (angle != null && delay <= 0.0f) {

            CreateBullet();
            delay = delayReset;
        } else {

            this.transform.Translate(0.0f, 0.0f, Time.deltaTime * _moveSpeed);
        }
    }

    //Vector3 CalculateTrajectory() {

    //    Vector3 p = enemy.transform.position - this.transform.position;
    //    Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
    //    float s = bullet.GetComponent<MoveShell>().speed;

    //    float a = Vector3.Dot(v, v) - s * s;
    //    float b = Vector3.Dot(p, v);
    //    float c = Vector3.Dot(p, p);
    //    float d = b * b - a * c;

    //    if (d < 0.1f) return Vector3.zero;

    //    float sqrt = Mathf.Sqrt(d);
    //    float t1 = (-b - sqrt) / c;
    //    float t2 = (-b + sqrt) / c;

    //    float t = 0.0f;
    //    if (t1 < 0.0f && t2 < 0.0f) return Vector3.zero;
    //    else if (t1 < 0.0f) t = t2;
    //    else if (t2 < 0.0f) t = t1;
    //    else {

    //        t = Mathf.Max(new float[] { t1, t2 });
    //    }
    //    return t * p + v;
    //}
}
