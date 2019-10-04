using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Distance
{
    public float distance;
    public Transform firstPoint;
    public Transform secondPoint;

    public Distance(Transform firstPoint, Transform secondPoint)
    {
        this.firstPoint = firstPoint;
        this.secondPoint = secondPoint;

        distance = Mathf.Pow(firstPoint.position.x - secondPoint.position.x, 2) +
                   Mathf.Pow(firstPoint.position.y - secondPoint.position.y, 2);
    }

    public Distance(Transform singlePoint)
    {
        firstPoint = singlePoint;
        secondPoint = null;

        distance = Mathf.Infinity;
    }
}

public class ClosestPairOfPoints
{
    public Distance Calculate(List<Transform> points)
    {
        points = points.OrderBy(point => point.position.x).ToList();

        int median = points.Count / 2;

        Distance leftSmallestDistance = DivideAndConquer(points.Take(median).ToList());
        Distance rightSmallestDistance = DivideAndConquer(points.Skip(median).ToList());

        Distance smallestDistance;

        if (leftSmallestDistance.distance < rightSmallestDistance.distance)
        {
            smallestDistance = leftSmallestDistance;
        }
        else
        {
            smallestDistance = rightSmallestDistance;
        }

        float medianX = points[median].position.x;

        List<Transform> medianPoints = new List<Transform>();

        for (int i = 0; i < points.Count; i++)
        {
            float distance = Mathf.Pow(points[i].position.x - medianX, 2);

            if (distance <= smallestDistance.distance)
            {
                medianPoints.Add(points[i]);
            }
        }

        medianPoints = medianPoints.OrderBy(point => point.position.y).ToList();

        Distance smallestMedianDistance = DivideAndConquer(medianPoints);

        if (smallestDistance.distance < smallestMedianDistance.distance)
        {
            return smallestDistance;
        }
        else
        {
            return smallestMedianDistance;
        }
    }

    private Distance DivideAndConquer(List<Transform> points)
    {
        if (points.Count == 1)
        {
            return new Distance(points[0]);
        }
        else if (points.Count == 2)
        {
            return new Distance(points[0], points[1]);
        }

        int median = points.Count / 2;
        List<Transform> leftPoints = points.Take(median).ToList();
        List<Transform> rightPoints = points.Skip(median).ToList();

        Distance leftDistance = DivideAndConquer(leftPoints);
        Distance rightDistance = DivideAndConquer(rightPoints);
        Distance middleDistance = new Distance(leftPoints[leftPoints.Count - 1], rightPoints[0]);

        if (leftDistance.distance < rightDistance.distance &&
            leftDistance.distance < middleDistance.distance)
        {
            return leftDistance;
        }
        else if (rightDistance.distance < leftDistance.distance &&
                 rightDistance.distance < middleDistance.distance)
        {
            return rightDistance;
        }
        else
        {
            return middleDistance;
        }
    }
}
