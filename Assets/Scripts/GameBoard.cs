using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour {

    public Tilemap Tilemap { get; private set; }
    public TetrData[] tetrs;
    public Piece CurrPiece { get; private set; }
    public Vector3Int spawnPos;
    public Vector2Int gameBoardSize = new Vector2Int(10, 20);

    public RectInt Bound {

        get {

            Vector2Int pos = new(-gameBoardSize.x / 2, -gameBoardSize.y / 2);
            return new RectInt(pos, gameBoardSize);

        }

    }
    private void Awake() {

        this.Tilemap = GetComponentInChildren<Tilemap>();
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
        Set(this.CurrPiece);

    }

    public void Set(Piece piece) {

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + piece.Pos;
            this.Tilemap.SetTile(tilePos, piece.Data.tile);

        }

    }

    public void Clear(Piece piece) {

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + piece.Pos;
            this.Tilemap.SetTile(tilePos, null);

        }

    }

    public bool ValidPos(Piece piece, Vector3Int pos) {

        RectInt bound = Bound;

        for (int i = 0; i < piece.Cells.Length; i++) {

            Vector3Int tilePos = piece.Cells[i] + pos;

            if (!bound.Contains((Vector2Int)tilePos)) {
                
                return false;

            }

            if (this.Tilemap.HasTile(tilePos)) {

                return false;

            }

        }

        return true;

    }
    
}
