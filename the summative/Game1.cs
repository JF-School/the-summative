using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace the_summative
{
    public class Game1 : Game
    {
        Texture2D nasaTexture, spaceXTexture, introBack, spaceBack, launchTexture, 
            landTexture, mercuryTexture, venusTexture, earthTexture, marsTexture, sunTexture, 
            mercuryBack, mercury2Back, expTexture, astroTexture, fireTexture, outroBack,
            quitTexture;
        Rectangle window, nasaRect, spaceXRect, launchRect, mercuryRect, venusRect, earthRect,
            marsRect, sunRect, expRect, astroRect, fireRect, quitRect;
        Vector2 nasaSpeed, spaceXSpeed;
        SpriteFont planetFont, codFont;
        SoundEffect rocketSound, hiSound, screamSound;
        SoundEffectInstance rocketSoundInstance, hiSoundInstance, screamSoundInstance;

        // change back to 3 seconds
        float seconds = 3f, seconds2 = 1f, seconds3 = 3f;
        bool fire = true, astro = true;
        bool mercuryDeath, venusDeath, marsDeath;

        MouseState mouseState, prevMouseState;

        enum Screen
        {
            Intro,
            Space,
            Mercury,
            Mercury2,
            Venus,
            Mars,
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
            expRect = new Rectangle(-35, 300, 240, 240);
            astroRect = new Rectangle(334, 338, 58, 80);
            fireRect = new Rectangle(227, 146, 329, 400);
            quitRect = new Rectangle(300, 300, 200, 80);

            nasaSpeed = new Vector2(0, 4);
            spaceXSpeed = new Vector2(0, 4);

            screen = Screen.Intro;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // image textures
            nasaTexture = Content.Load<Texture2D>("nasarocket");
            spaceXTexture = Content.Load<Texture2D>("spacexrocket");
            landTexture = Content.Load<Texture2D>("landbutton");
            launchTexture = Content.Load<Texture2D>("launchbutton");
            sunTexture = Content.Load<Texture2D>("sun");
            mercuryTexture = Content.Load<Texture2D>("mercury");
            venusTexture = Content.Load<Texture2D>("venus");
            earthTexture = Content.Load<Texture2D>("earth");
            marsTexture = Content.Load<Texture2D>("mars");
            expTexture = Content.Load<Texture2D>("boom");
            astroTexture = Content.Load<Texture2D>("astronaut");
            fireTexture = Content.Load<Texture2D>("firepic");
            quitTexture = Content.Load<Texture2D>("quit");

            // background textures
            introBack = Content.Load<Texture2D>("nasawins");
            spaceBack = Content.Load<Texture2D>("spacebackground");
            mercuryBack = Content.Load<Texture2D>("mercurybackground");
            mercury2Back = Content.Load<Texture2D>("mercurybackground2");
            outroBack = Content.Load<Texture2D>("BHdeathscreen");

            // text
            planetFont = Content.Load<SpriteFont>("planetFont");
            codFont = Content.Load<SpriteFont>("codFont");

            // sound effects
            rocketSound = Content.Load<SoundEffect>("rocketlaunch");
            rocketSoundInstance = rocketSound.CreateInstance();
            hiSound = Content.Load<SoundEffect>("robloxhi");
            hiSoundInstance = hiSound.CreateInstance();
            screamSound = Content.Load<SoundEffect>("RobloxRAIG");
            screamSoundInstance = screamSound.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            mouseState = Mouse.GetState();
            // screens yay
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
                        nasaRect.Width = 60;
                        nasaRect.Height = 164;
                        nasaRect.X = 703;
                        nasaRect.Y = -146;
                        nasaSpeed.X = 0;
                        nasaSpeed.Y = 3;

                        spaceXRect.Width = 36;
                        spaceXRect.Height = 164;
                        spaceXRect.X = 60;
                        spaceXRect.Y = -145;
                        spaceXSpeed.X = 0;
                        spaceXSpeed.Y = 4;
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
            if (screen == Screen.Mercury)
            {
                rocketSoundInstance.Play();
                nasaRect.X += (int)nasaSpeed.X;
                nasaRect.Y += (int)nasaSpeed.Y;
                spaceXRect.X += (int)spaceXSpeed.X;
                spaceXRect.Y += (int)spaceXSpeed.Y;
                if (nasaRect.Bottom > 240)
                {
                    nasaSpeed.Y = 0;
                }
                if (spaceXRect.Bottom > 512)
                {
                    spaceXSpeed.Y = 0;
                    seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (seconds <= 0)
                    {
                        screen = Screen.Mercury2;
                    }
                }
            }
            if (screen == Screen.Mercury2)
            {
                rocketSoundInstance.Stop();
                nasaRect.X = 263;
                nasaRect.Y = -300;
                nasaRect.Width = 228;
                nasaRect.Height = 615;
                // astronaut spawning
                seconds2 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds2 <= 0 && astro)
                {
                    hiSoundInstance.Play();
                    astro = false;
                }
                // fire spawning
                seconds3 -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds3 <= 0 && fire)
                {
                    fire = false;
                    screamSoundInstance.Play();

                }
                if (!fire)
                {
                    if (screamSoundInstance.State == SoundState.Stopped)
                    {
                        screen = Screen.Outro;
                        mercuryDeath = true;
                    }
                }
            }
            if (screen == Screen.Outro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (quitRect.Contains(mouseState.Position))
                    {
                        Exit();
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
                _spriteBatch.Draw(introBack, window, Color.White);
                _spriteBatch.Draw(nasaTexture, nasaRect, Color.White);
                _spriteBatch.Draw(spaceXTexture, spaceXRect, Color.White);
                _spriteBatch.Draw(launchTexture, launchRect, Color.White);
            }
            if (screen == Screen.Space)
            {
                _spriteBatch.Draw(spaceBack, window, Color.White);
                _spriteBatch.Draw(mercuryTexture, mercuryRect, Color.White);
                _spriteBatch.Draw(venusTexture, venusRect, Color.White);
                _spriteBatch.Draw(marsTexture, marsRect, Color.White);
                _spriteBatch.DrawString(planetFont, ("Pick a Planet!"), new Vector2(300, 400), Color.White);
            }
            if (screen == Screen.Mercury)
            {
                _spriteBatch.Draw(mercuryBack, window, Color.White);
                _spriteBatch.Draw(nasaTexture, nasaRect, Color.White);
                _spriteBatch.Draw(spaceXTexture, spaceXRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 1f);
                if (spaceXRect.Bottom > 512)
                {
                    _spriteBatch.Draw(expTexture, expRect, Color.White);
                }
            }
            if (screen == Screen.Mercury2)
            {
                _spriteBatch.Draw(mercury2Back, window, Color.White);
                _spriteBatch.Draw(nasaTexture, nasaRect, Color.White);
                if (!astro)
                {
                    _spriteBatch.Draw(astroTexture, astroRect, Color.White);
                }
                
                if (!fire)
                {
                    _spriteBatch.Draw(fireTexture, fireRect, Color.White);
                }
            }
            if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(outroBack, window, Color.White);
                if (mercuryDeath)
                {
                    _spriteBatch.DrawString(codFont, ("Cause of Death: Mercury Heat"), new Vector2(150, 100), Color.White);
                }
                _spriteBatch.Draw(quitTexture, quitRect, Color.White);
            }

            _spriteBatch.End();
        }
    }
}
