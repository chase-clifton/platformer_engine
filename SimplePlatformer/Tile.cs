using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimplePlatformer
{
    public class Tile
    {
        public Vector2 Position { get; set; }
        public Point TilePositionInMap { get; set; }

        public Tile(Vector2 position, Point tilePositionInMap)
        {
            TilePositionInMap = tilePositionInMap;
            Position = position;
        }

        public void Draw()
        {
            Rectangle rec = new Rectangle(TilePositionInMap.X * 64, TilePositionInMap.Y * 64, 64, 64);

            SimplePlatformerGame.MySpriteBatch.Draw(Board.CurrentBoard.TileTexture, Position - Camera.CurrentCamera.Position, rec, Color.White);
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            }
        }
    }
}
