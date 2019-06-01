using System;
using System.Collections.Generic;
using System.Linq;
using SysConsole = System.Console;

namespace JenkinsNET.Console.Internal
{
    internal class ArgumentsGroup
    {
        private readonly Dictionary<string, ArgumentGroupAction> groupMap;
        private readonly Dictionary<string, Action<string>> actionMap;


        public ArgumentsGroup()
        {
            groupMap = new Dictionary<string, ArgumentGroupAction>(StringComparer.OrdinalIgnoreCase);
            actionMap = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase);
        }

        public void Parse(string[] args)
        {
            for (var i = 0; i < args.Length; i++) {
                var arg = args[i];

                if (i == 0 && groupMap.TryGetValue(arg, out var groupItem)) {
                    var subArgs = args.Skip(1).ToArray();
                    groupItem.Action?.Invoke();
                    groupItem.Group.Parse(subArgs);
                    return;
                }

                var x = arg.IndexOfAny(new[] {'=', ':'});
                var key = x >= 0 ? arg.Substring(0, x) : arg;
                var value = x >= 0 ? arg.Substring(x + 1) : null;

                if (actionMap.TryGetValue(key, out var action)) {
                    action.Invoke(value);
                    continue;
                }

                SysConsole.ForegroundColor = ConsoleColor.DarkYellow;
                SysConsole.WriteLine($"Unknown argument '{arg}'!");
            }
        }

        public ArgumentActionBuilder Map(params string[] args)
        {
            return new ArgumentActionBuilder(args, this);
        }

        public class ArgumentActionBuilder
        {
            private readonly string[] arguments;
            private readonly ArgumentsGroup parentGroup;


            public ArgumentActionBuilder(string[] arguments, ArgumentsGroup parentGroup)
            {
                this.arguments = arguments;
                this.parentGroup = parentGroup;
            }

            public ArgumentActionBuilder ToGroup(ArgumentsGroup group, Action action = null)
            {
                foreach (var arg in arguments)
                    parentGroup.groupMap[arg] = new ArgumentGroupAction(group, action);

                return this;
            }

            public ArgumentActionBuilder ToAction(Action<string> action)
            {
                foreach (var arg in arguments)
                    parentGroup.actionMap[arg] = action;

                return this;
            }

            public ArgumentActionBuilder ToAction<T>(Action<T> action, T defaultValue = default)
            {
                foreach (var arg in arguments) {
                    parentGroup.actionMap[arg] = value => {
                        var valueT = value != null
                            ? (T)Convert.ChangeType(value, typeof(T))
                            : defaultValue;

                        action.Invoke(valueT);
                    };
                }

                return this;
            }
        }

        private class ArgumentGroupAction
        {
            public ArgumentsGroup Group {get;}
            public Action Action {get;}

            public ArgumentGroupAction(ArgumentsGroup group, Action action)
            {
                this.Group = group;
                this.Action = action;
            }
        }
    }
}
