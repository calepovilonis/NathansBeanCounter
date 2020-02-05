using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MonoGameWindowsStarter
{
   /// <summary>
   /// This is the main type for your game.
   /// </summary>
   public class Game1 : Game
   {
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;
      Random random = new Random();
      List<Ball> balls = new List<Ball>();
      Texture2D background;
      Texture2D nathanbean;

      float timer = (float).75;
      const float TIMER = (float).75;

      Vector2 ballPosition = Vector2.Zero;
      Vector2 ballVelocity;

      private SpriteFont font;
      private string x1 = "?";
      private string x2 = "?";
      private string mod;
      private int result;

      Paddle paddle;

      KeyboardState oldKeyboardState;
      KeyboardState newKeyboardState;

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
         paddle = new Paddle(this);
         calculateGoal();
      }

      public void calculateGoal ()
      {
         int val1 = random.Next(1, 9);
         int val2 = random.Next(1, 9);
         int whichMod = random.Next(1, 4);
         switch (whichMod)
         {
            case 1:
               result = val1 + val2;
               mod = "+";
               break;
            case 2:
               if (val1 > val2)
                  result = val1 - val2;
               else result = val2 - val1;
               mod = "-";
               break;
            case 3:
               result = val1 + val2;
               mod = "*";
               break;
            case 4:
               if (val1 > val2)
                  result = val1 / val2;
               else result = val2 / val1;
               mod = "%";
               break;
            default: break;
         }
            
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
         graphics.PreferredBackBufferWidth = 1042;
         graphics.PreferredBackBufferHeight = 1042;
         graphics.ApplyChanges();

         ballVelocity = new Vector2(
             (float)random.NextDouble(),
             (float)random.NextDouble()
         );
         ballVelocity.Normalize();

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
         font = Content.Load<SpriteFont>("Goal");

         background = Content.Load<Texture2D>("background");
         nathanbean = Content.Load<Texture2D>("nathanbean");
         // TODO: use this.Content to load your game content here
         foreach (Ball ball in balls)
         {
            ball.LoadContent(Content);
         }
         paddle.LoadContent(Content);
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         newKeyboardState = Keyboard.GetState();

         if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

         if (newKeyboardState.IsKeyDown(Keys.Escape))
            Exit();

         for (int i = 0; i < balls.Count; i++)
         {
            if (balls[i].BoundsY == graphics.GraphicsDevice.Viewport.Height - balls[i].BoundsHeight) balls.Remove(balls[i]);
         }

         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
         timer -= elapsed;
         if (timer < 0)
         {
            Ball newBall = new Ball(graphics);
            newBall.LoadContent(Content);
            balls.Add(newBall);
            timer = TIMER;
         }

         foreach (Ball ball in balls)
         {
            ball.Update(gameTime);
         }

         paddle.Update(gameTime);

         // TODO: Add your update logic here
         ballPosition.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * ballVelocity.Y;

         // Check for wall collisions
         if (ballPosition.Y < 0)
         {
            ballVelocity.Y *= -1;
            float delta = 0 - ballPosition.Y;
            ballPosition.Y += 2 * delta;
         }

         if (ballPosition.Y > graphics.PreferredBackBufferHeight - 100)
         {
            ballVelocity.Y *= -1;
            float delta = graphics.PreferredBackBufferHeight - 100 - ballPosition.Y;
            ballPosition.Y += 2 * delta;
         }

         if (ballPosition.X < 0)
         {
            ballVelocity.X *= -1;
            float delta = 0 - ballPosition.X;
            ballPosition.X += 2 * delta;
         }

         if (ballPosition.X > graphics.PreferredBackBufferWidth - 100)
         {
            ballVelocity.X *= -1;
            float delta = graphics.PreferredBackBufferWidth - 100 - ballPosition.X;
            ballPosition.X += 2 * delta;
         }

         oldKeyboardState = newKeyboardState;
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
         spriteBatch.Begin();
         spriteBatch.Draw(background, new Rectangle(-200, 0, graphics.GraphicsDevice.Viewport.Width + 350, graphics.GraphicsDevice.Viewport.Height), Color.White);
         spriteBatch.Draw(nathanbean, new Rectangle(graphics.GraphicsDevice.Viewport.Width - 274, graphics.GraphicsDevice.Viewport.Height / 2, 264 , 384), Color.White);
         foreach (Ball ball in balls)
         {
            ball.Draw(spriteBatch);
         }
         paddle.Draw(spriteBatch);
         string goal = x1 + " " + mod.ToString() + " " + x2 + " = " + result.ToString();
         spriteBatch.DrawString(font, goal, new Vector2(graphics.GraphicsDevice.Viewport.Width - 274, 75), Color.Black);
         spriteBatch.End();


         base.Draw(gameTime);
      }
   }
}
