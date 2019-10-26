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
  // Tile[][] world;

  public void init() {
    player = GameManager.instance.getPlayer();
    grid = GameManager.instance.getGrid();
    origin = player.transform.position;
    ground = grid.transform.Find("Ground").gameObject.GetComponent<Tilemap>();
    // onGround = grid.transform.Find("On Ground").gameObject.GetComponent<Tilemap>();
    blockingCells = grid.transform.Find("Blocking Cells").gameObject.GetComponent<Tilemap>();
    boundaries = grid.transform.Find("Boundaries").gameObject.GetComponent<Tilemap>();

    for (int i = 0; i < 16; ++i) {
      // world +=
      buildRow(i);
    }
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

    Tile[] buildRow(int rowNum) {
        Tile[] curRow = new Tile[cols];
        for (int i = 0; i < cols; ++i) {
          curRow[i] = buildCell(curRow, prevRow, i, rowNum);
        }
        return curRow;
    }

    Tile buildCell(Tile[] curRow, Tile[] prevRow, int curCol, int rowNum) {
      Vector3Int intOrigin = Vector3Int.FloorToInt(origin);
      intOrigin.x -= 1;
      Tile curCell = ScriptableObject.CreateInstance<Tile>();
      Vector3Int pos = new Vector3Int(intOrigin.x + curCol, intOrigin.y + rowNum, 0);

      if (curCol == 0 || curCol == cols - 1) {
        curCell.sprite = boundarySprite;
        boundaries.SetTile(pos, curCell);
        return curCell;
      }

      int seed = Random.Range(0, 100);
      if (seed > 70 && (curCol != 1 || rowNum != 0)) {
        curCell.sprite = blockedCellSprites[Random.Range(0, blockedCellSprites.Length)];
        blockingCells.SetTile(pos, curCell);
      }
      else {
        curCell.sprite = normalCellSprites[Random.Range(0, normalCellSprites.Length)];
        ground.SetTile(pos, curCell);
      }
      return curCell;
    }
}
