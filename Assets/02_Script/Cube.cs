using UnityEngine;
using System;

[Serializable]
public class Cube
{
    public int q, r, s;

    public Cube(int q=0, int r=0, int s=0)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }

    public Vector3Int cube_to_oddr(Cube hex)
    {
        int col = hex.q + (hex.r - (hex.r & 1)) / 2;
        int row = hex.r;
        return new Vector3Int(col, row, 0);
    }

    public Cube oddr_to_cube(Vector3Int hex)
    {
        int q = hex.x - (hex.y - (hex.y & 1)) / 2;
        int r = hex.y;
        return new Cube(q, r, -q - r);
    }
}
