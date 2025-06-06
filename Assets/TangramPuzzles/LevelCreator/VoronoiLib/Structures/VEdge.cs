﻿namespace VoronoiLib.Structures
{
    public class VEdge
    {
        public VPoint Start { get; internal set; }
        public VPoint End { get; internal set; }
		public FortuneSite Left { get; set; }
		public FortuneSite Right { get; set; }
		internal double SlopeRise { get; set; }
		internal double SlopeRun { get; set; }
		internal double? Slope { get; set; }
		internal double? Intercept { get; set; }

        public VEdge Neighbor { get; internal set; }

        internal VEdge(VPoint start, FortuneSite left, FortuneSite right)
        {
            Start = start;
            Left = left;
            Right = right;
            
            //for bounding box edges
            if (left == null || right == null)
                return;

            //from negative reciprocal of slope of line from left to right
            //ala m = (left.y -right.y / left.x - right.x)
            SlopeRise = left.X - right.X;
            SlopeRun = -(left.Y - right.Y);
            Intercept = null;

            if (SlopeRise.ApproxEqual(0) || SlopeRun.ApproxEqual(0)) return;
            Slope = SlopeRise/SlopeRun;
            Intercept = start.Y - Slope*start.X;
        }
    }
}
