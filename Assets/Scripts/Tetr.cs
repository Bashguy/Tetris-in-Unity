using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetr {

    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[System.Serializable]
public struct TetrData {

    public Tetr tetr;
    public Tile tile;

    public Vector2Int[] Cells {get; private set;}

    public void Create() {

        this.Cells = Data.Cells[this.tetr];

    }

}