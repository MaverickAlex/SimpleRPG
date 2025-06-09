using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SimpleRPG;

public static class MySounds
{
  public static SoundEffect ProjectileSound;
  public static Song Music;
}
public enum Dir
{
  Down,
  Up,
  Left,
  Right
}
public class Game1 : Game
{
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;

  private Texture2D _playerSprite;
  private Texture2D _walkDown;
  private Texture2D _walkLeft;
  private Texture2D _walkRight;
  private Texture2D _walkUp;

  private Texture2D _background;
  private Texture2D _ball;
  private Texture2D _skull;

  Player _player = new Player();

  private Camera _camera;
  public Game1()
  {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    _graphics.PreferredBackBufferWidth = 1280;
    _graphics.PreferredBackBufferHeight = 720;
    _graphics.ApplyChanges();
    _camera = new Camera(_graphics.GraphicsDevice);
    base.Initialize();
  }

  protected override void LoadContent()
  {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    _playerSprite = Content.Load<Texture2D>("Player/player");
    _walkDown = Content.Load<Texture2D>("Player/walkDown");
    _walkLeft = Content.Load<Texture2D>("Player/walkLeft");
    _walkRight = Content.Load<Texture2D>("Player/walkRight");
    _walkUp = Content.Load<Texture2D>("Player/walkUp");
    _background = Content.Load<Texture2D>("background");
    _ball = Content.Load<Texture2D>("ball");
    _skull = Content.Load<Texture2D>("skull");

    MySounds.ProjectileSound = Content.Load<SoundEffect>("Sounds/blip");
    MySounds.Music = Content.Load<Song>("Sounds/nature");
    MediaPlayer.Play(MySounds.Music);
    _player.Animations[0] = new SpriteAnimation(_walkDown, 4, 8);
    _player.Animations[1] = new SpriteAnimation(_walkUp, 4, 8);
    _player.Animations[2] = new SpriteAnimation(_walkLeft, 4, 8);
    _player.Animations[3] = new SpriteAnimation(_walkRight, 4, 8);
    _player.animation = _player.Animations[0];
  }

  protected override void Update(GameTime gameTime)
  {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    _player.Update(gameTime);
    if(!_player.Dead)
      Controller.Update(gameTime, _skull);
    _camera.Position = _player.Position;
    _camera.Update(gameTime);

    foreach (Projectile projectile in Projectile.Projectiles)
    {
      projectile.Update(gameTime);
    }

    foreach (Enemy enemy in Enemy.Enemies)
    {
      enemy.Update(gameTime, _player.Position, _player.Dead);
      int sum = 32 + enemy._radius;
      if (Vector2.Distance(_player.Position, enemy.Position) < sum)
      {
        _player.Dead = true;
      }
    }

    foreach (Projectile projectile in Projectile.Projectiles)
    {
      foreach (Enemy enemy in Enemy.Enemies)
      {
        int sum = projectile._radius + enemy._radius;
        if (Vector2.Distance(projectile.Position, enemy.Position) < sum)
        {
          projectile.Collided = true;
          enemy.IsDead = true;
        }
      }
    }
    Projectile.Projectiles.RemoveAll(x => x.Collided);
    Enemy.Enemies.RemoveAll(x => x.IsDead);

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin(_camera);
    _spriteBatch.Draw(_background, new Vector2(-500, -500), Color.White);

    foreach (Enemy enemy in Enemy.Enemies)
    {
      enemy.animation.Draw(_spriteBatch);
    }
    foreach (Projectile projectile in Projectile.Projectiles)
    {
      _spriteBatch.Draw(_ball, new Vector2(projectile.Position.X - 48, projectile.Position.Y - 48), Color.White);
    }
    if(!_player.Dead)
      _player.animation.Draw(_spriteBatch);
    _spriteBatch.End();

    base.Draw(gameTime);
  }
}
