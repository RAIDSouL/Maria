using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Squared.Tiled;
using System.IO;
using Maria.Sprites;
using Maria.Models;
using Maria.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Maria
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Vector2 viewportPosition;
        Song song;
        List<SoundEffect> soundeffects;

        public Sprite player;

        #region Camera
        // Camera
        Camera camera;

        public Rectangle spriteRectangle;

        #endregion

        public SpriteManager spriteManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Maria Jumpu";
            graphics.PreferredBackBufferWidth = 320*2;
            graphics.PreferredBackBufferHeight = 240*2;
            graphics.ApplyChanges();

            soundeffects = new List<SoundEffect>();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);
            spriteManager = new SpriteManager();    

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var animations = new Dictionary<string, Animation>()
            {
                { "bunny", new Animation(Content.Load<Texture2D>("Player/bunny"), 4) }
            };

            spriteManager.AddSprite(new Player(animations, 10f)
            {

                Position = new Vector2(0, 0),
                Input = new Input()
                {
                    Jump = Keys.X,
                    Left = Keys.A,
                    Right = Keys.D,
                    },
            });
            
            spriteManager.AddSprite(new Sprite(Content.Load<Texture2D>("tiled/blocks")) {
                Position = new Vector2(0, 100)
            });
            player = spriteManager.List[0];
            // TODO: use this.Content to load your game content here
            LoadSfx("jump");
            LoadSfx("hit");

            LoadMap("level1");
            LoadSong("bbsong");
            MediaPlayer.Play(song);

            map.SetupSprite(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), viewportPosition);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void LoadMap (string mapName)
        {
            map = Map.Load(Path.Combine(Content.RootDirectory, "maps/" + mapName + ".tmx"), Content);
        }
        
        public void LoadSong (string songName)
        {
            song = Content.Load<Song>(Path.Combine("Music/" + songName));
        }

        public void LoadSfx(string sfxName)
        {
            soundeffects.Add(Content.Load<SoundEffect>(Path.Combine("sfx/" + sfxName)));
        }
        

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spriteManager.Update(gameTime);
            
            if (player.Texture != null)
                spriteRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y,
                                   player.Texture.Width, player.Texture.Height);

            camera.Update(gameTime, this);

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                soundeffects[0].Play();
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.tranform
                );
            // Render map
            
            //map.Draw(spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), viewportPosition);
            //map.Draw(spriteBatch, new Rectangle(0, 0, 200, 100), viewportPosition);
            // Render sprite

            spriteManager.Draw(gameTime, spriteBatch);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
