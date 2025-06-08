using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SimpleRPG;

public class Player
{
  private Vector2 _position = new Vector2(500, 300);
  private int _speed = 300;
  private Dir _direction = Dir.Down;
  private bool _isMoving = false;
  public SpriteAnimation animation;

  public SpriteAnimation[] Animations = new SpriteAnimation[4];

  public Vector2 Position => _position;

  public void SetX(int x)
  {
    _position.X = x;
  }
  public void SetY(int y)
  {
    _position.Y = y;
  }

  public void Update(GameTime gameTime)
  {
    _isMoving = false;
    KeyboardState keyboardState = Keyboard.GetState();
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    if (keyboardState.IsKeyDown(Keys.Right))
    {
      _direction = Dir.Right;
      _isMoving = true;
    }

    if (keyboardState.IsKeyDown(Keys.Left))
    {
      _direction = Dir.Left;
      _isMoving = true;
    }

    if (keyboardState.IsKeyDown(Keys.Up))
    {
      _direction = Dir.Up;
      _isMoving = true;
    }

    if (keyboardState.IsKeyDown(Keys.Down))
    {
      _direction = Dir.Down;
      _isMoving = true;
    }

    if (_isMoving)
    {

      switch (_direction)
      {
        case Dir.Left:
          _position.X -= _speed * dt;
          break;
        case Dir.Right:
          _position.X += _speed * dt;
          break;
        case Dir.Up:
          _position.Y -= _speed * dt;
          break;
        case Dir.Down:
          _position.Y += _speed * dt;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    animation = Animations[(int)_direction];
    animation.Position = new Vector2(_position.X - 48, _position.Y - 48);
    if (_isMoving)
    {
      animation.Update(gameTime);
    }
    else
    {
      animation.setFrame(1);
    }
  }
}
