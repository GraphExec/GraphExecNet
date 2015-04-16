using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphExec
{
    public static class BehaviorNodeExtensions
    {
        public static bool HasProvider<TResult, TProvider>(this IBehaviorNode<TResult, TProvider> _this)
            where TProvider : class, IBehaviorProvider<TResult>
        {
            return _this.Provider != null;
        }

        public static bool HasProvider<TResult, TProviderInfo, TProvider>(this IBehaviorNode<TResult, TProviderInfo, TProvider> _this)
            where TProviderInfo : class, IProviderInfo, new()
            where TProvider : class, IBehaviorProvider<TResult, TProviderInfo>
        {
            return _this.Provider != null;
        }

        public static bool HasProvider<TResult, TBehaviorInfo, TProviderInfo, TProvider>(this IBehaviorNode<TResult, TBehaviorInfo, TProviderInfo, TProvider> _this)
            where TProviderInfo : class, IProviderInfo, new()
            where TBehaviorInfo : class, IBehaviorInfo, new()
            where TProvider : class, IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
        {
            return _this.Provider != null;
        }

        public static bool HasProvider<TResult, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider>(this IBehaviorNode<TResult, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider> _this)
            where TProviderInfo : class, IProviderInfo, new()
            where TBehaviorInfo : class, IBehaviorInfo, new()
            where TNodeInfo : class, INodeInfo, new()
            where TProvider : class, IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
        {
            return _this.Provider != null;
        }
    }
}
