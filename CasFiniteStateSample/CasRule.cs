using System;
using System.Activities;

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
            var bmn = string.Format("Rule-{0}", RuleNumber);
            context.CreateBookmark(bmn, OnResumeBookmark);
        }

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