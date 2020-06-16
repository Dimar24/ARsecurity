// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using Lazy.Generic;

namespace Lazy
{
    public static class LazySequenceExtensions
    {
        public static LazySequence Join(this LazySequence target, Action action) 
            => target.Join(LazyAction.New(action));

        public static LazySequence Append(this LazySequence target, Action action) 
            => target.Append(LazyAction.New(action));

        public static LazySequence Join(this LazySequence target, Action<Action> action)
            => target.Join(LazyCallback.New(action));

        public static LazySequence Append(this LazySequence target, Action<Action> action) 
            => target.Append(LazyCallback.New(action));
    }
}