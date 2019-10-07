using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TiledSharp;

namespace SimplePlatformer
{
    public class Board
    {
        public List<Tile> Tiles = new List<Tile>();
        public int TileWidth;
        public int TileHeight;
        public int tilesetTilesWide;
        public int tilesetTilesHigh;
        public Texture2D TileTexture { set; get; }
        public TmxMap Map;

        public static Board CurrentBoard { get; private set; }

        public Board()
        {
            Map = new TmxMap("Content/stage1.tmx");
            TileTexture = SimplePlatformerGame.MyContent.Load<Texture2D>(Map.Tilesets[0].Name.ToString());

            TileWidth = Map.Tilesets[0].TileWidth;
            TileHeight = Map.Tilesets[0].TileHeight;

            tilesetTilesHigh = TileTexture.Width / TileWidth;
            tilesetTilesWide = TileTexture.Height / TileHeight;

            CreateNewBoard();
            Board.CurrentBoard = this;
        }

        public void CreateNewBoard()
        {
            for (var i = 0; i < Map.Layers[0].Tiles.Count; i++)
            {
                var tile = Map.Layers[0].Tiles[i];
                int gid = tile.Gid;

                if (gid == 0) // No tile
                {
                    continue;
                }

                int timeFrame = gid - 1;

                int column = timeFrame % tilesetTilesWide;
                int row = (int)Math.Floor((double)timeFrame / (double)tilesetTilesWide);

                int x = (i % Map.Width);
                int y = (i / Map.Width);

                Point tilePositionInMap = new Point(column, row);

                Vector2 tilePosition = new Vector2(x * Map.Tilesets[0].TileWidth, y * Map.Tilesets[0].TileHeight);
                Tiles.Add(new Tile(tilePosition, tilePositionInMap));
            }
        }

        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        private Vector2 CheckPossibleNonDiagonalMovement(MovementWrapper wrapper, int stepsAlreadyTaken)
        {
            if (wrapper.IsDiagonalMove)
            {
                int stepsLeft = wrapper.NumberOfStepsToBreakMovementInto - (stepsAlreadyTaken - 1);

                Vector2 remainingHorizontalMovement = wrapper.OneStep.X * Vector2.UnitX * stepsLeft;
                wrapper.FurthestAvailableLocationSoFar =
                    WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, wrapper.FurthestAvailableLocationSoFar + remainingHorizontalMovement, wrapper.BoundingRectangle);

                Vector2 remainingVerticalMovement = wrapper.OneStep.Y * Vector2.UnitY * stepsLeft;
                wrapper.FurthestAvailableLocationSoFar =
                    WhereCanIGetTo(wrapper.FurthestAvailableLocationSoFar, wrapper.FurthestAvailableLocationSoFar + remainingVerticalMovement, wrapper.BoundingRectangle);
            }

            return wrapper.FurthestAvailableLocationSoFar;
        }

        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 targetPosition, Rectangle boundingRectangle)
        {
            MovementWrapper move = new MovementWrapper(originalPosition, targetPosition, boundingRectangle);

            for (int i = 1; i <= move.NumberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + (move.OneStep * i);
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, boundingRectangle.Width, boundingRectangle.Height);

                if (HasRoomForRectangle(newBoundary))
                {
                    move.FurthestAvailableLocationSoFar = positionToTry;
                }
                else
                {
                    if (move.IsDiagonalMove)
                    {
                        move.FurthestAvailableLocationSoFar = CheckPossibleNonDiagonalMovement(move, i);
                    }
                    break;
                }
            }

            return move.FurthestAvailableLocationSoFar;
        }

        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (var tile in Tiles)
            {
                if (tile.Bounds.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            return true;
        }

        public void Draw()
        {

            foreach (var tile in Tiles)
            {
                tile.Draw();
            }
        }
    }
}
