using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace the_summative
{
    public class Game1 : Game
    {
        Texture2D nasaTexture, spaceXTexture, introBackTexture, spaceBackTexture, launchTexture, 
            landTexture, mercuryTexture, venusTexture, earthTexture, marsTexture, sunTexture;
        Rectangle window, nasaRect, spaceXRect, launchRect, mercuryRect, venusRect, earthRect,
            marsRect, sunRect;
        Vector2 nasaSpeed, spaceXSpeed;
        SpriteFont planetFont;

        float seconds;

        MouseState mouseState, prevMouseState;

        enum Screen
        {
            Intro,
            Space,
            Moon,
            Mars,
            Venus,
            Mercury,
            Outro
        }
        Screen screen;
        

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            nasaRect = new Rectangle(500, 200, 152, 410);
            spaceXRect = new Rectangle(150, 200, 92, 410);
            launchRect = new Rectangle(275, 200, 250, 250);
            mercuryRect = new Rectangle(75, 100, 150, 150);
            venusRect = new Rectangle(300, 100, 150, 150);
            marsRect = new Rectangle(525, 100, 150, 150);

            nasaSpeed = new Vector2(0, 4);
            spaceXSpeed = new Vector2(0, 4);

            screen = Screen.Intro;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            nasaTexture = Content.Load<Texture2D>("nasarocket");
            spaceXTexture = Content.Load<Texture2D>("spacexrocket");
            landTexture = Content.Load<Texture2D>("landbutton");
            launchTexture = Content.Load<Texture2D>("launchbutton");
            sunTexture = Content.Load<Texture2D>("sun");
            mercuryTexture = Content.Load<Texture2D>("mercury");
            venusTexture = Content.Load<Texture2D>("venus");
            earthTexture = Content.Load<Texture2D>("earth");
            marsTexture = Content.Load<Texture2D>("mars");

            introBackTexture = Content.Load<Texture2D>("nasawins");
            spaceBackTexture = Content.Load<Texture2D>("spacebackground");

            planetFont = Content.Load<SpriteFont>("planetFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (launchRect.Contains(mouseState.Position))
                    {
                        nasaRect.X -= (int)nasaSpeed.X;
                        nasaRect.Y -= (int)nasaSpeed.Y;
                        spaceXRect.X -= (int)spaceXSpeed.X;
                        spaceXRect.Y -= (int)spaceXSpeed.Y;
                    }

                }
                if (nasaRect.Bottom < 0 || spaceXRect.Bottom < 0)
                {
                    screen = Screen.Space;
                }
            }
            if (screen == Screen.Space)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (mercuryRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Mercury;
                    }
                    if (venusRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Venus;
                    }
                    if (marsRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Mars;
                    }
                }
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introBackTexture, window, Color.White);
                _spriteBatch.Draw(nasaTexture, nasaRect, Color.White);
                _spriteBatch.Draw(spaceXTexture, spaceXRect, Color.White);
                _spriteBatch.Draw(launchTexture, launchRect, Color.White);
            }
            if (screen == Screen.Space)
            {
                _spriteBatch.Draw(spaceBackTexture, window, Color.White);
                _spriteBatch.Draw(mercuryTexture, mercuryRect, Color.White);
                _spriteBatch.Draw(venusTexture, venusRect, Color.White);
                _spriteBatch.Draw(marsTexture, marsRect, Color.White);
                _spriteBatch.DrawString(planetFont, ("Pick a Planet!"), new Vector2(300, 400), Color.White);
            }

            _spriteBatch.End();
        }
    }
}
