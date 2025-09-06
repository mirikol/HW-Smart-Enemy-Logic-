﻿namespace Labirint
{
    public class SmartEnemy : Unit
    {
        private Unit _target;
        private int[] dx = { -1, 0, 1, 0 };
        private int[] dy = { 0, 1, 0, -1 };

        public SmartEnemy(Vector2 startPosition, char symbol, ConsoleRenderer renderer, Unit target) :
            base(startPosition, symbol, renderer)
        {
            _target = target;
        }

        public void SetTarget(Unit? target)
        {
            if (target != null)
            {
                _target = target;
            }
        }
        public override void Update()
        {
            if (_target == null)
                return;

            List<Node> path = FindPath();

            if (path == null || path.Count <= 1)
                return;

            Node nextPosition = path[1];
            TryChangePosition(nextPosition.Position);
        }

        public List<Node>? FindPath()
        {
            if (_target == null)
            {
                return null;
            }

            Node startNode = new Node(Position);
            Node targetNode = new Node(_target.Position);

            List<Node> openList = new List<Node> { startNode };

            List<Node> closedList = new List<Node>();

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                foreach (var node in openList)
                {
                    if (node.Value < currentNode.Value)
                        currentNode = node;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.Position.Equals(targetNode.Position))
                {
                    List<Node> path = new List<Node>();

                    while (currentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode.Parent;
                    }

                    path.Reverse();
                    return path;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    int newX = currentNode.Position.X + dx[i];
                    int newY = currentNode.Position.Y + dy[i];

                    if (IsValid(newX, newY))
                    {
                        Node neighbor = new Node(new Vector2(newX, newY));

                        if (closedList.Contains(neighbor))
                            continue;

                        neighbor.Parent = currentNode;
                        neighbor.CalculateEstimate(targetNode.Position);
                        neighbor.CalculateValue();

                        openList.Add(neighbor);
                    }
                }
            }
            return null;
        }

        private bool IsValid(int x, int y)
        {
            char[,] map = GameData.GetInstance().GetMap();
            bool containsX = x >= 0 && x < map.GetLength(1);
            bool containsY = y >= 0 && y < map.GetLength(0);
            bool isNotWall = map[y, x] != '#';
            return containsX && containsY && isNotWall;
        }

        public override void Unsubscribe()
        {
            _target = null;
            base.Unsubscribe();
        }
    }
}