using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

// Коректний в даному випадку namespace буде Zuma.GameEngine.Auxiliary_Classes.
namespace Zuma.GameEngine
{	
    // Доцільно клас зробити статичним, оскільки в ньому лише один статичний метод.
	public class Bezier
	{
		public static List<PointF> AbsoluteBezierPoints(string str, bool svgOriginalCulture)
		{
            if (svgOriginalCulture)
            {
                StringBuilder tmpStr = new StringBuilder(str);
                tmpStr.Replace(',', ':');
                tmpStr.Replace('.', ',');

                str = tmpStr.ToString();
            }
			
            // Використати константу.
			const string matchStartPoint = @"(M|m) ((-?\d+,?\d*):(-?\d+,?\d*) )+";
			const string matchPointSequence = @"(C|c) ((-?\d+,?\d*):(-?\d+,?\d*) )+";
			
			List<PointF> result = new List<PointF>();
			
			PointF[] pointsArray;
			
			MatchCollection startPoint = Regex.Matches(str, matchStartPoint);
			pointsArray = PointF.Points(startPoint[0].Value, false);
			result.Add(pointsArray[0]);
			
			MatchCollection points = Regex.Matches(str, matchPointSequence);
			foreach(Match m in points)
			{
                // Пропущено фігурні дужки.
			    if (m.Value.StartsWith("C "))
			    {
			        result.AddRange(PointF.Points(m.Value, false));
			    }
			    else
			    {
			        pointsArray = PointF.Points(m.Value, false);
			        var tmp = result[result.Count - 1];
			        var tmpSize = new SizeF();
                    // В даному випадку доцільніше використати var.
			        for (var i = 0; i < pointsArray.Length; i++)
			        {
			            tmpSize.Width = pointsArray[i].X;
			            tmpSize.Height = pointsArray[i].Y;
			            result.Add(PointF.Add(tmp, tmpSize));
                        // Пропущено фігурні дужки
			            if ((i + 1)%3 == 0)
			            {
			                tmp = result[result.Count - 1];
			            }
			        }
			    }
			}
			
			return result;
		}
	}
}