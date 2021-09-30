using System;
using System.Reflection;

namespace SxGD
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OnReadyBase : Attribute
    {
        public string Path { set; get; }
        public bool Root { set; get; }

        public OnReadyBase(string path = "", bool root = false)
        {
            Path = path;
            Root = root;
        }

        public string GetNodePath(FieldInfo field)
        {
            var bindPath = "";

            // Insert /root/
            if (Root)
            {
                bindPath += "/root/";
            }

            // No path bind, return type name
            if (Path == "")
            {
                bindPath += field.FieldType.Name;
            }
            else
            {
                bindPath += Path;
            }

            return bindPath;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class OnReady : OnReadyBase
    {
        public OnReady(string path = "") : base(path, false) { }
    }
}