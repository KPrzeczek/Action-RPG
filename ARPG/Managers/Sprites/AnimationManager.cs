using ARPG.Models.Sprites.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARPG.Managers.Sprites
{
    public class AnimationManager : ICloneable
    {
        private Animation animation;
        private float timer;

        private bool stopped = false;

        public Animation CurrentAnimation => animation;

        public float Layer { get; set; }
        public Vector2 Origin { get; set; }
		
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }

        public AnimationManager(Animation anim)
        {
            animation = anim;
            Scale = 1f;
        }

        public void Update(float deltaTime)
        {
            if(!stopped)
            {
                timer += deltaTime;

                if(timer > animation.FrameSpeed && animation.IsLooping)
                {
                    timer = 0f;
                    animation.CurrentFrame++;

                    if(animation.CurrentFrame >= animation.FrameCount)
                        animation.CurrentFrame = 0;
                }
            }
        }
		
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                animation.Texture,
                Position,
                new Rectangle(
                    animation.CurrentFrame * animation.FrameWidth,
                    0,
                    animation.FrameWidth,
                    animation.FrameHeight
                ),
                Color.White,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                Layer
            );
        }

        public void Play(Animation anim)
        {
            if(animation == anim)
                return;

            animation = anim;
            animation.CurrentFrame = 0;
            timer = 0;
        }

        public void Play()
        {
            stopped = false;
        }

        public void Stop()
        {
            stopped = true;
        }

        public void Restart()
        {
            timer = 0f;
            animation.CurrentFrame = 0;
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as AnimationManager;
            animationManager.animation = animationManager.animation.Clone() as Animation;
            return animationManager;
        }
    }
}
