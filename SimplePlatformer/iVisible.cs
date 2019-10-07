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
    public interface iVisible
    {
        void Draw(Texture2D Texture, Vector2 Position);
    }

    public class Visible : iVisible
    {
        public void Draw(Texture2D Texture, Vector2 Position)
        {
            SimplePlatformerGame.MySpriteBatch.Draw(Texture, Position - Camera.CurrentCamera.Position, Camera.CurrentCamera.ClearColor);
        }
    }

    public class Invisible : iVisible
    {
        public void Draw(Texture2D Texture, Vector2 Position)
        {
            // do nothing??
        }
    }

}
