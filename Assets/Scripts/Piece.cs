using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public GameBoard Board { get; private set; }
    public TetrData Data { get; private set; }
    public Vector3Int Pos { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public void Create(GameBoard Board, Vector3Int Pos, TetrData Data) {

        this.Board = Board;
        this.Pos = Pos;
        this.Data = Data;

        if (this.Cells == null) {
            this.Cells = new Vector3Int[Data.Cells.Length];
        }

        for (int i = 0; i < Data.Cells.Length; i++) {

            this.Cells[i] = (Vector3Int)Data.Cells[i];

        }

    }

    private void Update() {

        this.Board.Clear(this);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            Move(Vector2Int.left);

        }   else if (Input.GetKeyDown(KeyCode.RightArrow)) {

            Move(Vector2Int.right);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            Move(Vector2Int.down);

        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            QuickDrop();

        } 

        this.Board.Set(this);

    }

    private void QuickDrop() {

        while (Move(Vector2Int.down)) {

            continue;

        }

    }

    private bool Move(Vector2Int amount) {

        Vector3Int newPos = this.Pos;
        newPos.x += amount.x;
        newPos.y += amount.y;

        bool valid = Board.ValidPos(this, newPos);

        if (valid) {

            this.Pos = newPos;

        }

        return valid;

    }

}
