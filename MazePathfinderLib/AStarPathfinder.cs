using System;
using System.Collections.Generic;
using LeePathfinderLib;

//Based on: http://blog.two-cats.com/2014/06/a-star-example/
namespace MazePathfinderLib
{
    //Todo: refactor
    public class AStarPathfinder
    {
        private readonly int _columnCount;
        private readonly Node _endNode;
        private readonly int _rowCount;
        private readonly Node _startNode;
        private readonly Node[,] _maze;
        private bool _foundWay;
        public bool IsOctaDirectional//returns if we can move on diagonals
        { get; private set;}
        public AStarPathfinder(int[,] maze, bool isOctaDirectional=false)
        {
            _rowCount = maze.GetLength(0);
            _columnCount = maze.GetLength(1);
            var end = new Cell();
            _maze = new Node[_rowCount, _columnCount];
            for (var i = 0; i < _rowCount; i++)
            {
                for (var j = 0; j < _columnCount; j++)
                {
                    if (maze[i, j] == 2)
                        end = new Cell(i, j);
                }
            }
            for (var i = 0; i < _rowCount; i++)
            {
                for (var j = 0; j < _columnCount; j++)
                {
                    _maze[i, j] = new Node(i, j, maze[i, j] != -1, end);
                    if (maze[i, j] == 1)
                        _startNode = _maze[i, j];
                }
            }
            _endNode = _maze[end.Row, end.Column];
            _startNode.State = Node.NodeState.Open;
        }

        public bool Solve()
        {
            if (!_foundWay)
                _foundWay = Search(_startNode);
            return _foundWay;
        }

        public IEnumerable<Cell> Bactkrace()
        {
            if(!_foundWay)
                throw new InvalidOperationException("There is no way from begginign to end, or Solve was not called");
            var path = new List<Cell>();
            var node = _endNode;
            while (node.ParentNode!=null)
            {
                path.Add(node.Location);
                node = node.ParentNode;
            }
            path.Add(_startNode.Location);
            path.Reverse();
            return path;
        }

        private IEnumerable<Cell> GetAdjacentLocations(Cell fromLocation)
        {
            if(!IsOctaDirectional)
            return new[]
            {
                new Cell(fromLocation.Row-1, fromLocation.Column),
                new Cell(fromLocation.Row,  fromLocation.Column+1),
                new Cell(fromLocation.Row+1, fromLocation.Column),
                new Cell(fromLocation.Row,   fromLocation.Column-1)
            };
            return new[]
            {
                new Cell(fromLocation.Row - 1, fromLocation.Column - 1),
                new Cell(fromLocation.Row - 1, fromLocation.Column),
                new Cell(fromLocation.Row - 1, fromLocation.Column + 1),
                new Cell(fromLocation.Row, fromLocation.Column + 1),
                new Cell(fromLocation.Row + 1, fromLocation.Column + 1),
                new Cell(fromLocation.Row + 1, fromLocation.Column),
                new Cell(fromLocation.Row + 1, fromLocation.Column - 1),
                new Cell(fromLocation.Row, fromLocation.Column - 1)
            };
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            var walkableNodes = new List<Node>();
            var nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                var row = location.Row;
                var column = location.Column;

                // Stay within the grid's boundaries
                if (row < 0 || row >= _rowCount || column < 0 || column >= _columnCount)
                    continue;
                var node = _maze[row, column];
                if (!node.IsWalkable)
                    continue;

                switch (node.State)
                {
                    case Node.NodeState.Closed:
                        continue;
                    case Node.NodeState.Open:
                        var traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                        var gTemp = fromNode.G + traversalCost;
                        if (gTemp < node.G)
                        {
                            node.ParentNode = fromNode;
                            walkableNodes.Add(node);
                        }
                        break;
                    default:
                        node.ParentNode = fromNode;
                        node.State = Node.NodeState.Open;
                        walkableNodes.Add(node);
                        break;
                }
            }
            return walkableNodes;
        }

        private bool Search(Node currentNode)
        {
            currentNode.State = Node.NodeState.Closed;
            var nextNodes = GetAdjacentWalkableNodes(currentNode);
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location.Equals(_endNode.Location))
                {
                    return true;
                }
                if (Search(nextNode))
                    return true;
            }
            return false;
        }
    }
}