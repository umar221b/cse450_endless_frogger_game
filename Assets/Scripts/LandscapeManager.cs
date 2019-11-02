using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandscapeManager : MonoBehaviour
{

  GameObject player;
  GameObject grid;
  Vector3 origin;
  Tilemap ground, blockingCells, boundaries;
  // Tilemap onGround;
  Tile [][] world;
  char [, ] cellType;
  int xProbability = 50;
  int psInPrevRow = 0;

  public void init() {
    player = GameManager.instance.getPlayer();
    grid = GameManager.instance.getGrid();
    origin = player.transform.position;
    ground = grid.transform.Find("Ground").gameObject.GetComponent<Tilemap>();
    // onGround = grid.transform.Find("On Ground").gameObject.GetComponent<Tilemap>();
    blockingCells = grid.transform.Find("Blocking Cells").gameObject.GetComponent<Tilemap>();
    boundaries = grid.transform.Find("Boundaries").gameObject.GetComponent<Tilemap>();
    world = new Tile[ROWS][];
    cellType = new char[ROWS + 1, COLS];
    cellType[0, 0] = 'B';
    cellType[0, COLS - 1] = 'B';
    for (int j = 1; j < COLS - 1; ++j) {
      cellType[0, j] = 'P';
    }

    for (int i = 0; i < ROWS; ++i) {
      world[i] = buildRow(i + 1);
      connectPath(i + 1);
    }

    // string s = "";
    // for (int i = ROWS; i >= 0; --i) {
    //   for (int j = 0; j < COLS; ++j) {
    //     if (j != 0)
    //       s += ' ';
    //     s += cellType[i, j];
    //   }
    //   s += '\n';
    // }
    // print(s);

  }
  public int COLS;
  public int ROWS;

  public Sprite[] blockedCellSprites;
  // public Sprite[] onGroundCellSprites;
  public Sprite boundarySprite;
  public Sprite[] normalCellSprites;

  Tile[] buildRow(int curRowNum) {
    psInPrevRow = 0;
    for (int j = 1; j < COLS - 1; ++j) {
      if (cellType[curRowNum - 1, j] == 'P')
      ++psInPrevRow;
    }
    Tile[] curRow = new Tile[COLS];
    for (int i = 0; i < COLS; ++i) {
      curRow[i] = buildCell(curRow, i, curRowNum);
    }
    return curRow;
  }

  Tile buildCell(Tile[] curRow, int curColNum, int curRowNum) {
    Vector3Int intOrigin = Vector3Int.FloorToInt(origin);
    intOrigin.x -= 1;
    Tile curCell = ScriptableObject.CreateInstance<Tile>();
    Vector3Int pos = new Vector3Int(intOrigin.x + curColNum, intOrigin.y + curRowNum - 1, 0);

    char curCellType = 'B';

    if (curColNum == 0 || curColNum == COLS - 1) {
      curCell.sprite = boundarySprite;
      boundaries.SetTile(pos, curCell);
      cellType[curRowNum, curColNum] = curCellType;
      return curCell;
    }

    // Check previous row
    switch (cellType[curRowNum - 1, curColNum]) {
      case 'X':
      case 'N':
        curCellType = randomXWithProbability(xProbability);
        break;
      case 'P':
      if (psInPrevRow > 1 && curRowNum > 1)  {
        curCellType = randomXWithProbability(xProbability);
                    if (curCellType == 'N')
                        curCellType = 'P';
                    else
                    {
                        --psInPrevRow;
                       
                    }
      }
      else {
        curCellType = 'P';
      }
      break;
    }

    cellType[curRowNum, curColNum] = curCellType;

    switch (cellType[curRowNum, curColNum]) {
      case 'X':
        curCell.sprite = blockedCellSprites[Random.Range(0, blockedCellSprites.Length)];
        blockingCells.SetTile(pos, curCell);
        break;
            case 'N':
            case 'P':
        curCell.sprite = normalCellSprites[Random.Range(0, normalCellSprites.Length)];
        ground.SetTile(pos, curCell);
        break;
        }

    // iterate row to update Ps
    return curCell;
  }

  char randomXWithProbability(int prob) {
    int seed = Random.Range(0, 100);
    if (seed < prob)
      return 'X';
    else
      return 'N';
  }

    void connectPath(int rowNum)
    {
        List<int> nIndices = new List<int>();
        for (int i = 1; i < COLS - 1; ++i)
        {
            bool seenP = false;
            for (int j = i; j < COLS - 1; ++j)
            {
                bool breakFromJLoop = false;
                switch (cellType[rowNum, j])
                {
                    case 'X':
                        if (seenP)
                        {
                            foreach (int nIndex in nIndices)
                            {
                                cellType[rowNum, nIndex] = 'P';
                            }
                        }
                        nIndices.Clear();
                        i = j;
                        breakFromJLoop = true;
                        break;
                    case 'N':
                        nIndices.Add(j);
                        break;
                    case 'P':
                        seenP = true;
                        break;
                }
                if (breakFromJLoop)
                    break;
            }
        }
    }
}
