using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{

    public int x;
    public int y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Coordinate() { }

    public Coordinate RandomVector()
    {
        int roll = Random.Range(0, 4);
        switch (roll)
        {
            case 0:
                return new Coordinate(x++, y);
            case 1:
                return new Coordinate(x--, y);
            case 2:
                return new Coordinate(x, y++);
            case 3:
                return new Coordinate(x, y--);
            default:
                return new Coordinate(x--, y--);
        }
    }
}
