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
  private KeyboardState _keyboardStateOld = Keyboard.GetState();

  public SpriteAnimation[] Animations = new SpriteAnimation[4];

  public Vector2 Position => _position;

  public bool Dead { get; set; } = false;

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

    if(keyboardState.IsKeyDown(Keys.Space))
      _isMoving=false;

    if (Dead)
      _isMoving = false;

    if (_isMoving)
    {
      switch (_direction)
      {
        case Dir.Left:
          if (_position.X > 225)
            _position.X -= _speed * dt;
          break;
        case Dir.Right:
          if( _position.X < 1275)
            _position.X += _speed * dt;
          break;
        case Dir.Up:
          if( _position.Y > 200)
            _position.Y -= _speed * dt;
          break;
        case Dir.Down:
          if( _position.Y < 1250)
            _position.Y += _speed * dt;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    animation = Animations[(int)_direction];
    animation.Position = new Vector2(_position.X - 48, _position.Y - 48);
    if (keyboardState.IsKeyDown(Keys.Space))
    {
      animation.setFrame(0);
    }
    else if (_isMoving)
    {
      animation.Update(gameTime);
    }
    else
    {
      animation.setFrame(1);
    }

    if (_keyboardStateOld.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
    {
      Projectile.Projectiles.Add(new Projectile(_position, _direction));
      MySounds.ProjectileSound.Play(1f,0.5f,0f);
    }
    _keyboardStateOld = keyboardState;
  }
}
