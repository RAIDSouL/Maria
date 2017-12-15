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
        public static Game1 Instance { get; private set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Vector2 viewportPosition;
        Song song;
        public List<SoundEffect> soundeffects;

        public Sprite player;

        #region Camera
        // Camera
        Camera camera;

        // Mainmenu
        MainMenu mainmenu;

        public Rectangle spriteRectangle;

        #endregion

        public SpriteManager spriteManager;
        private float timer = 0;

        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Maria Jumpu";
            graphics.PreferredBackBufferWidth = 320*2;
            graphics.PreferredBackBufferHeight = 240*2;
            graphics.ApplyChanges();
            soundeffects = new List<SoundEffect>();
            mainmenu = new MainMenu();
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
                { "bunny", new Animation(Content.Load<Texture2D>("Player/bunnyjung"), 3) },
            };

            spriteManager.AddSprite(new Player(animations, 10f)
            {

                Position = new Vector2(0, 100),
                Input = new Input()
                {
                    Jump = Keys.X,
                    Left = Keys.A,
                    Right = Keys.D,
                },
            });

            spriteManager.DebugBox.Add(Content.Load<Texture2D>("debugbox"));
            
            player = spriteManager.List[0];
            // TODO: use this.Content to load your game content here
            
            LoadObj();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            
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
            SpriteManager.Instance.DestroyAllBlock();
            map = Map.Load(Path.Combine(Content.RootDirectory, "maps/" + mapName + ".tmx"), Content);
            map.SetupSprite();
            map.SetPlayerLocation();
        }
        
        public void LoadSong (string songName)
        {
            //song = Content.Load<Song>(Path.Combine("Music/" + songName));
        }

        public void LoadSfx(string sfxName)
        {
            soundeffects.Add(Content.Load<SoundEffect>(Path.Combine("sfx/" + sfxName)));
        }
        
        public void LoadObj()
        {
            //sfx
            LoadSfx("jump");
            LoadSfx("hit");
            LoadSfx("crystal");
            LoadSfx("changeblock");
            LoadSfx("levelselect");

            //map
            //LoadMap("level1");

            //song
            LoadSong("0");
        }

        public void PlayLevel (int index)
        {
            LoadMap(LevelList.list[index].map);
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

            if (mainmenu.Active)
            {
                mainmenu.Update(gameTime);
            }
            else
            {
                spriteManager.Update(gameTime);
            
                if (player.Texture != null)
                    spriteRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y,
                                   player.Texture.Width, player.Texture.Height);
                camera.Update(gameTime, this);
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
            // Render map

            //map.Draw(spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), viewportPosition);
            //map.Draw(spriteBatch, new Rectangle(0, 0, 200, 100), viewportPosition);
            // Render sprite

            if (mainmenu.Active)
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    mainmenu.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            } else {
                spriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    null, null, null, null,
                    camera.tranform
                    );
                spriteManager.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }


            base.Draw(gameTime);
        }
    }
}
