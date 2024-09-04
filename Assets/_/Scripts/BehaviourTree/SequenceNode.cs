using System;

namespace ajc.BehaviourTree
{
    public class SequenceNode : CompositeNode
    {

        private int m_index;
    
        public override STATUS Process(float _deltaTime)
        {
            var childProgressStatus = m_children[m_index].Process(_deltaTime);
            switch (childProgressStatus)
            {
                case STATUS.Failure:
                    m_index = 0;
                    return STATUS.Failure;
                case STATUS.Success:
                {
                    m_index += 1;
                    if (m_index < m_children.Count) return STATUS.Running;
                    m_index = 0;
                    return STATUS.Success;
                }default:
                    return STATUS.Running;
            }
            
        }

        public SequenceNode(string _name) : base(_name)
        {
        }
    }

    public class Selector : CompositeNode
    {
        private int m_index;

        public Selector(string _name) : base(_name)
        {
        }

        public override STATUS Process(float _deltaTime)
        {
            var childProgressStatus = m_children[m_index].Process(_deltaTime);
            switch (childProgressStatus)
            {
                case STATUS.Failure:

                    m_index += 1;
                    if (m_index < m_children.Count) return STATUS.Running;
                    m_index = 0;

                    return STATUS.Failure;
                case STATUS.Success:
                    m_index = 0;
                    return STATUS.Success;
                default:
                    return STATUS.Running;
            }
        }
    }

    public class ExtendedSelector : CompositeNode
    {

        public ExtendedSelector(string _name) : base(_name)
        {
        }

        public override STATUS Process(float _deltaTime)
        {
            foreach (var child in m_children)
            {
                var status = child.Process(_deltaTime);
                switch (status)
                {
                    case STATUS.Running:
                        return STATUS.Running;
                    case STATUS.Success:
                        return STATUS.Success;                       
                    case STATUS.Failure:
                        continue;

                }
                
            }
            return STATUS.Failure;
        }
    }
}