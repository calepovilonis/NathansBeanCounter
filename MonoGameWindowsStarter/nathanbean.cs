using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
   public class Nathanbean
   {
      const int ANIMATION_FRAME_RATE = 150;
      const int FRAME_WIDTH = 400;
      const int FRAME_HEIGHT = 500;

      Game1 game;
      Texture2D texture;
      TimeSpan timer;
      int frame = 0;
      Vector2 position;
      Rectangle[] rectangles;

      public Nathanbean(Game1 graphics)
      {
         this.game = graphics;
         this.timer = new TimeSpan(0);
         //position = new Vector2(game.GraphicsDevice.Viewport.Width - 320, game.GraphicsDevice.Viewport.Height);
      }

      public void LoadContent()
      {
         texture = game.Content.Load<Texture2D>("beananimated");
         rectangles = new Rectangle[8];
         for (int i = 0; i < 8; i++)
         {
            rectangles[i] = new Rectangle((i) * FRAME_WIDTH, 0, FRAME_WIDTH, FRAME_HEIGHT);
         }
         position = new Vector2(700,540);
      }

      public void Update(GameTime gameTime)
      {
         timer += gameTime.ElapsedGameTime;

         // Determine the frame should increase.  Using a while 
         // loop will accomodate the possiblity the animation should 
         // advance more than one frame.
         while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
         {
            // increase by one frame
            frame++;
            // reduce the timer by one frame duration
            timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
         }

         // Keep the frame within bounds (there are four frames)
         frame %= 8;
      }

      public void Draw(SpriteBatch spriteBatch)
      {
         var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

         spriteBatch.Draw(texture, position, rectangles[frame], Color.White);
      }
   }
}
