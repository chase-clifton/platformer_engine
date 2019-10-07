using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimplePlatformer
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Color ClearColor = Color.White;

        public static Camera CurrentCamera { get; private set; }

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = Vector2.Zero;

            Camera.CurrentCamera = this;
        }

        public void Follow(Vector2 p)
        {
            Position = p - new Vector2(Bounds.Width / 2, Bounds.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            //Vector2 Movement = new Vector2(32f, 2f); ;

            //Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
        }


    }
}
