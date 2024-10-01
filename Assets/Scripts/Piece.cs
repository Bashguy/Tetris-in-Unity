using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece : MonoBehaviour {

    public GameBoard Board { get; private set; }
    public TetrData data { get; private set; }
    public Vector3Int Pos { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public int RotationIndex { get; private set; }
    public void Create(GameBoard Board, Vector3Int Pos, TetrData data) {

        this.Board = Board;
        this.Pos = Pos;
        this.data = data;
        this.RotationIndex = 0;

        if (this.Cells == null) {
            this.Cells = new Vector3Int[data.Cells.Length];
        }

        for (int i = 0; i < data.Cells.Length; i++) {

            this.Cells[i] = (Vector3Int)data.Cells[i];

        }

    }

    private void Update() {

        this.Board.Clear(this);

        if (Input.GetKeyDown(KeyCode.UpArrow)) {

            Rotate(1);

        }

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

    private void Rotate(int direction) {

        int ogRotation = this.RotationIndex;
        this.RotationIndex = Wrap(this.RotationIndex + direction, 0, 4);

        DoRotationMatrix(direction);

        if (!WallKickTest(this.RotationIndex, direction)) {

            this.RotationIndex = ogRotation;
            DoRotationMatrix(-direction);

        }

    }

    private void DoRotationMatrix(int direction) {

        for (int i = 0; i < this.Cells.Length; i++) {

            Vector3 cell = this.Cells[i];

            int x, y;

            switch (this.data.tetr) {

                case Tetr.I:
                case Tetr.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;

                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                    

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));

                    break;


            }

            this.Cells[i]  = new Vector3Int(x, y, 0);

        }

    }

    private bool WallKickTest(int RotationIndex, int rotationDirection) {

        int wallKickIndex = WallKickIndex(RotationIndex, rotationDirection);

        for (int i = 0; i < this.data.WallKicks.GetLength(1); i++) {

            Vector2Int amount = this.data.WallKicks[wallKickIndex, i];

            if(Move(amount)) {

                return true;

            }

        }

        return false;

    }

    private int WallKickIndex(int RotationIndex, int rotationDirection) {

        int wallKickIndex = RotationIndex * 2;

        if (rotationDirection < 0) {

            wallKickIndex--;

        }

        return Wrap(wallKickIndex, 0, this.data.WallKicks.GetLength(0));

    }

    private int Wrap(int input, int min, int max) {

        if (input < min) {

            return max - (min - input) % (max - min);

        }   else {

            return min + (input - min) % (max - min);

        }

    }

}
