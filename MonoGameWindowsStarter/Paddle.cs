using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
   public class Paddle
   {
      Game1 game;

      public BoundingRectangle bounds;
      public BoundingRectangle hitbox;

      Texture2D texture;

      /// <summary>
      /// Creates a paddle
      /// </summary>
      /// <param name="game">The game this paddle belongs to</param>
      public Paddle(Game1 game)
      {
         this.game = game;
      }

      public void LoadContent(ContentManager content)
      {
         texture = content.Load<Texture2D>("basket");
         bounds.Width = 200;
         bounds.Height = 136;
         bounds.X = game.GraphicsDevice.Viewport.Width / 2 - 274;
         bounds.Y = game.GraphicsDevice.Viewport.Height;
         hitbox.Width = 170;
         hitbox.Height = 70;
         hitbox.X = game.GraphicsDevice.Viewport.Width / 2 - 259;
         hitbox.Y = game.GraphicsDevice.Viewport.Height - 15;
      }

      public void Update(GameTime gameTime)
      {
         var keyboardState = Keyboard.GetState();
         /*
         if (keyboardState.IsKeyDown(Keys.Up))
         {
             // move up
             bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
         }


         if (keyboardState.IsKeyDown(Keys.Down))
         {
             // move down
             bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
         }
         */

         if (keyboardState.IsKeyDown(Keys.Left))
         {
            // move left
            var elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            bounds.X -= elapsed;
            hitbox.X -= elapsed;
         }


         if (keyboardState.IsKeyDown(Keys.Right))
         {
            // move right
            var elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            bounds.X += elapsed;
            hitbox.X += elapsed;
         }

         if (bounds.Y < 0)
         {
            bounds.Y = 0;
         }
         if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
         {
            bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
         }
         if (bounds.X < 0)
         {
            bounds.X = 0;
            hitbox.X = 15;
         }
         if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width - 274)
         {
            bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width - 274;
            hitbox.X = game.GraphicsDevice.Viewport.Width - bounds.Width - 259; 
         }
      }

      public void Draw(SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, bounds, Color.White);
      }
   }
}
