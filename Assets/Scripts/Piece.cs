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

}
