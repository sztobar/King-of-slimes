using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public static class DerivativeHelpers
  {
    public delegate float Curve(float t);

    // Gives best results so far
    public static float ForwardDerivative(float t, float duration, Curve curve)
    {
      float dt = Time.deltaTime;
      if (dt == 0 || duration == 0)
        return 0;

      float t_next = t + dt;
      if (t >= duration)
      {
        return BackwardDerivative(t, duration, curve);
      }
      else
      {
        float x = curve(t / duration);
        float x_next = curve(t_next / duration);
        return (x_next - x) / dt;
      }
    }

    public static float BackwardDerivative(float t, float duration, Curve curve)
    {
      float dt = Time.deltaTime;
      if (dt == 0 || duration == 0)
        return 0;

      if (t < dt)
      {
        return ForwardDerivative(t, duration, curve);
      }
      else
      {
        float t_prev = t - dt;
        float x = curve(t / duration);
        float x_prev = curve(t_prev / duration);
        return (x - x_prev) / dt;
      }
    }

    public static float SymmetricDerivative(float t, float duration, Curve curve)
    {
      float dt = Time.deltaTime;
      if (dt == 0 || duration == 0)
        return 0;

      if (t < dt)
      {
        return ForwardDerivative(t, duration, curve);
      }
      else if (t >= duration)
      {
        return BackwardDerivative(t, duration, curve);
      }
      else
      {
        float t_prev = t - dt;
        float t_next = t + dt;
        float x_prev = curve(t_prev / duration);
        float x_next = curve(t_next / duration);
        return (x_next - x_prev) / (2 * dt);
      }
    }

    public static float CentralDerivative(float t, float duration, Curve curve)
    {
      float dt = Time.deltaTime;
      if (dt == 0 || duration == 0)
        return 0;

      if (t < dt)
      {
        return ForwardDerivative(t, duration, curve);
      }
      else if (t >= duration)
      {
        return BackwardDerivative(t, duration, curve);
      }
      else
      {
        float half_dt = dt / 2;
        float t_next = t + half_dt;
        float t_prev = t - half_dt;
        float x_prev = curve(t_prev / duration);
        float x_next = curve(t_next / duration);
        return (x_next - x_prev) / dt;
      }
    }
  }
}