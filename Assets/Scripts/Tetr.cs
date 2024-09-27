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

}