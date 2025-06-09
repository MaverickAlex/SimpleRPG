using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SimpleRPG;

public class Enemy
{
  public static List<Enemy> Enemies = [];
  private Vector2 _position = Vector2.Zero;
  int _speed = 150;
  public int _radius = 30;

  public bool IsDead { get; set; } = false;

  public SpriteAnimation animation;
  public Enemy(Vector2 position, Texture2D spriteSheet)
  {
    _position = position;
    animation = new SpriteAnimation(spriteSheet, 10, 6);
  }
  public Vector2 Position => _position;

  public void Update(GameTime gameTime, Vector2 playerPosition)
  {
    animation.Update(gameTime);
    animation.Position = new Vector2(_position.X - 48, _position.Y - 66);
    Vector2 moveDir = playerPosition - _position;
    moveDir.Normalize();
    _position += moveDir * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
  }


}
