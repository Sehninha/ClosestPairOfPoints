using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private GameObject point;
    private LineRenderer line;
    private ClosestPairOfPoints closestPairOfPoints;

    private void Start()
    {
        point = Resources.Load<GameObject>("Prefabs/Point");
        line = GetComponent<LineRenderer>();
        closestPairOfPoints = new ClosestPairOfPoints();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DefaultPoints();
            Distance smallestDistance = closestPairOfPoints.Calculate(GetPoints());

            smallestDistance.firstPoint.GetComponent<SpriteRenderer>().color = Color.red;
            smallestDistance.secondPoint.GetComponent<SpriteRenderer>().color = Color.red;

            line.SetPositions(new Vector3[]
            {
                smallestDistance.firstPoint.position,
                smallestDistance.secondPoint.position
            });
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RandomPoints();
            Distance smallestDistance = closestPairOfPoints.Calculate(GetPoints());

            smallestDistance.firstPoint.GetComponent<SpriteRenderer>().color = Color.red;
            smallestDistance.secondPoint.GetComponent<SpriteRenderer>().color = Color.red;

            line.SetPositions(new Vector3[]
            {
                smallestDistance.firstPoint.position,
                smallestDistance.secondPoint.position
            });
        }
    }

    private void DefaultPoints()
    {
        Clear();

        Instantiate(point, new Vector2(-13, -0.5f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(-10.5f, 11.5f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(-10, -9), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(-4.5f, 2), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(-1, -8.5f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(0.5f, -6), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(0.5f, 12), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(2, -12.5f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(3.5f, -11), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(5.5f, -3), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(5.5f, 7), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(5, -11.5f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(6.5f, -3.2f), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(7, 10), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(9, 5), Quaternion.identity, transform).name = "Point";
        Instantiate(point, new Vector2(11.5f, 4), Quaternion.identity, transform).name = "Point";
    }

    private void RandomPoints()
    {
        Clear();

        for (int i = 0; i < 16; i++)
        {
            float x = Random.Range(-15f, 15f);
            float y = Random.Range(-15f, 15f);

            Vector2 randomPosition = new Vector2(x, y);

            Instantiate(point, randomPosition, Quaternion.identity, transform).name = "Point";
        }
    }

    private List<Transform> GetPoints()
    {
        List<Transform> points = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i));
        }

        return points;
    }

    private void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
