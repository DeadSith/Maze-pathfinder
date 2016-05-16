using System;

namespace LeePathfinderLib
{
    public class Node
    {
        public enum NodeState
        {
            Untested,
            Open,
            Closed
        }

        public float F => G + H;
        public NodeState State { get; set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                _parentNode = value;
                G = _parentNode.G + GetTraversalCost(Location, _parentNode.Location);
            }
        }
        internal readonly bool IsWalkable;
        internal Cell Location;
        private Node _parentNode;


        public Node(int row, int column, bool isWalkable, Cell endLocation)
        {
            Location = new Cell(row, column);
            State = NodeState.Untested;
            IsWalkable = isWalkable;
            H = GetTraversalCost(Location, endLocation);
            G = 0;
        }

        internal static float GetTraversalCost(Cell location, Cell otherLocation)
        {
            float deltaX = otherLocation.Row - location.Row;
            float deltaY = otherLocation.Column - location.Column;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}