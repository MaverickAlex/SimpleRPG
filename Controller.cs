using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SimpleRPG;

public class Controller
{
  public static double timer = 2D;
  public static double maxTime = 2D;

  public static void Update(GameTime gameTime, Texture2D spriteSheet)
  {
    timer -= gameTime.ElapsedGameTime.TotalSeconds;
    if (timer <= 0)
    {
      int side = Random.Shared.Next(4);
      Vector2 pos = new Vector2(0, 0);
      switch (side)
      {
        case 0:
          pos = new Vector2(-500, Random.Shared.Next(-500, 2000));
          break;
        case 1:
          pos = new Vector2(2000, Random.Shared.Next(-500, 2000));
          break;
        case 2:
          pos = new Vector2(Random.Shared.Next(-500, 2000), -500);
          break;
        case 3:
          pos = new Vector2(Random.Shared.Next(-500, 2000), 2000);
          break;
      }
      Enemy.Enemies.Add(new Enemy(pos, spriteSheet));
      timer = maxTime;
      if (maxTime > 0.5)
      {
        maxTime -= 0.05D;
      }
    }
  }
}
