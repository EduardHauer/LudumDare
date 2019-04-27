using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float[] maxDistances;
    public GameObject[] guns;

    private int gun = 0;
    private int maxGun = 3;
    private Rigidbody2D rg2d;

    public Transform aim;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rg2d.velocity = (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up).normalized * speed;
        aim.position = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude > maxDistances[gun] ? (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * maxDistances[gun] : (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)) + transform.position;
        aim.position = new Vector3(aim.position.x, aim.position.y, -2f);
        Debug.Log(Input.GetAxis("Scroll") * 0.001f);
        guns[gun].SetActive(false);
        gun += (int)(Input.GetAxis("Scroll") * 0.001f);
        if (gun >= maxGun)
            gun = 0;
        else if (gun < 0)
            gun = maxGun - 1;
        guns[gun].SetActive(true);
        if (maxDistances[gun] <= 0f)
            aim.gameObject.SetActive(false);
        else
            aim.gameObject.SetActive(true);
    }
}
