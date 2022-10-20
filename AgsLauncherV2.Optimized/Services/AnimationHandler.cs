using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AgsLauncherV2.Optimized.Services
{
    internal class AnimationHandler
    {
        /// <summary>
        /// Fades in an inputted XAML object
        /// </summary>
        /// <param name="targetObject">The object to fade in</param>
        /// <param name="timeToFade">The amount time to fade the object in</param>
        public static void FadeIn(DependencyObject targetObject, double timeToFade)
        {
            var b = targetObject;
            var fade = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(timeToFade),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }
        /// <summary>
        /// Fades out an inputted XAML object
        /// </summary>
        /// <param name="targetObject">The object to fade out</param>
        /// <param name="timeToFade">The amount time to fade the object out</param>
        public static void FadeOut(DependencyObject targetObject, double timeToFade)
        {
            var b = targetObject;
            var fade = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(timeToFade),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }

        /// <summary>
        /// Moves an XAML object to any position over a set amount of time
        /// </summary>
        /// <param name="targetObject">The object to move</param>
        /// <param name="time">The amount of time it will take for hte object to reach it's destination</param>
        /// <param name="from">The original position of the object. Can be object.Margin</param>
        /// <param name="to">The destination the object will get to at the end of the time inputted</param>
        public static void MovementAnimation(DependencyObject targetObject, double time, Thickness from, Thickness to)
        {
            var b = targetObject;
            var fade = new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.MarginProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }

        /// <summary>
        /// Animates color of a given XAML object
        /// </summary>
        /// <param name="targetObject">The object to animate color</param>
        /// <param name="time">The amount of time it will take for the object to reach the desired color</param>
        /// <param name="originatingColor">The origin color of the selected object</param>
        /// <param name="targetColor">The color the object will be at the end of the duration selected</param>
        /// <param name="dependencyProperty">The dependency PROPERTY of the object</param>
        public static void ColorAnimation(DependencyObject targetObject, Duration time, System.Windows.Media.Color originatingColor, System.Windows.Media.Color targetColor, DependencyProperty dependencyProperty)
        {
            ColorAnimation animation;
            animation = new ColorAnimation();
            animation.From = originatingColor;
            animation.To = targetColor;
            animation.Duration = time;
            Storyboard.SetTarget(animation, targetObject);
            Storyboard.SetTargetProperty(animation, new PropertyPath(targetObject));
            var sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Begin();
        }
    }
}
