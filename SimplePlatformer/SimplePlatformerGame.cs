using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml;

namespace SimplePlatformer
{
    public class SimplePlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _jumperTexture;
        private Jumper _jumper;
        private Board _board;
        private Camera _camera;
        private SpriteFont _debugFont;

        public static ContentManager MyContent;
        public static SpriteBatch MySpriteBatch;

        public SimplePlatformerGame()
        {
            MyContent = Content;

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 640;

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _jumperTexture = Content.Load<Texture2D>("jumper");
            _jumper = new Jumper(_jumperTexture, Vector2.One * 80, new Visible());
            _board = new Board();
            _camera = new Camera(GraphicsDevice.Viewport);
            _debugFont = Content.Load<SpriteFont>("DebugFont");

            MySpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _jumper.Update(gameTime);
            _camera.Follow(_jumper.Position);
            _camera.Update(gameTime);
            CheckKeyboardAndReact();
        }

        private void CheckKeyboardAndReact()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.F5)) { RestartGame(); }
            if (state.IsKeyDown(Keys.Escape)) { Exit(); }
        }

        private void RestartGame()
        {
            Board.CurrentBoard.CreateNewBoard();
            PutJumperInTopLeftCorner();
        }

        private void PutJumperInTopLeftCorner()
        {
            _jumper.Position = Vector2.One * 80;
            _jumper.Movement = Vector2.Zero;
        }

        protected override void Draw(GameTime gameTime)
        {
            string positionInText =
                string.Format("Position of Jumper: ({0:0.0}, {1:0.0})", _jumper.Position.X, _jumper.Position.Y);
            string movementInText =
                string.Format("Current movement: ({0:0.0}, {1:0.0})", _jumper.curSpeed.X, _jumper.curSpeed.Y);

            string isOnFirmGroundText = string.Format(" On firm ground ? : {0}", _jumper.IsOnFirmGround());

            GraphicsDevice.Clear(Color.Aqua);
            _spriteBatch.Begin();
            base.Draw(gameTime);
            _board.Draw();
            _spriteBatch.DrawString(_debugFont, positionInText, new Vector2(10, 0), Color.White);
            _spriteBatch.DrawString(_debugFont, movementInText, new Vector2(10, 20), Color.White);
            _spriteBatch.DrawString(_debugFont, isOnFirmGroundText, new Vector2(10, 40), Color.White);
            _jumper.Draw();
            _spriteBatch.End();
        }
    }
}
