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

      Texture2D background;
      Texture2D nathanbean;
      Texture2D speachbubble;
      Texture2D shadow;

      private SpriteFont font;
      private SpriteFont score;

      List<Bean> beans = new List<Bean>();
      Basket basket;

      int userScore = 0;

      float timer = (float).85;
      const float TIMER = (float).85;

      private string x1 = "?";
      private string x2 = "?";
      private string mod;
      private int result;

      KeyboardState oldKeyboardState;
      KeyboardState newKeyboardState;

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
         basket = new Basket(this);
         calculateGoal();
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
         score = Content.Load<SpriteFont>("Score");

         background = Content.Load<Texture2D>("background");
         speachbubble = Content.Load<Texture2D>("speachbubble");
         shadow = Content.Load<Texture2D>("shadow");
         nathanbean = Content.Load<Texture2D>("nathanbean");
         // TODO: use this.Content to load your game content here
         foreach (Bean ball in beans)
         {
            ball.LoadContent(Content);
         }
         basket.LoadContent(Content);
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

         basket.Update(gameTime);

         for (int i = 0; i < beans.Count; i++)
         {
            if (basket.hitbox.CollidesWith(beans[i].bounds))
            {
               switch (mod)
               {
                  case "%":
                     if (beans[i].Value / Int32.Parse(x2) == result)
                     {
                        userScore += beans[i].Value + Int32.Parse(x2) + result;
                        calculateGoal();
                     }
                     else deductScore(i);
                     break;
                  case "-":
                     if (x1.Equals("?"))
                     {
                        if (beans[i].Value - Int32.Parse(x2) == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x2) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }
                     else if (!x1.Equals("?"))
                     {
                        if (Int32.Parse(x1) - beans[i].Value == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x1) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }

                     break;
                  case "*":
                     if (x1.Equals("?"))
                     {
                        if (beans[i].Value * Int32.Parse(x2) == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x2) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }
                     else if (!x1.Equals("?"))
                     {
                        if (beans[i].Value * Int32.Parse(x1) == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x1) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }
                     break;
                  case "+":
                     if (x1.Equals("?"))
                     {
                        if (beans[i].Value + Int32.Parse(x2) == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x2) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }
                     else if (!x1.Equals("?"))
                     {
                        if (beans[i].Value + Int32.Parse(x1) == result)
                        {
                           userScore += beans[i].Value + Int32.Parse(x1) + result;
                           calculateGoal();
                        }
                        else deductScore(i);
                     }
                     break;
                  default:
                     break;
               }

               beans.Remove(beans[i]);
            }
         }


         for (int i = 0; i < beans.Count; i++)
         {
            if (beans[i].BoundsY == graphics.GraphicsDevice.Viewport.Height) beans.Remove(beans[i]);
         }

         float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
         timer -= elapsed;
         if (timer < 0)
         {
            Bean newBall = new Bean(graphics);
            newBall.LoadContent(Content);
            beans.Add(newBall);
            timer = TIMER;
         }

         foreach (Bean ball in beans)
         {
            ball.Update(gameTime);
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
         spriteBatch.Draw(speachbubble, new Rectangle(graphics.GraphicsDevice.Viewport.Width - 274, graphics.GraphicsDevice.Viewport.Height / 2 - 250, 274, 233), Color.White);
         spriteBatch.Draw(shadow, new Rectangle(graphics.GraphicsDevice.Viewport.Width - 315, graphics.GraphicsDevice.Viewport.Height / 2 + 350, 274, 172), Color.White);
         spriteBatch.Draw(nathanbean, new Rectangle(graphics.GraphicsDevice.Viewport.Width - 274, graphics.GraphicsDevice.Viewport.Height / 2, 260, 315), Color.White);
         foreach (Bean ball in beans)
         {
            ball.Draw(spriteBatch);
         }
         basket.Draw(spriteBatch);
         string goal = x1 + " " + mod.ToString() + " " + x2 + " = " + result.ToString();
         spriteBatch.DrawString(font, goal, new Vector2(graphics.GraphicsDevice.Viewport.Width - 220, graphics.GraphicsDevice.Viewport.Height / 2 - 180), Color.Black);
         spriteBatch.DrawString(score, "Count: " + userScore, new Vector2(graphics.GraphicsDevice.Viewport.Width - 300, 50), Color.Black);
         spriteBatch.End();


         base.Draw(gameTime);
      }

      public void calculateGoal()
      {
         x1 = "?";
         x2 = "?";
         int val1 = random.Next(1, 9);
         int val2 = random.Next(1, 9);
         int whichMod = random.Next(1, 4);
         switch (whichMod)
         {
            case 1:
               result = val1 + val2;
               if (random.Next(1, 100) % 2 == 0) x1 = val1.ToString();
               else x2 = val2.ToString();
               mod = "+";
               break;
            case 2:
               result = val1 * val2;
               if (random.Next(1, 100) % 2 == 0) x1 = val1.ToString();
               else x2 = val2.ToString();
               mod = "*";
               break;
            
            case 3:
               if (val1 > val2)
               {
                  result = val1 / val2;
                  x2 = val2.ToString();
               }
               else
               {
                  result = val2 / val1;
                  x2 = val1.ToString();
               }
               mod = "%";
               break;
            case 4:
               if (val1 > val2)
               {
                  result = val1 - val2;
                  x1 = val1.ToString();
               }
               else
               {
                  result = val2 - val1;
                  x2 = val1.ToString();
               }
               mod = "-";
               break;

            default: break;
         }
      }

      public void deductScore(int i)
      {
         if (beans[i].bounds.Y >= graphics.GraphicsDevice.Viewport.Height - 50) beans[i].Value = 0;
         if (beans[i].Value == 1 && !(userScore == 0))
         {
            userScore--;
            return;
         }
         if (userScore - (beans[i].Value / 2) < 0)
         {
            userScore = 0;
         }
         else userScore -= (beans[i].Value / 2);
      }
   }
}
