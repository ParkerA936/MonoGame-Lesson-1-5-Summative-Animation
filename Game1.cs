using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Lesson_1_5_Summative_Animation
{
    enum Screen
        {
            //Defined the screens
            Intro,
            GamePlay,
            End
        }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D starBackTexture, shipTexture;
        SpriteFont skyText;
        Rectangle window,starBackRect, shipRect;
        Screen screen;
        MouseState mouseState, prevMouseState;
        Vector2 textLocation, textSize, prevTextSize;
        float textScale = 1f;
        float xTextOffset;
        SoundEffect themeSound;
        SoundEffectInstance instantThemeSound;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

           

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            textLocation = new Vector2(150, 700);
           
            starBackRect = new Rectangle(0, 0, window.Width, window.Height);
            shipRect = new Rectangle(0, 250, 340, 300);
            xTextOffset = 0f;
            base.Initialize();
            textSize = skyText.MeasureString("Welcome to Parkers Animation\n\n Click To Continue");

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            starBackTexture = Content.Load<Texture2D>("12");
            skyText = Content.Load<SpriteFont>("SkyText");
            shipTexture = Content.Load<Texture2D>("death_Star");
            themeSound = Content.Load<SoundEffect>("Star Wars Main Theme (Full)");
            instantThemeSound = themeSound.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.Intro)
            {
                textLocation.Y -= 1;
                
                //textLocation.X += textSize.X * textScale / 2;
                textSize = skyText.MeasureString("Welcome to Parkers Animation\n\n Click To Continue");
                float prevScale = textScale;
                prevTextSize = textSize * prevScale;
                textScale *= 0.999f;
                textSize = textSize * textScale;
                this.Window.Title = textSize.X.ToString();
                textLocation.X += (prevTextSize.X - textSize.X) / 2;

                instantThemeSound.Play();

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.GamePlay;
                    
                }
            }
            else if (screen == Screen.GamePlay)
            {
               instantThemeSound.Stop();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.End;
                }
            }
                // TODO: Add your update logic here

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);       
                _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(starBackTexture, starBackRect, Color.White);
                
                _spriteBatch.DrawString(skyText, "Welcome to Parkers Animation\n\n        Click To Continue", textLocation, Color.Yellow, 0f, Vector2.Zero, textScale, SpriteEffects.None, 1f);
            }
            if (screen == Screen.GamePlay)
            {
                _spriteBatch.Draw(starBackTexture, starBackRect, Color.White);
                _spriteBatch.Draw(shipTexture, shipRect, Color.White);
            }
            if (screen == Screen.End)
            {
               
                _spriteBatch.DrawString(skyText, ("The End"), new Vector2(250, 200), Color.Yellow);

            }
                // TODO: Add your drawing code here
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
