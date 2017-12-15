using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maria.Models;
using Maria.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Maria.Enum;

namespace Maria.Sprites
{
    public class Player : Sprite
    {

        public int score;

        public float Speed = 1f;
        public EBlock blockType;

        private Vector2 gravityVelocity = new Vector2(0, 0);

        public override Rectangle Rectangle()
        {
            return new Rectangle((int)Position.X + 4, (int)Position.Y, (animations.ElementAt(0).Value.Texture.Width / animations.ElementAt(0).Value.FrameCount) - 4, animations.ElementAt(0).Value.Texture.Height);
        }

        public Player(Dictionary<string, Animation> _animation, float _gravity)
           : base(_animation, _gravity)
        {
            animations = _animation;
            animationManager = new AnimationManager(animations.First().Value);
            gravity = _gravity;
        }

        public Player(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            SetAnimation();
            if (animations != null)
                animationManager.Update(gameTime);

            Physics(sprites);

            if (Keyboard.GetState().IsKeyDown(Input.Right))
                translation.X = Speed;   
            else if (Keyboard.GetState().IsKeyDown(Input.Left)) translation.X = -Speed;
            else translation.X = 0;

            Changeblock();
            translation.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Jump) && grounded)
                Jump(1.4f);
            if (Position.Y > 1000)
                Die();
        }

        public void Reset ()
        {
            blockType = EBlock.A;
            gravityVelocity = Vector2.Zero;
            jumpForce = 0;
            score = 0;
        }

        public void Changeblock()
        {
            if(Keyboard.GetState().IsKeyDown(Input.ChangeBlockA))
            {
                if (blockType != EBlock.A)
                    Game1.Instance.soundeffects[3].Play();
                blockType = EBlock.A;
                
            }
            if (Keyboard.GetState().IsKeyDown(Input.ChangeBlockB))
            {
                if (blockType != EBlock.B)
                    Game1.Instance.soundeffects[3].Play();
                blockType = EBlock.B;
                
            }
            if (Keyboard.GetState().IsKeyDown(Input.ChangeBlockC))
            {
                if (blockType != EBlock.C)
                    Game1.Instance.soundeffects[3].Play();
                blockType = EBlock.C;
            }
            
        }

        public void Die ()
        {
            Game1.Instance.soundeffects[1].Play();
            Position = Vector2.Zero;
            blockType = EBlock.A;
        }

        /**
         *  When It toching sprite from that direction
         */
        public override void OnTouchingLeft(Sprite sprite) {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType == blockType) Die();
            if (sprite.GetType() == typeof(Coin)) ((Coin)sprite).Collect();
        }
        public override void OnTouchingRight(Sprite sprite) {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType == blockType) Die();
            if (sprite.GetType() == typeof(Coin)) ((Coin)sprite).Collect();
        }
        public override void OnTouchingTop(Sprite sprite) {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType == blockType) Die();
            if (sprite.GetType() == typeof(Coin)) ((Coin)sprite).Collect();
        }
        public override void OnTouchingBottom(Sprite sprite) {
            if (sprite.GetType() == typeof(Coin)) ((Coin)sprite).Collect();
        }

        protected virtual bool IsTouchingLeft(Sprite sprite)
        {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType != blockType) return false;
            return IsTouchingLeft(sprite);
        }

        protected virtual bool IsTouchingRight(Sprite sprite)
        {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType != blockType) return false;

            return IsTouchingRight(sprite);
        }

        protected override bool IsTouchingTop(Sprite sprite)
        {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType != blockType) return false;

            return base.IsTouchingTop(sprite);
        }

        protected override bool IsTouchingBottom(Sprite sprite)
        {
            if (sprite.GetType() == typeof(Block) && ((Block)sprite).blockType != blockType) return false;

            return base.IsTouchingBottom(sprite);
        }

    }
}
