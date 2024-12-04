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

    public Vector3Int cube_to_oddr()
    {
        int col = q + (r - (r & 1)) / 2;
        int row = r;
        return new Vector3Int(col, row, 0);
    }

    public Cube oddr_to_cube(Vector3Int hex)
    {
        int q = hex.x - (hex.y - (hex.y & 1)) / 2;
        int r = hex.y;
        return new Cube(q, r, -q - r);
    }

    public Vector3Int[] direction = new Vector3Int[6]{ new Vector3Int(1, 0, -1 ), new Vector3Int(0, 1, -1), new Vector3Int(-1, 1, 0), new Vector3Int(-1, 0, 1), new Vector3Int(0, -1, 1), new Vector3Int(1, -1, 0) };


    //function cube_direction(directikjon):
    //    return cube_direction_vectors[direction]

    //public void Add(Cube other)
    //{
    //    q += other.q;
    //    r += other.r;
    //    s += other.s;
    //}

    /// <summary>
    /// other을 더한 새로운 Cube 객체 생성 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Cube Add(Vector3Int other)
    {
        return new Cube(q + other.x, r + other.y, s + other.z);
    }


    //function cube_neighbor(cube, direction):
    //    return cube_add(cube, cube_direction(direction))

    // 두 Cube 객체가 같은지 비교하는 메서드
    public override bool Equals(object obj)
    {
        // obj가 Cube 타입이 아니면 false 반환
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Cube other = (Cube)obj;
        // 좌표 q, r, s가 모두 같다면 true
        return q == other.q && r == other.r && s == other.s;
    }

    // Cube 객체의 고유 해시 코드 생성
    public override int GetHashCode()
    {
        // q, r, s를 사용해 고유한 해시 코드 생성
        return HashCode.Combine(q, r, s);
    }

    public override string ToString()
    {
        return "q: "+q+ ", r :" + r+", s :" + s;
    }

}
