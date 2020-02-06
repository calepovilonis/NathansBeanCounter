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
    class Bean 
    {
      GraphicsDeviceManager graphics;
      Texture2D texture;
      public BoundingRectangle bounds;
      Random random = new Random();
      int value;

      /// <summary>
      /// Creates a paddle
      /// </summary>
      /// <param name="game">The game this paddle belongs to</param>
      public Bean(GraphicsDeviceManager graphics)
      {
         this.graphics = graphics;
      }

      public void LoadContent(ContentManager content)
      {
         int myVal = random.Next(1, 9);
         value = myVal;
         texture = content.Load<Texture2D>("bean" + myVal.ToString());
         bounds.Width = 100;
         bounds.Height = 150;
         bounds.X = random.Next(0, (int)(graphics.GraphicsDevice.Viewport.Width - bounds.Width) - 274) * (float)random.NextDouble();
         bounds.Y = -150;
      }

      public void Update(GameTime gameTime)
      {

         bounds.Y += (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 2.5);

         if (bounds.Y > graphics.GraphicsDevice.Viewport.Height - bounds.Height)
         {
            //bounds.Y = graphics.GraphicsDevice.Viewport.Height - bounds.Height;
         }
      }

      public void Draw(SpriteBatch spriteBatch)
      {
         spriteBatch.Draw(texture, bounds, Color.White);
      }

      public float BoundsY => bounds.Y;

      public float BoundsHeight => bounds.Height;

      public int Value
      {
         get { return this.value; }
         set { this.value = value; }
      }
   }
}
