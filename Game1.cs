using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        Texture2D starBackTexture, shipTexture, earthTexture, planetExplodesTexture, planetExplodedTexture, laserTexture, endTexture;
        SpriteFont skyText;
        Rectangle window,starBackRect, shipRect, earthRect, planetExplodesRect, planetExplodedRect, laserRect;
        Screen screen;
        MouseState mouseState, prevMouseState;
        Vector2 textLocation, textSize, prevTextSize, textLocation2;
        float textScale = 1f;
        float xTextOffset;
        SoundEffect themeSound, laserSound, explosionSound;
        SoundEffectInstance instantThemeSound, instantLaserSound, instantExplosionSound;
        bool beam, click, click2, music;
        int timer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Lesson 4 - Sound and Time";
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
            textLocation2 = new Vector2(200, 20);
            earthRect = new Rectangle(400,120,500,300);
            planetExplodesRect = new Rectangle(460,110,580,350);
            planetExplodedRect = new Rectangle(0, 0, 800, 600);
            laserRect = new Rectangle(150,240,500,250);
            click = false;
            click2 = false;
            beam = false;
            music = false;
            starBackRect = new Rectangle(0, 0, window.Width, window.Height);
            shipRect = new Rectangle(-508, 250, 1017, 880);
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
            earthTexture = Content.Load<Texture2D>("Earth");
            planetExplodedTexture = Content.Load<Texture2D>("Planet Exploded");
            planetExplodesTexture = Content.Load<Texture2D>("Planet_Half");
            laserTexture = Content.Load<Texture2D>("Laser Beam");
            themeSound = Content.Load<SoundEffect>("Star Wars Main Theme (Full)");
            instantThemeSound = themeSound.CreateInstance();
            laserSound = Content.Load<SoundEffect>("Laser Sound");
            instantLaserSound = laserSound.CreateInstance();
            explosionSound = Content.Load<SoundEffect>("explosion");
            instantExplosionSound = explosionSound.CreateInstance();
            endTexture = Content.Load<Texture2D>("Star Wars End");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = mouseState.Position.ToString();
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
               
                
                //if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released&& click && click2)
                //{
                //    screen = Screen.End;
                //}

                //if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && click )
                //{
                    
                //        click2 = true;
                   
                //}

                //if (laserRect.Width <= 500 && laserRect.Height <= 250 && click)
                //    {
                //        laserRect.Width += 2;
                //        laserRect.Height += 1;
                //    }

               
                
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    
                    instantLaserSound.Play();
                    
                    
                        click = true;
                   // if (laserRect.Width >= 500 && laserRect.Height >= 250)
                    //need to change
                   // { beam =  true; }
                   
                }
                if (click)
                {
                    timer += 1;
                }
                if (timer == 45)
                {
                    beam = true;
                }

                if (timer == 280 )
                    {
                    instantExplosionSound.Stop();
                        click2 = true;
                    instantExplosionSound.Play();
                   
                    }
                if (timer == 600 )
                {
                    instantExplosionSound.Stop();
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
                _spriteBatch.DrawString(skyText, "Click To Destroy Earth",textLocation2, Color.Yellow);
                _spriteBatch.Draw(shipTexture, shipRect, Color.White);
                _spriteBatch.Draw(earthTexture, earthRect, Color.White);
                if (click)
                {
                    if (beam)
                    {
                        _spriteBatch.Draw(planetExplodesTexture, planetExplodesRect, Color.White);
                    }
                 
                _spriteBatch.Draw(laserTexture, laserRect, Color.White);        
                       
                   if (click2)
                        {
                            _spriteBatch.Draw(planetExplodedTexture, planetExplodedRect, Color.White);
                        }
                       
                   
                   
                }

            }
            if (screen == Screen.End)
            {
               
                
                _spriteBatch.Draw(endTexture, window, Color.White);

            }
                // TODO: Add your drawing code here
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
