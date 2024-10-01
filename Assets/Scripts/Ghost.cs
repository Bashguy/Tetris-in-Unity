using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Ghost : MonoBehaviour {

    public Tile tile;
    public GameBoard board;
    public Piece trackingPiece;

    public Tilemap tilemap { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Pos { get; private set; }

    private void Awake() {

        this.tilemap = GetComponentInChildren<Tilemap>();
        this.Cells = new Vector3Int[4];

    }

    private void LateUpdate() {

        Clear();
        Copy();
        Drop();
        Set();

    }

private void Clear()
    {
        for (int i = 0; i < Cells.Length; i++) {

            Vector3Int tilePosition = Cells[i] + Pos;

            tilemap.SetTile(tilePosition, null);

        }

    }

    private void Copy() {

        for (int i = 0; i < Cells.Length; i++) {

            Cells[i] = trackingPiece.Cells[i];

        }

    }

    private void Drop() {

        Vector3Int position = trackingPiece.Pos;

        int current = position.y;
        int bottom = -this.board.gameBoardSize.y / 2 - 1;

        this.board.Clear(trackingPiece);

        for (int row = current; row >= bottom; row--) {

            position.y = row;

            if (this.board.ValidPos(trackingPiece, position)) {

                this.Pos = position;

            } else {

                break;

            }

        }

        this.board.Set(trackingPiece);
    }

    private void Set() {

        for (int i = 0; i < Cells.Length; i++) {

            Vector3Int tilePosition = Cells[i] + Pos;
            tilemap.SetTile(tilePosition, tile);

        }
        
    }

}
