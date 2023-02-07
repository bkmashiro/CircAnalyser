namespace CircAnalyser
{
    class Connection
    {

    }
    public enum PortType
    {
        In,
        Out,
        ExternalIn,
        ExternalOut,
        Undefined,
    }


    public sealed class PortState
    {
        private State _state;
        private PortState(State s) { this._state = s; }
        public static readonly PortState Lo = new(State.Lo);
        public static readonly PortState Hi = new(State.Hi);
        public static readonly PortState Z = new(State.Z);
        enum State
        {
            Hi,
            Lo,
            Z,
        }

        public static implicit operator bool(PortState p)
        {
            return p._state switch
            {
                State.Hi => true,
                State.Lo => false,
                State.Z => false,
                _ => throw new Exception(),
            };
        }

        public static implicit operator PortState(bool b)
        {
            return b switch
            {
                true => Hi,
                false => Lo,
            };
        }
        public override string ToString()
        {
            return _state switch
            {
                State.Hi => "Hi",
                State.Lo => "Lo",
                State.Z => "Z ",
                _ => throw new NotImplementedException(),
            };
        }
    }
    public class Port
    {
        public string Name { get; set; }
        public PortType Type { get; set; } = PortType.Undefined;
        public string? Description { get; set; }
        public PortState PortState { get; set; } = PortState.Z;
        public ComponentBase Parent { get; set; }
        public Port(string name, ComponentBase parent)
        {
            Name = name;
            Parent = parent;
        }
        public Port Lo()
        {
            return Set(PortState.Lo);
        }
        public Port Hi()
        {
            return Set(PortState.Hi);
        }
        public Port Z()
        {
            return Set(PortState.Z);
        }
        public Port Set(PortState state) { this.PortState = state; return this; }

        List<Port> target = new();
        public void Bind(Port p)
        {
            target.Add(p);
        }

        public static Port operator +(Port lhs, Port rhs) 
        {
            lhs.Bind(rhs);
            return lhs;
        }

        public void Forward()
        {
            foreach (var port in target)
            {
                port.Set(this.PortState);
                port.Parent?.Update();
            } 
        }
        public PortState this[object o]
        {
            set { this.PortState = value; }
            get { return this.PortState; }
        }

        public static implicit operator bool(Port p)
        {
            return p.PortState;
        }

        public static implicit operator PortState(Port p)
        {
            return p.PortState;
        }
        public override string ToString()
        {
            return PortState.ToString();
        }

    }
    interface IComponent
    {
        public void Tick();     // triggerred by clock
        public void Update();   // Globally triggerred

    }

    public abstract class ComponentBase : IComponent
    {
        List<Port> _ports = new List<Port>();
        protected Dictionary<string, Port> p = new Dictionary<string, Port>();
        virtual public string? Name { get; set; }
        virtual public string? Description { get; set; }

        public virtual void Tick() { }
        public virtual void PostTick() { }
        public abstract void Update();
        public virtual void PostUpdate() { }
        public void RegisterPort(Port po)
        {
            _ports.Add(po);
            p.Add(po.Name, po);
        }
        public void AddPort(string name)
        {
            RegisterPort(new(name, this));
        }
        public void AddPort(params string[] names)
        {
            foreach (var p in names)
            {
                AddPort(p);
            }
        }

        public Port this[string index]
        {
            get
            {
                return p[index];
            }
            set
            {
                p[index] = value;
            }
        }
    }

    public class Components
    {
        public static void TickAll(params ComponentBase[] componentBases)
        {
            foreach (var component in componentBases)
            {
                component.Tick();
            }
        }

        public static void UpdateAll(params ComponentBase[] componentBases)
        {
            foreach (var component in componentBases)
            {
                component.Update();
            }
        }
        public static void TickAndUpdateAll(params ComponentBase[] componentBases)
        {
            TickAll(componentBases);
            UpdateAll(componentBases);
        }
    }
    [Serializable]
    public class NotTemporalException : Exception
    {
        public NotTemporalException() { }
        public NotTemporalException(string message) : base("Not a temporal component:" + message) { }
        public NotTemporalException(string message, Exception inner) : base(message, inner) { }
        protected NotTemporalException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
