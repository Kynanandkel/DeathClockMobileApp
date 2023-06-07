using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace DeathClock.Renderers
{
	public class LoadingBarRenderer : IRenderer
	{
        

        public event EventHandler RefreshRequested;

        public float _Percentage = 0;
        public float Percentage
        {
            get => _Percentage;

            set
            {
                if (value >= 100)
                {
                    _Percentage = 100;
                }
                else if (value <= 0)
                {
                    _Percentage = 0;
                }
                else
                {
                    _Percentage = value;
                }
                RefreshRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        public void PaintSurface(SKSurface surface, SKImageInfo info)
        {
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPath path = new SKPath())
            {
                int lineWidth = 10;
                int border = 40 ;
                path.MoveTo(new SKPoint(border, border - lineWidth));

                path.LineTo(new SKPoint(info.Width - border, border - lineWidth)); //border
                path.MoveTo(new SKPoint(info.Width / 2, border - lineWidth));
                path.LineTo(new SKPoint(info.Width / 2, info.Height - (border - lineWidth)));

                path.MoveTo(new SKPoint(border, info.Height - (border - lineWidth))); //info.height - border
                path.LineTo(new SKPoint(info.Width - border, info.Height - (border - lineWidth)));

                SKPaint whiteLinePaint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.White,
                    StrokeWidth = 10

                };

                canvas.DrawPath(path, whiteLinePaint);

                SKPaint GrayLinePaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.LightGray,
                    StrokeWidth = 10

                };

                

                float offset = 40;

                float HeightOfLoadingBar = (float)(info.Height - border*2);

                float LoadinBarSplitByOnePercent = (float)HeightOfLoadingBar / 100;

                float positionOfCircle = (float)LoadinBarSplitByOnePercent * (float)Percentage + offset ;

                canvas.DrawCircle(info.Width/2,positionOfCircle, 10,GrayLinePaint);
            }
        }

    }

    
}

