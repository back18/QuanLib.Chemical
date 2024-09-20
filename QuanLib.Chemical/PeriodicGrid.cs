using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public static class PeriodicGrid
    {
        public const int ATOMICNUMBER_MIN = 1;

        public const int ATOMICNUMBER_MAX = 118;

        public const int LANTHANIDE_ATOMICNUMBER_START = 57;

        public const int LANTHANIDE_ATOMICNUMBER_END = 71;

        public const int ACTINIDE_ATOMICNUMBER_START = 89;

        public const int ACTINIDE_ATOMICNUMBER_END = 103;

        static PeriodicGrid()
        {
            List<Position> positions = [];
            foreach (var position in new Enumerable())
                positions.Add(position);
            _positions = positions.ToArray();
        }

        private static readonly Position[] _positions;

        public static Position GetPosition(ElementSymbol symbol)
        {
            return _positions[(int)symbol - 1];
        }

        public static Block GetBlock(ElementSymbol symbol)
        {
            Position position = GetPosition(symbol);
            if (position.Group <= 2)
            {
                return Block.S;
            }
            else if (position.Group <= 10)
            {
                if (position == Position.LanthanidePosition &&
                    symbol != ElementSymbol.La ||
                    position == Position.ActinidePosition &&
                    symbol != ElementSymbol.Ac)
                    return Block.F;
                else
                    return Block.D;
            }
            else if (position.Group <= 12)
            {
                return Block.DS;
            }
            else
            {
                if (symbol == ElementSymbol.He)
                    return Block.S;
                else
                    return Block.P;
            }
        }

        private struct Enumerable : IEnumerable<Position>
        {
            public IEnumerator<Position> GetEnumerator()
            {
                return new Enumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private struct Enumerator : IEnumerator<Position>
        {
            static Enumerator()
            {
                _goto = [];
                _goto.Add(new(1, 2), new(1, 18));
                _goto.Add(new(2, 3), new(2, 13));
                _goto.Add(new(3, 3), new(3, 13));
                _goto.Add(new(Position.EndPosition.Period, Position.EndPosition.Group + 1), Position.StartPosition);
                for (int period = Position.StartPosition.Period; period < Position.EndPosition.Period; period++)
                    _goto.Add(new(period, Position.EndPosition.Group + 1), new(period + 1, 1));
            }

            public Enumerator()
            {
                _isComplete = false;
                _atomicNumber = ATOMICNUMBER_MAX;
                Current = Position.EndPosition;
            }

            private static readonly Dictionary<Position, Position> _goto;

            private bool _isComplete;

            private int _atomicNumber;

            public Position Current { get; private set; }

            readonly object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _atomicNumber++;
                if (_atomicNumber > ATOMICNUMBER_MAX)
                {
                    if (_isComplete)
                        return false;

                    _atomicNumber = ATOMICNUMBER_MIN;
                    _isComplete = true;
                }

                if (_atomicNumber > LANTHANIDE_ATOMICNUMBER_START && _atomicNumber <= LANTHANIDE_ATOMICNUMBER_END)
                {
                    Current = Position.LanthanidePosition;
                    return true;
                }

                if (_atomicNumber > ACTINIDE_ATOMICNUMBER_START && _atomicNumber <= ACTINIDE_ATOMICNUMBER_END)
                {
                    Current = Position.ActinidePosition;
                    return true;
                }

                Position current = Current.Offset(0, 1);
                if (_goto.TryGetValue(current, out var position))
                    current = position;

                Current = current;
                return true;
            }

            public void Reset()
            {
                _isComplete = false;
                _atomicNumber = ATOMICNUMBER_MAX;
                Current = Position.EndPosition;
            }

            public void Dispose()
            {
                Reset();
            }
        }
    }
}
