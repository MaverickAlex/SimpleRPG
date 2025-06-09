using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SimpleRPG;

public class Projectile
{
  public static List<Projectile> Projectiles = [];
  private Vector2 _position;
  private int _speed = 1000;
  public int _radius = 18;
  private Dir _direction;

  public Projectile(Vector2 position, Dir direction)
  {
    _position = position;
    _direction = direction;
  }
  public bool Collided { get; set; } = false;

  public Vector2 Position => _position;

  public void Update(GameTime gameTime)
  {
    switch (_direction)
    {
      case Dir.Left:
        _position.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        break;
      case Dir.Right:
        _position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        break;
      case Dir.Up:
        _position.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        break;
      case Dir.Down:
        _position.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}
