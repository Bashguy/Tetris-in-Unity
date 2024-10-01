using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

    public Tilemap tilemap { get; private set; }
    public TetrData[] tetrs;
    public Piece CurrPiece { get; private set; }
    public Vector3Int spawnPos;
    public Vector2Int gameBoardSize = new(10, 20);

    public RectInt Bound {

        get {

            Vector2Int pos = new(-gameBoardSize.x / 2, -gameBoardSize.y / 2);
            return new RectInt(pos, gameBoardSize);

        }

    }
    private void Awake() {

        this.tilemap = GetComponentInChildren<Tilemap>();
        this.CurrPiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrs.Length; i++) {

            this.tetrs[i].Create();

        }

    }

    private void Start() {

        CreatePiece();


    }

    public void CreatePiece() {

        int rand = Random.Range(0, this.tetrs.Length);
        TetrData data = this.tetrs[rand];

        this.CurrPiece.Create(this, this.spawnPos, data);

        if(ValidPos(this.CurrPiece, this.spawnPos)) {

            Set(this.CurrPiece);

        } else {

            GameOver();

        }

    }

    private void GameOver() {

        this.tilemap.ClearAllTiles();
        
    }

    public void Set(Piece piece) {

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + piece.Pos;
            this.tilemap.SetTile(tilePos, piece.data.tile);

        }

    }

    public void Clear(Piece piece) {

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + piece.Pos;
            this.tilemap.SetTile(tilePos, null);

        }

    }

    public bool ValidPos(Piece piece, Vector3Int pos) {

        RectInt bound = Bound;

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + pos;

            if (!bound.Contains((Vector2Int)tilePos)) {
                
                return false;

            }

            if (this.tilemap.HasTile(tilePos)) {

                return false;

            }

        }

        return true;

    }

    public void ClearLines()
    {
        RectInt bound = Bound;
        int row = bound.yMin;

        while (row < bound.yMax) {

            if (CheckLine(row)) {

                LineClear(row);

            } else {

                row++;

            }

        }

    }

    public bool CheckLine(int row)
    {
        RectInt bound = Bound;

        for (int col = bound.xMin; col < bound.xMax; col++) {

            Vector3Int position = new(col, row, 0);

            if (!tilemap.HasTile(position)) {

                return false;

            }

        }

        return true;
    }

    public void LineClear(int row) {

        RectInt bound = Bound;

        for (int col = bound.xMin; col < bound.xMax; col++) {

            Vector3Int position = new(col, row, 0);
            tilemap.SetTile(position, null);

        }

        while (row < bound.yMax) {

            for (int col = bound.xMin; col < bound.xMax; col++)
            {
                Vector3Int position = new(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }

    }

}
