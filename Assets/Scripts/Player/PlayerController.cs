using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public int xPos = 0, yPos = 0;

    PlayerStats playerStats;

    public PlayerController()
    {
        playerStats = Player.Instance.stats;
    }

    public void TryMove(Direction dir)
    {
        if (!Player.Instance.canMove)
            return;

        Action action = DoMove(dir);
        Debug.Log(action.ToString());

        if (action == Action.Nothing)
        {
            // temp TODO: przemyśl to
            if (dir == Direction.DownLeft)
            {
                action = DoMove(Direction.Left);
                dir = Direction.Left;
            }
            else if (dir == Direction.UpLeft)
            {
                action = DoMove(Direction.Left);
                dir = Direction.Left;
            }
            else if (dir == Direction.DownRight)
            {
                action = DoMove(Direction.Right);
                dir = Direction.Right;
            }
            else if (dir == Direction.UpRight)
            {
                action = DoMove(Direction.Right);
                dir = Direction.Right;
            }
        }
        
        Player.Instance.WaitForMove();

    }

    public Action DoMove(Direction dir)
    {
        Action action = GetAction(dir);

        if (!playerStats.CheckAction(action))
            return Action.Nothing;

        switch (action)
        {
            case Action.Move:
                Move(dir, Action.Move); // contains Move()
                return action;
                break;

            case Action.Dig:

                // przekaż tile do animatora który będzie uruchamiał eventy w animacji
                Dig(dir);
                break;

            case Action.Climb:
                Move(dir, Action.Move);
                break;

            case Action.JumpDown:
                Move(dir, Action.Move);
                break;

            case Action.Nothing:
                Move(dir, Action.Move);
                break;

            case Action.DigAndRope:

                TileMap myTile = GetMyTile();
                Rope rope = myTile.GetElement(ItemType.Rope) as Rope;
                rope.length--;
                rope.isLast = true;

                Dig(dir, rope); // contains Move()

                rope.isLast = false;
                myTile.RefreshElements();
                // przekaż tile do animatora który będzie uruchamiał eventy w animacji

                break;
        }


        return action;
    }

    void Move(Direction dir, Action action)
    {
        if (playerStats.DoAction(action))
        {
            Player.Instance.movement.MoveTransformInDirecton(dir, 1);
            MovePos(dir); // zakończenie ruchu i update pozycji gracza na siatce
        }
    }

    void Dig(Direction dir, Rope rope = null)
    {
        TileMap tile = GetTileInDirection(dir);

        // Sprawdź czy jest w stanie kopać
        if (tile.ore != Ore.Ground)
        {
            Player.Instance.bag.AddOre(tile.ore, tile.oreAmount);
        }
        if (rope != null)
        {
            tile.Dig(rope);
            Move(dir, Action.DigAndRope);
        }
        else
        {
            tile.Dig();
            Move(dir, Action.DigAndRope);
        }

    }

    Action GetAction(Direction dir)
    {
        TileMap tile = GetTileInDirection(dir);

        switch (tile.tileType)  // TODO: tileType switch
        {
            case TileType.Surface:
                return Action.Nothing;

            case TileType.Ground:
                {
                    if (dir == Direction.Left || dir == Direction.Right)
                        return Action.Dig;
                    if (dir == Direction.Down)
                        return CheckDig(Direction.Down);
                    // if ladder
                }
                break;

            case TileType.Empty:
                if (dir == Direction.Left || dir == Direction.Right)
                    return Action.Move;
                if (dir == Direction.Up && CheckClimbing())
                    return Action.Move;
                break;

            case TileType.Building:
                if (dir == Direction.Left || dir == Direction.Right)    // up
                    return Action.Move;
                break;

            case TileType.Hole:
                // jeśli jest przedmiot umozliwiający poruszanie
                if (dir == Direction.Left || dir == Direction.Right)
                    return Action.Move;
                else if (dir == Direction.Down)
                {
                    if (CheckDownMove())
                        return Action.Move;
                }
                else if (dir == Direction.Up)
                {
                    if (CheckUpMove())
                        return Action.Move;
                }
                else if (dir == Direction.UpLeft
                && GetTileInDirection(Direction.Up).tileType == TileType.Hole
                && GetTileInDirection(Direction.Left).tileType == TileType.Ground)
                    return Action.Climb;
                if (dir == Direction.UpRight
                    && GetTileInDirection(Direction.Up).tileType == TileType.Hole
                    && GetTileInDirection(Direction.Right).tileType == TileType.Ground)
                    return Action.Climb;
                if (dir == Direction.DownLeft
                    && GetTileInDirection(Direction.Left).tileType == TileType.Hole
                    && GetTileInDirection(Direction.Down).tileType == TileType.Ground)
                    return Action.JumpDown;
                if (dir == Direction.DownRight
                    && GetTileInDirection(Direction.Right).tileType == TileType.Hole
                    && GetTileInDirection(Direction.Down).tileType == TileType.Ground)
                    return Action.JumpDown;
                break;

            default:
                return Action.Nothing;
        }
        return Action.Nothing;
    }

    /// <summary>
    /// Sprawdza czy można kopać w dół. Zwraca odpowiednią akcję
    /// </summary>
    private Action CheckDig(Direction dir)
    {
        TileMap tile = GetTileInDirection(dir);

        // dig if( gracz ma wystarczająco mocny ekwipunek )
        if (CheckRope())
            return Action.DigAndRope;
        else
            return Action.Dig;

    }

    /// <summary>
    /// Zwraca true jeśli można rozwinąć linę w dół
    /// </summary>
    /// <returns></returns>
    private bool CheckRope()
    {
        TileMap myTile = GetTileInDirection(Direction.Center);
        Rope rope = myTile.GetElement(ItemType.Rope) as Rope;
        if (rope == null)
        {
            return false;
        }
        if (rope.length > 0)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Sprawdza czy można iść w kierunku
    /// </summary>
    bool CanWalk(Direction dir)
    {
        TileMap tile;
        if (dir == Direction.Left)
        {
            tile = GetTileInDirection(Direction.DownLeft);
        }
        else if (dir == Direction.Right)
        {
            tile = GetTileInDirection(Direction.DownRight);
        }
        else
            return false;

        if (tile.tileType == TileType.Ground
            || tile.CheckElement(ItemType.Ladder)
            || tile.CheckElement(ItemType.Rope))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Zwraca true jeśli można poruszać sie w górę
    /// </summary>
    bool CheckUpMove()
    {
        if (GetMyTile().CheckElement(ItemType.Rope)
            || GetMyTile().CheckElement(ItemType.Ladder))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Zwraca true jeśli można poruszać sie w dół
    /// </summary>
    bool CheckDownMove()
    {
        if (GetTileInDirection(Direction.Down).CheckElement(ItemType.Rope)
            || GetTileInDirection(Direction.Down).CheckElement(ItemType.Ladder))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Zwraca kafelek z podanego kierunku
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public TileMap GetTileInDirection(Direction dir)
    {
        int x, y;
        switch (dir)
        {
            case Direction.Down:
                x = xPos;
                y = yPos + 1;
                break;
            case Direction.DownLeft:
                x = xPos - 1;
                y = yPos + 1;
                break;
            case Direction.Left:
                x = xPos - 1;
                y = yPos;
                break;
            case Direction.UpLeft:
                x = xPos - 1;
                y = yPos - 1;
                break;
            case Direction.Up:
                x = xPos;
                y = yPos - 1;
                break;
            case Direction.UpRight:
                x = xPos + 1;
                y = yPos - 1;
                break;
            case Direction.Right:
                x = xPos + 1;
                y = yPos;
                break;
            case Direction.DownRight:
                x = xPos + 1;
                y = yPos + 1;
                break;
            default:
                x = xPos;
                y = yPos;
                break;
        }

        return MapManager.Instance.GetTile(x, y);
    }

    /// <summary>
    /// Sprawdza czy można sie wspinać (na razie tylko lina)
    /// </summary>
    /// <returns></returns>
    private bool CheckClimbing()
    {
        if (GetTileInDirection(Direction.Center).tileType == TileType.Hole)
        {
            Element element = GetTileInDirection(Direction.Center).GetElement(ItemType.Rope);
            if (element == null)
                return false;
            else
                return true;
        }
        else
            return false;
    }

    public TileMap GetMyTile()
    {
        return GetTileInDirection(Direction.Center);
    }

    public void MovePos(Direction dir)
    {
        switch (dir)
        {
            case Direction.Down:
                yPos++;
                break;
            case Direction.DownLeft:
                xPos--;
                yPos++;
                break;
            case Direction.Left:
                xPos--;
                break;
            case Direction.UpLeft:
                xPos--;
                yPos--;
                break;
            case Direction.Up:
                yPos--;
                break;
            case Direction.UpRight:
                xPos++;
                yPos--;
                break;
            case Direction.Right:
                xPos++;
                break;
            case Direction.DownRight:
                xPos++;
                yPos++;
                break;
            case Direction.Center:
                break;
        }
    }
}