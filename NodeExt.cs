using System;
using System.Reflection;
using Godot;

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

    public static class NodeExt
    {
        /// Automatically bind nodes with a OnReadyBase attribute.
        /// <c>OnReady</c> will get a node corresponding to the type name, unless a path is explicitly given.
        public static void BindNodes(Node node)
        {
            var fields = node.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var customAttr = (OnReadyBase)field.GetCustomAttribute(typeof(OnReadyBase));
                if (customAttr != null)
                {
                    var bindPath = customAttr.GetNodePath(field);

                    // Bind
                    var nodeInstance = node.GetNode(bindPath);
                    field.SetValue(node, nodeInstance);
                }
            }
        }
    }
}
