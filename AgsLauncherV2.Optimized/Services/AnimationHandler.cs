/*
 * AveryGame Launcher
 *  Copyright (C) 2022, Avery Fiebig-Dorey & Tristan Gee
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace AgsLauncherV2.Optimized.Services
{
    internal static class AnimationHandler
    {
        /// <summary>
        /// Fades in an inputted XAML object
        /// </summary>
        /// <param name="targetObject">The object to fade in</param>
        /// <param name="timeToFade">The amount time to fade the object in</param>
        public static void FadeIn(DependencyObject targetObject, double timeToFade)
        {
            var fadeC = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(timeToFade),
            };
            Storyboard.SetTarget(fadeC, targetObject);
            Storyboard.SetTargetProperty(fadeC, new PropertyPath(UIElement.OpacityProperty));
            var sbC = new Storyboard();
            sbC.Children.Add(fadeC);
            sbC.Begin();
        }

        /// <summary>
        /// Fades an inputted XAML object to any opacity
        /// </summary>
        /// <param name="targetObject">The object to fade in</param>
        /// <param name="timeToFade">The amount time to fade the object in</param>
        /// <param name="originatingOpacity">The opacity at which the animation will start</param>
        /// <param name="targetOpacity">The opacity at which the animation will end</param>
        public static void FadeAnimation(DependencyObject targetObject, double timeToFade, double originatingOpacity, double targetOpacity)
        {
            var fade = new DoubleAnimation()
            {
                From = originatingOpacity,
                To = targetOpacity,
                Duration = TimeSpan.FromSeconds(timeToFade),
            };
            Storyboard.SetTarget(fade, targetObject);
            Storyboard.SetTargetProperty(fade, new PropertyPath(UIElement.OpacityProperty));
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
            var fade = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(timeToFade),
            };
            Storyboard.SetTarget(fade, targetObject);
            Storyboard.SetTargetProperty(fade, new PropertyPath(UIElement.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }
    }

    public class BrushAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(Brush);

        public override object GetCurrentValue(object defaultOriginValue,
                                               object defaultDestinationValue,
                                               AnimationClock animationClock)
        {
            return GetCurrentValue(defaultOriginValue as Brush,
                                   defaultDestinationValue as Brush,
                                   animationClock);
        }

        private object GetCurrentValue(Brush defaultOriginValue,
                                      Brush defaultDestinationValue,
                                      Clock animationClock)
        {
            if (!animationClock.CurrentProgress.HasValue)
                return Brushes.Transparent;
            defaultOriginValue = this.From ?? defaultOriginValue;
            defaultDestinationValue = this.To ?? defaultDestinationValue;
            return animationClock.CurrentProgress.Value switch
            {
                0 => defaultOriginValue,
                1 => defaultDestinationValue,
                _ => new VisualBrush(new Border()
                {
                    Width = 1,
                    Height = 1,
                    Background = defaultOriginValue,
                    Child = new Border()
                    {
                        Background = defaultDestinationValue, Opacity = animationClock.CurrentProgress.Value,
                    }
                })
            };
        }
        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }
        public Brush From
        {
            get => (Brush)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public Brush To
        {
            get => (Brush)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(Brush), typeof(BrushAnimation));
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(Brush), typeof(BrushAnimation));
    }
}