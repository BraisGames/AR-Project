using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 2;
    public float zoomOutMax = 4;
    [SerializeField] GameObject[] objects;
    [SerializeField] float[] posInicial;
    [SerializeField] float[] posFinal;

    private void Awake()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            posInicial[i] = objects[i].transform.position.z;
        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;

        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    public void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        MoveObjects(increment);
        print(Input.GetAxis("Mouse ScrollWheel"));
    }

    void MoveObjects(float increment)
    {
        float incr = increment;

        if (increment > 0)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) > zoomOutMin + increment)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, objects[i].transform.position.z + incr);
                    incr += Time.deltaTime * increment;
                    print("BBBBBBBBB");
                }
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) <= zoomOutMin)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, posInicial[i]);
                }

            }
        }
        else if(increment<0)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) > zoomOutMin + increment)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, objects[i].transform.position.z + incr);
                    incr -= Time.deltaTime;

                    print("BBBBBBBBB");
                }
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) <= zoomOutMin)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, posInicial[i]);
                }

            }
        }


        if (Input.GetAxis("Mouse ScrollWheel") == -0.1f)
        {
            incr = 0.1f;
            print(Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) + " descendo " + zoomOutMax);

            for (int i = 0; i < objects.Length; i++)
            {
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) < zoomOutMax - 0.1f)
                {
                    print("AAAAAAAAA");

                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, objects[i].transform.position.z + incr);
                    incr -= Time.deltaTime;

                }
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) >= zoomOutMax)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, posFinal[i]);
                }

            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") == 0.1f)
        {
            incr = -0.1f;
            for (int i = 0; i < objects.Length; i++)
            {
                print(Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) + " subindo " + zoomOutMin);
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) > zoomOutMin + 0.1f)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, objects[i].transform.position.z + incr);
                    incr += 0.05f;
                    print("BBBBBBBBB");
                }
                if (Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax) <= zoomOutMin)
                {
                    objects[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, posInicial[i]);
                }

            }
        }
    }
}
