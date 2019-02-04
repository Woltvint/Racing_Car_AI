using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Car_Control[] cars;
    public Car_Control car;
    
    public void cameraInic()
    {
        GameObject[] tmp;
        tmp = GameObject.FindGameObjectsWithTag("Car");
        cars = new Car_Control[tmp.Length];

        for (int i = 0; i < tmp.Length; i++)
        {
            cars[i] = tmp[i].GetComponent<Car_Control>();
        }
    }


    
    void FixedUpdate()
    {
        foreach (Car_Control c in cars)
        {
            if (c.alive)
            {
                car = c;
            }
        }

        if (car)
        {
            transform.position = new Vector3(car.gameObject.transform.position.x, car.gameObject.transform.position.y, -10);
            transform.rotation = car.gameObject.transform.rotation;
        }
        
    }
}
