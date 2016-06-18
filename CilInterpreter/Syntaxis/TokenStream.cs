using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis
{
    internal delegate TokenStream ComplexFunction();

    internal class TokenStream
    {
        List<Token> _tokens = new List<Token>();
        public int Position { get; set; } = 0;
        bool _error;
        object _lastElement;
        public Atom CreatedAtom { get; set; }
        List<Data> _dataQueue  = new List<Data>();
        int _queueNumber = 0;

        class Data
        {
            public int QueueNumber { get; set; }
            public string Key { get; set; }
            public object Object { get; set; }
            public Data(int q, string k, object o)
            {
                QueueNumber = q; Key = k; Object = o;
            }
        }

        
        public TokenStream(List<Token> tokens)
        {
            _tokens = new List<Token>(tokens);
            for (int i = 0; i < 16; i++)
                V.Add(0);
        }

        public void Reset(int position = 0)
        {
            _error = false;
            Position = position;
            _dataQueue.Clear();
            CreatedAtom = null;
            _lastElement = null;
        }

        public int C { get; set; }

        public List<int> V { get; } = new List<int>(16);

        public bool End
        {
            get
            {
                if (!_error && CreatedAtom!=null)
                {
                    foreach(var data in _dataQueue)
                    {
                        if (data.QueueNumber >= _queueNumber)
                        {
                            if (data.Object is Token)
                                CreatedAtom.Tokens.Add(data.Key, (Token)data.Object);
                            else if (data.Object is Atom)
                                CreatedAtom.Atoms.Add(data.Key, (Atom)data.Object);
                            else
                                CreatedAtom.Info.Add(data.Key, data.Object);
                        }
                    }                 
                }
                return !_error;
            }
        }

        public bool IsFinish
        {
            get
            {
                return Position == _tokens.Count;
            }
        }

        public TokenStream Is(ITokenType type)
        {
            if (_error) return this;
            if (Position >= _tokens.Count || _tokens[Position].Type != type)
            {
                _error = true;
                return this;
            }
            _lastElement = _tokens[Position];
            Position++;       
            return this;
        }

        public TokenStream Is(string value)
        {
            if (_error) return this;
            if (Position >= _tokens.Count || !_tokens[Position].Value.Equals(value))
            {
                _error = true;
                return this;
            }
            _lastElement = _tokens[Position];
            Position++;        
            return this;
        }

        public TokenStream Empty()
        {
            if (_error) return this;
            if (Position < _tokens.Count)
            {
                _error = true;
                return this;
            }
            Position++;
            _lastElement = null;
            return this;
        }

        public TokenStream OneOrMore(ComplexFunction function)
        {
            if (_error) return this;
            int currentLevel = _queueNumber++;
            function();
            if (_error)
            {
                DiscardData(currentLevel);
                return this;
            }
            CommitData(currentLevel);
            int lastPosition = 0;
            int nextLevel = 0;
            while (!_error)
            {
                nextLevel = _queueNumber++;
                lastPosition = Position;
                function();
            }
            DiscardData(nextLevel);
            CommitData(currentLevel);
            Position = lastPosition;
            _error = false;
            return this;
        }

        public TokenStream ZeroOrOne(ComplexFunction function)
        {
            if (_error) return this;
            int currentLevel = _queueNumber++;
            int lastPosition = Position;
            function();
            if (_error)
            {
                DiscardData(currentLevel);
                _error = false;
                Position = lastPosition;
                return this;
            }
            CommitData(currentLevel);
            return this;
        }

        public TokenStream ZeroOrMore(ComplexFunction function)
        {
            if (_error) return this;
            int currentLevel = _queueNumber++;
            int lastPosition = 0;
            int nextLevel = 0;
            while (!_error)
            {
                nextLevel = _queueNumber++;
                lastPosition = Position;
                function();
            }
            DiscardData(nextLevel);
            CommitData(currentLevel);
            Position = lastPosition;
            _error = false;
            return this;
        }

        public TokenStream OneOf(params string[] variants)
        {
            if (_error) return this;
            int lastPosition = Position;
            foreach(var variant in variants)
            {
                Is(variant);
                if (!_error)
                    return this;
                else
                {
                    Position = lastPosition;
                    _error = false;
                }
            }
            _error = true;
            return this;
        }

        public TokenStream OneOf(params ITokenType[] variants)
        {
            if (_error) return this;
            int lastPosition = Position;
            foreach (var variant in variants)
            {
                Is(variant);
                if (!_error)
                    return this;
                else
                {
                    Position = lastPosition;
                    _error = false;
                }
            }
            _error = true;
            return this;
        }

        public TokenStream OneOf(IEnumerable<IAtomType> variants)
        {
            if (_error) return this;
            int lastPosition = Position;
            foreach (var variant in variants)
            {
                Atom(variant);
                if (!_error)
                    return this;
                else
                {
                    Position = lastPosition;
                    _error = false;
                }
            }
            _error = true;
            return this;
        }

        public TokenStream OneOf(params ComplexFunction[] variants)
        {
            if (_error) return this;
            int lastPosition = Position;
            int currentNumber = _queueNumber++;
            foreach (var variant in variants)
            {
                variant();
                if (_error)
                {
                    DiscardData(currentNumber);
                    Position = lastPosition;
                    _error = false;
                }
                else
                {
                    CommitData(currentNumber);
                    return this;
                }
            }
            _error = true;
            return this;
        }

        public TokenStream Atom(IAtomType atomType)
        {
            if (_error) return this;
            int localC = C;
            C = 0;
            int currentNumber = _queueNumber++;          
            if (CreatedAtom == null)
                atomType.Is(this);
            else
            {
                var outerAtom = CreatedAtom;
                CreatedAtom = atomType.Create();
                _lastElement = atomType.Get(this);
                CreatedAtom = outerAtom;
                DiscardData(currentNumber);               
            }
            C = localC;
            return this;
        }

        public TokenStream Bounds(string left, ComplexFunction action, string right)
        {
            if (_error) return this;
            Is(left);
            if (_error) return this;
            int currentNumber = _queueNumber++;
            action();
            Is(right);
            if (_error)
            {
                DiscardData(currentNumber);
                return this;
            }
            CommitData(currentNumber);
            return this;
        }

        public TokenStream Bounds(string left, ITokenType token, string right)
        {
            return Is(left).Is(token).Is(right);
        }

        public TokenStream As(string key)
        {
            if (CreatedAtom!=null && !_error)
                _dataQueue.Add(new Data(_queueNumber, key, _lastElement));
            return this;
        }

        public TokenStream CInc()
        {
            if (!_error) C = C + 1;
            return this;
        }

        public TokenStream DropC()
        {
            if (!_error) C = 0;
            return this;
        }

        public TokenStream DropV()
        {
            if (!_error)
                for (int i = 0; i < V.Count; i++)
                    V[i] = 0;
            return this;
        }

        public TokenStream Do(Action action)
        {
            if (!_error) action();
            return this;
        }

        public TokenStream Info(string key, object obj)
        {
            if (CreatedAtom != null && !_error)
                _dataQueue.Add(new Data(_queueNumber, key, obj));
            return this;
        }

        private void CommitData(int queueNumber)
        {
            _dataQueue.ForEach((d) =>
            {
                if (d.QueueNumber > queueNumber)
                    d.QueueNumber = queueNumber;
            });
            _queueNumber = queueNumber;
        }

        private void DiscardData(int queueNumber)
        {
            _dataQueue.RemoveAll((d) => d.QueueNumber > queueNumber);
            _queueNumber = queueNumber;
        }
    }
}
