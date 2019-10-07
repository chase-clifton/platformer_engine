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
    public class Sprite
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { set; get; }
        protected iVisible _v;

        public int width { set; get; }
        public int height { set; get; }

        public Sprite(Texture2D texture, Vector2 position, iVisible visible)
        {
            Texture = texture;
            Position = position;
            _v = visible;
        }

        public bool IsOnFirmGround()
        {
            Rectangle onePixelLower = Bounds;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
    }
}
