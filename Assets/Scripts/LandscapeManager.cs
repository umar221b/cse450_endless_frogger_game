using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandscapeManager : MonoBehaviour
{

  public Sprite someSprite;
  GameObject player;
  GameObject grid;
  Vector3 origin;
  Tilemap ground, onGround, blockingCells, boundaries;

  public void init() {
    player = GameManager.instance.getPlayer();
    grid = GameManager.instance.getGrid();
    origin = player.transform.position;
    ground = grid.transform.Find("Ground").gameObject.GetComponent<Tilemap>();
    // onGround = grid.transform.Find("On Ground").gameObject.GetComponent<Tilemap>();
    // blockingCells = grid.transform.Find("Blocking Cells").gameObject.GetComponent<Tilemap>();
    // boundaries = grid.transform.Find("Boundaries").gameObject.GetComponent<Tilemap>();

    Tile curCell = ScriptableObject.CreateInstance<Tile>();
    curCell.sprite = someSprite;
    Vector3Int pos = Vector3Int.FloorToInt(origin);
    ground.SetTile(pos, curCell);
  }
    int distanceCrossed;
    public int cols;
    Tile[] prevRow = null;

    public Sprite[] blockedCellSprites;
    public Sprite[] onFloorCellSprites;
    public Sprite boundarySprite;
    public Sprite[] normalCellSprites;
    //
    // public void init() {
    //   player = GameManager.instance.getPlayer();
    //   origin = player.transform.position;
    // }

    Tile[] buildRow() {
        Tile[] curRow = new Tile[cols];
        for (int i = 0; i < cols; ++i) {
          curRow[i] = buildCell(curRow, prevRow);
        }
        return curRow;
    }

    Tile buildCell(Tile[] curRow, Tile[] prevRow) {
      int index = curRow.Length;
      Tile curCell = ScriptableObject.CreateInstance<Tile>();

      Vector3Int pos = new Vector3Int(0 + 1, 0 + 1, 0);
      ground.SetTile(pos, curCell);
      // print(curCell.sprite);

      if (index == 0 || index == cols - 1) {
        curCell.sprite = someSprite;
      }
      return curCell;
    }
}
