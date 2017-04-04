using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace CasFiniteStateSample
{

    public class CasRule : NativeActivity
    {
        // Define an activity input argument of type string
        public int RuleNumber { get; set; }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            if (RuleNumber < 0)
                metadata.AddValidationError("Rule number must not be negative");
            if (RuleNumber == 0)
                metadata.AddValidationError("Please enter a rule number");
            base.CacheMetadata(metadata);
        }

        protected override bool CanInduceIdle => true;
        protected override void Execute(NativeActivityContext context)
        {
            Console.WriteLine("Using rule R{0:n0}", RuleNumber);
            var bmn = string.Format("Rule-{0}", RuleNumber); // preliminary

            var parent = ParentOf(this);
            var transitions = Transitions(parent);
            foreach(var t in transitions)
            {
                if (t.Trigger == this)
                {
                    var to = t.To;
                    var advertisedEvent = to.Entry as CasAdvertiseEvent;
                    if (advertisedEvent != null)
                        bmn = string.Format("to state {0} with event {1}", to.DisplayName, advertisedEvent.EventId);
                }
            }


            context.CreateBookmark(bmn, OnResumeBookmark);
        }

        public static Activity ParentOf(Activity it)
        {
            if (it == null) return null;
            var Parent = it.GetType()
                .GetProperty("Parent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(it, null);
            return Parent as Activity;
        }

        public static IEnumerable<Transition> Transitions(Activity it)
        {
            if (it == null) return null;

            var ret = it.GetType()
                .GetProperty(nameof(Transitions), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .GetValue(it, null);
            return ret as IEnumerable<Transition>;
        }

        //public static T ParentOf<T> (Activity it) where T : Activity
        //{
        //    var t = typeof(T);
        //    var Parent = it;
        //    while (Parent != null && !t.IsAssignableFrom(Parent.GetType()))
        //        Parent = ParentOf(Parent);
        //    return Parent as T;
        //}

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bm, object value)
        {
            Console.WriteLine("Received {0}", bm.Name);
        }

        public override string ToString()
        {
            return string.Format("On rule R{0}", RuleNumber);
        }
    }
}