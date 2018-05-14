using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageEntityEntryAttribute : EntryAttribute
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DirectionTypes _direction;
        public DirectionTypes Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private InteractionTypes _interaction;
        public InteractionTypes Interaction
        {
            get { return _interaction; }
            set { _interaction = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public MessageEntityEntryAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public MessageEntityEntryAttribute(string name, DirectionTypes direction)
        {
            Name = name;
            Direction = direction;
        }
        public MessageEntityEntryAttribute(string name, InteractionTypes interaction)
        {
            Name = name;
            Interaction = interaction;
        }
        public MessageEntityEntryAttribute(string name, DirectionTypes direction, string description)
        {
            Name = name;
            Direction = direction;
            Description = description;
        }
        public MessageEntityEntryAttribute(string name, InteractionTypes interaction, string description)
        {
            Name = name;
            Interaction = interaction;
            Description = description;
        }
        public MessageEntityEntryAttribute(string name, DirectionTypes direction, InteractionTypes interaction, string description)
        {
            Name = name;
            Direction = direction;
            Interaction = interaction;
            Description = description;
        }
    }
}
